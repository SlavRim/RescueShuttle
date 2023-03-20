using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace RescueShuttle
{
	public class EmpireShuttleTrader : ITrader
	{

		public IEnumerable<Thing> ColonyThingsWillingToBuy(Pawn playerNegotiator)
		{
			return TradeUtility.AllLaunchableThingsForTrade(playerNegotiator.MapHeld, this).Where(t => t.def == ThingDefOf.Silver);
			//return Find.CurrentMap.mapPawns.FreeColonistsSpawned.Select(p=>new RescuablePawn(p));
		}

		public void GiveSoldThingToTrader(Thing toGive, int countToGive, Pawn playerNegotiator)
		{
			if (toGive.def != ThingDefOf.Silver) return;

			Thing thing = toGive.SplitOff(countToGive);
			thing.PreTraded(TradeAction.PlayerSells, playerNegotiator, this);
			Thing thing2 = TradeUtility.ThingFromStockToMergeWith(this, thing);
			if (thing2 != null)
			{
				if (!thing2.TryAbsorbStack(thing, false)) thing.Destroy();
			}
		}

		public void GiveSoldThingToPlayer(Thing toGive, int countToGive, Pawn playerNegotiator)
		{
			if(toGive is Pawn pawn) Dialog_Trade_Shuttle.rescuePawns.Add(pawn);
		}

		public TraderKindDef TraderKind => GenericUtility.empireRescueDef;
		public IEnumerable<Thing> Goods => Find.CurrentMap.mapPawns.FreeColonists;
		public int RandomPriceFactorSeed => 43643626;
		public string TraderName => Faction.OfEmpire.Name;
		public bool CanTradeNow => true;
		public float TradePriceImprovementOffsetForPlayer => 0;
		public Faction Faction => null; // so gifting doesn't become an option
		public TradeCurrency TradeCurrency => TradeCurrency.Silver;
	}
}
