using HarmonyLib;
using MakeHediffAddiction;
using RimWorld;
using System.Linq;
using Verse;
using Verse.AI;

namespace MakeHediffAddiction
{
    [HarmonyPatch(typeof(MentalBreaker), "TryDoRandomMoodCausedMentalBreak")]
    internal static class Patch_MentalBreaker_TryDoRandomMoodCausedMentalBreak
    {
        /// <summary>
        /// 前缀：在系统随机挑选崩溃类型之前，先检查我们自己的映射表。
        /// 如果找到符合条件的 Hediff，就直接开指定精神状态并跳过原方法。
        /// </summary>
        /// <param name="__instance">正在运作的 MentalBreaker</param>
        /// <param name="___pawn">Harmony 自动注入的私有字段 pawn</param>
        /// <param name="__result">给原方法的返回值赋值</param>
        /// <returns>返回 false = 不再执行原方法</returns>
        static bool Prefix(MentalBreaker __instance, Pawn ___pawn, ref bool __result)
        {
            Pawn pawn = ___pawn;

            foreach (var set in DefDatabase<HediffMentalBreakMappingSetDef>.AllDefs)
            {
                foreach (var mapping in set.mappings)
                {
                    if (pawn.health.hediffSet.HasHediff(mapping.hediff))
                    {
                        //强制开始指定的精神状态
                        bool ok = pawn.mindState.mentalStateHandler.TryStartMentalState(
                            mapping.mentalState,
                            reason: "MentalStateReason_Hediff".Translate(mapping.hediff.label),
                            forced: true
                        );

                        //添加额外 Hediff
                        foreach (var doer in mapping.onBreakDoers)
                        {
                            //用的食物触发的hediff,null表示不需要食物,1：表示吃了一个，severity 会按 severity * 1 应用。
                            doer.DoIngestionOutcome(pawn, null, 1);
                        }

                        __result = true;
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
