using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace RescueShuttle
{
	public class Dialog_Trade_Shuttle : Dialog_Trade
	{
		private readonly Action<IEnumerable<Pawn>> onAccepted;
		public static readonly List<Pawn> rescuePawns = new List<Pawn>();

		public Dialog_Trade_Shuttle(Pawn negotiator, ITrader trader, Action<IEnumerable<Pawn>> onAccepted) : base(negotiator, trader)
		{
			rescuePawns.Clear();
			this.onAccepted = onAccepted;
		}

		public override void Close(bool doCloseSound = true)
		{
			base.Close(doCloseSound);
			//Log.Message($"Closed dialog. Pawns = {rescuePawns.Select(p => p.NameShortColored.ToString()).ToCommaList()}");

			if (rescuePawns.Any())
			{
				onAccepted.Invoke(rescuePawns);
			}
		}
	}
}
