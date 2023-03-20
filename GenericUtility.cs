using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using RimWorld.QuestGen;
using Verse;

namespace RescueShuttle
{
	public static class GenericUtility
	{
		public static readonly TraderKindDef empireRescueDef = DefDatabase<TraderKindDef>.GetNamed("RescueShuttle_Empire");
		public static EmpireShuttleTrader empire = new EmpireShuttleTrader();

		[DebugAction("Rescue Shuttle", allowedGameStates = AllowedGameStates.PlayingOnMap)]
		public static void ShowTradeDialog()
		{
			ShowTradeDialog(Find.CurrentMap.mapPawns.FreeColonistsSpawned.RandomElement(), pawns => Log.Message($"Selected {pawns?.Count()} pawns."));
		}
		public static void ShowTradeDialog(Pawn negotiator, Action<IEnumerable<Pawn>> onAccepted)
		{
			Find.WindowStack.Add(new Dialog_Trade_Shuttle(negotiator, empire, onAccepted));
		}

		[DebugAction("Rescue Shuttle", allowedGameStates = AllowedGameStates.PlayingOnMap)]
		public static void CallShuttle()
		{
			CallShuttle(Find.CurrentMap.mapPawns.FreeColonistsSpawned.TakeRandom(2));
		}

		public static void CallShuttle(IEnumerable<Pawn> colonists)
		{
			QuestScriptDef script = DefDatabase<QuestScriptDef>.GetNamed("RescueShuttle_PawnPickup");
			Slate slate = new Slate();
			slate.Set("colonistsToRescue", colonists);
			slate.Set("map", Find.CurrentMap);
			slate.Set("permitFaction", Find.FactionManager.OfEmpire);
			QuestUtility.GenerateQuestAndMakeAvailable(script, slate);
		}
	}
}
