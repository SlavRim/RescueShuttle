using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using Verse;

namespace RescueShuttle
{
	public class _FactionDialogMaker_Patch
	{
		[HarmonyPatch(typeof(FactionDialogMaker),nameof(FactionDialogMaker.FactionDialogFor))]
		public class FactionDialogFor
		{
			[HarmonyPostfix]
			public static void Postfix(Pawn negotiator, Faction faction, ref DiaNode __result)
			{
				if (negotiator.Map?.IsPlayerHome == true && faction.def == FactionDefOf.Empire)
				{
					__result.options.Insert(0, RequestRescueShuttle(negotiator.Map, faction, negotiator));
				}
			}

			private static DiaOption RequestRescueShuttle(Map map, Faction faction, Pawn negotiator)
			{
				string text = "Request a rescue shuttle for a colonist";
				if (faction.PlayerRelationKind == FactionRelationKind.Hostile)
				{
					DiaOption optionNoHostiles = new DiaOption(text);
					optionNoHostiles.Disable("Faction is hostile");
					return optionNoHostiles;
				}
				DiaOption optionRequest = new DiaOption(text);
				//DiaNode nodeOnTheirWay = new DiaNode("GuestsOnTheirWay".Translate(travelTicks.ToStringTicksToPeriodVague()));
				//DiaOption optionOK = FactionDialogMaker.OKToRoot(faction, negotiator);
				//nodeOnTheirWay.options.Add(optionOK);
				//optionInvite.link = nodeOnTheirWay;
				optionRequest.action = delegate {

					GenericUtility.ShowTradeDialog(negotiator, OnAccepted);
				};
				return optionRequest;
			}

			private static void OnAccepted(IEnumerable<Pawn> pawns)
			{
				GenericUtility.CallShuttle(pawns);
			}
		}
	}
}
