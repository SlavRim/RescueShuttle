using RimWorld;
using RimWorld.QuestGen;
using Verse;

namespace RescueShuttle
{
	public class QuestNode_GetFactionMember : QuestNode
	{
		[NoTranslate]
		public SlateRef<string> storeAs;

		public SlateRef<Faction> faction;

		public override void RunInt()
		{
			var quest = QuestGen.quest;
			var slate = QuestGen.slate;

			var f = faction.GetValue(slate);
			if (!slate.TryGet<Pawn>(storeAs.GetValue(slate), out var p))
			{
				var pawn = quest.GetPawn(new QuestGen_Pawns.GetPawnParms
				{
					canGeneratePawn = true,
					mustBeOfFaction = f
				});
				if (pawn.Faction != null && !pawn.Faction.Hidden)
				{
					QuestPart_InvolvedFactions questPart_InvolvedFactions = new QuestPart_InvolvedFactions();
					questPart_InvolvedFactions.factions.Add(pawn.Faction);
					QuestGen.quest.AddPart(questPart_InvolvedFactions);
				}
				QuestGen.slate.Set(storeAs.GetValue(slate), p);
			}
		}
		public override bool TestRunInt(Slate slate)
		{
			return storeAs.GetValue(slate) != null && faction.GetValue(slate) != null;
		}
	}
}
