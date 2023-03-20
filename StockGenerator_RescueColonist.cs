using System.Collections.Generic;
using RimWorld;
using Verse;

namespace RescueShuttle
{
	public class StockGenerator_RescueColonist : StockGenerator
	{
		public override IEnumerable<Thing> GenerateThings(int forTile, Faction faction = null)
		{
			yield break;
		}

		public override bool HandlesThingDef(ThingDef thingDef)
		{
			if (thingDef.category == ThingCategory.Pawn && thingDef.race.Humanlike)
			{
				return thingDef.tradeability != Tradeability.None;
			}

			if (thingDef == ThingDefOf.Silver) return true;

			return false;
		}
	}
}
