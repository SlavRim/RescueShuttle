using HugsLib;
using RimWorld;
using RimWorld.Planet;
using RimWorld.QuestGen;
using Verse;

namespace RescueShuttle
{
    [StaticConstructorOnStartup]
    public class ModBaseRescueShuttle : ModBase
    {
        public override string ModIdentifier => "RescueShuttle";

        //private static Settings settings;

        public override void DefsLoaded()
        {
            //settings = new Settings(Settings);
        }
    }
}