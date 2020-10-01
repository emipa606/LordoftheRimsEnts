using System;
using System.Collections.Generic;
using System.Linq;
using Ent.Logic;
using RimWorld;
using Verse;

namespace Ent
{
	// Token: 0x02000003 RID: 3
	public class HealingfactorHediff : HediffWithComps
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002081 File Offset: 0x00000281
		public HealingFactorProperties HealingFactorProps
		{
			get
			{
				if (this.healingFactorProps == null)
				{
					this.healingFactorProps = this.def.GetModExtension<HealingFactorProperties>();
					if (this.healingFactorProps == null)
					{
						this.healingFactorProps = new HealingFactorProperties();
					}
				}
				return this.healingFactorProps;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020BB File Offset: 0x000002BB
		public override void PostMake()
		{
			base.PostMake();
			this.SetNextMajorTick();
			this.SetNextMinorTick();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020CF File Offset: 0x000002CF
		public override void ExposeData()
		{
			base.ExposeData();
			Scribe_Values.Look<int>(ref this.ticksUntilNextMinorHeal, "ticksUntilNextMinorHeal", 0, false);
			Scribe_Values.Look<int>(ref this.ticksUntilNextMajorHeal, "ticksUntilNextMajorHeal", 0, false);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002414 File Offset: 0x00000614
		public override void Tick()
		{
			base.Tick();
			if (Current.Game.tickManager.TicksGame >= this.ticksUntilNextMinorHeal)
			{
				this.TryHealWounds();
				this.SetNextMinorTick();
			}
			if (Current.Game.tickManager.TicksGame >= this.ticksUntilNextMajorHeal)
			{
				if (this.CurStageIndex >= this.HealingFactorProps.healOldWoundStage)
				{
					this.TryHealRandomOldWound();
				}
				if (this.CurStageIndex >= this.HealingFactorProps.tendWoundStage)
				{
					this.TrySealWounds();
				}
				if (this.CurStageIndex >= this.HealingFactorProps.regrowBodypartStage)
				{
					this.TryRegrowBodyparts();
				}
				this.SetNextMajorTick();
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000024CC File Offset: 0x000006CC
		public void TryHealWounds()
		{
			IEnumerable<Hediff> enumerable = from hd in this.pawn.health.hediffSet.hediffs
			where hd is Hediff_Injury && !hd.IsPermanent()
			select hd;
			if (enumerable != null)
			{
				foreach (Hediff hediff in enumerable)
				{
					hediff.Severity -= GenMath.LerpDouble(0f, this.def.maxSeverity, this.HealingFactorProps.healWoundFactorMin, this.HealingFactorProps.healWoundFactorMax, this.Severity);
				}
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002588 File Offset: 0x00000788
		private void TryHealRandomOldWound()
		{
            if ((from hd in this.pawn.health.hediffSet.hediffs
                 where hd.IsPermanent()
                 select hd).TryRandomElement(out Hediff hediff))
            {
                hediff.Severity = 0f;
            }
        }

		// Token: 0x06000008 RID: 8 RVA: 0x000025E4 File Offset: 0x000007E4
		public void TrySealWounds()
		{
			IEnumerable<Hediff> enumerable = from hd in this.pawn.health.hediffSet.hediffs
			where hd.Bleeding
			select hd;
			if (enumerable != null)
			{
				foreach (Hediff hediff in enumerable)
				{
                    if (hediff is HediffWithComps hediffWithComps)
                    {
                        HediffComp_TendDuration hediffComp_TendDuration = hediffWithComps.TryGetComp<HediffComp_TendDuration>();
                        hediffComp_TendDuration.tendQuality = 2f;
                        hediffComp_TendDuration.tendTicksLeft = Find.TickManager.TicksGame;
                        this.pawn.health.Notify_HediffChanged(hediff);
                    }
                }
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000026A4 File Offset: 0x000008A4
		public void TryRegrowBodyparts()
		{
			using (IEnumerator<BodyPartRecord> enumerator = this.pawn.GetFirstMatchingBodyparts(this.pawn.RaceProps.body.corePart, HediffDefOf.MissingBodyPart, EntHediffDefOf.EntProtoBodypart, (Hediff hediff) => hediff is Hediff_AddedPart).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					BodyPartRecord part = enumerator.Current;
					Hediff hediff2 = this.pawn.health.hediffSet.hediffs.First((Hediff hediff) => hediff.Part == part && hediff.def == HediffDefOf.MissingBodyPart);
					if (hediff2 != null)
					{
						this.pawn.health.RemoveHediff(hediff2);
						this.pawn.health.AddHediff(EntHediffDefOf.EntProtoBodypart, part, null, null);
						this.pawn.health.hediffSet.DirtyCache();
					}
				}
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020FB File Offset: 0x000002FB
		public void SetNextMajorTick()
		{
			this.ticksUntilNextMajorHeal = Current.Game.tickManager.TicksGame + this.HealingFactorProps.ticksBetweenMajorHeal;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000211E File Offset: 0x0000031E
		public void SetNextMinorTick()
		{
			this.ticksUntilNextMinorHeal = Current.Game.tickManager.TicksGame + this.HealingFactorProps.ticksBetweenMinorHeal;
		}

		// Token: 0x04000008 RID: 8
		public int ticksUntilNextMinorHeal;

		// Token: 0x04000009 RID: 9
		public int ticksUntilNextMajorHeal;

		// Token: 0x0400000A RID: 10
		protected HealingFactorProperties healingFactorProps;
	}
}
