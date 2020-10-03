using System;
using Verse;

namespace Ent
{
	// Token: 0x02000002 RID: 2
	public class HealingFactorProperties : DefModExtension
	{
		// Token: 0x04000001 RID: 1
		public int tendWoundStage;

		// Token: 0x04000002 RID: 2
		public int regrowBodypartStage;

		// Token: 0x04000003 RID: 3
		public int healOldWoundStage;

		// Token: 0x04000004 RID: 4
		public float healWoundFactorMin = 0.01f;

		// Token: 0x04000005 RID: 5
		public float healWoundFactorMax = 5f;

		// Token: 0x04000006 RID: 6
		public int ticksBetweenMajorHeal = 2000;

		// Token: 0x04000007 RID: 7
		public int ticksBetweenMinorHeal = 20;
	}
}
