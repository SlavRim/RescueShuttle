using HarmonyLib;
using RimWorld;

namespace RescueShuttle
{
	public class _TransferableUIUtility_Patch
	{
		/// <summary>
		/// So our trader doesn't show "Join as slave" or "Join as colonist"
		/// </summary>
		[HarmonyPatch(typeof(TransferableUIUtility), nameof(TransferableUIUtility.DrawCaptiveTradeInfo))]
		public class DrawCaptiveTradeInfo
		{
			public static bool Prefix(ITrader trader)
			{
				return !(trader is EmpireShuttleTrader);
			}
		}
	}
}
