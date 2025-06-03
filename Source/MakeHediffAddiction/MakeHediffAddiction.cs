using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MakeHediffAddiction
{
    public class MakeHediffAddiction : Mod
    {
        public MakeHediffAddiction(ModContentPack content) : base(content)
        {
            new Harmony("MoManaCha.MagnumOpus").PatchAll();
        }
    }
}
