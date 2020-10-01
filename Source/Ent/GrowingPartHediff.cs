using System;
using System.Text;
using Ent.Logic;
using RimWorld;
using Verse;

namespace Ent
{
	// Token: 0x02000006 RID: 6
	public class GrowingPartHediff : Hediff_AddedPart
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000021AC File Offset: 0x000003AC
		public override bool ShouldRemove
		{
			get
			{
				return this.Severity >= this.def.maxSeverity;
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000021C4 File Offset: 0x000003C4
		public override void ExposeData()
		{
			base.ExposeData();
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000027B8 File Offset: 0x000009B8
		public override string TipStringExtra
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(base.TipStringExtra);
				stringBuilder.AppendLine(Translator.Translate("Efficiency") + ": " + this.def.addedPartProps.partEfficiency.ToStringPercent());
				stringBuilder.AppendLine("Growth: " + this.Severity.ToStringPercent());
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002830 File Offset: 0x00000A30
		public override void PostRemoved()
		{
			base.PostRemoved();
			bool flag = this.Severity >= 1f;
			if (flag)
			{
				this.pawn.ReplaceHediffFromBodypart(base.Part, HediffDefOf.MissingBodyPart, EntHediffDefOf.EntCuredBodypart);
			}
		}
	}
}
