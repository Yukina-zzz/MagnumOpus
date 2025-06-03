using RimWorld;
using System.Collections.Generic;
using Verse;


namespace MakeHediffAddiction
{
    public class CompProperties_DrugWithBodyPart : CompProperties_Drug
    {
        public List<BodyPartDef> bodyPartDefs = new List<BodyPartDef>();

        public CompProperties_DrugWithBodyPart() => this.compClass = typeof(CompDrugWithBodyPart);
    }


}

public class HediffMentalBreakMapping
{
    public HediffDef hediff;           // 触发用 Hediff  
    public MentalStateDef mentalState; // 指定崩溃  
    public List<IngestionOutcomeDoer> onBreakDoers = new List<IngestionOutcomeDoer>(); // 额外 Hediff（可空）  
}

public class HediffMentalBreakMappingSetDef : Def
{
    public List<HediffMentalBreakMapping> mappings = new List<HediffMentalBreakMapping>();
}
