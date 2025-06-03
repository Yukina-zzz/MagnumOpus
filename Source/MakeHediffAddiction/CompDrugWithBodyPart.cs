using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using Verse;


namespace MakeHediffAddiction
{
    public class CompDrugWithBodyPart : CompDrug
    {
        public new CompProperties_DrugWithBodyPart Props => (CompProperties_DrugWithBodyPart)this.props;

        public List<BodyPartDef> bodyPartDefs => this.Props.bodyPartDefs ?? (List<BodyPartDef>)null;

        public override void PrePostIngested(Pawn ingester)
        {
            if (!this.Props.Addictive || !ingester.RaceProps.IsFlesh)
                return;
            HediffDef addictionHediff = this.Props.chemical.addictionHediff;
            BodyPartDef targetBodyPartDef = (BodyPartDef)null;
            if (this.bodyPartDefs != null && this.bodyPartDefs.Count > 0)
                targetBodyPartDef = this.bodyPartDefs.RandomElement<BodyPartDef>();
            if (targetBodyPartDef != null)
            {
                BodyPartRecord part1 = ingester.health.hediffSet.GetNotMissingParts().FirstOrDefault<BodyPartRecord>((Func<BodyPartRecord, bool>)(part => part.def == targetBodyPartDef));
                if (part1 != null)
                    ingester.health.AddHediff(addictionHediff, part1);
            }
            else
                ingester.health.AddHediff(addictionHediff);
        }
    }
}
