using System;
using Verse;

namespace Ent
{
	// Token: 0x02000007 RID: 7
	public class RemovableHediff : Hediff
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000021D7 File Offset: 0x000003D7
		public override bool ShouldRemove
		{
			get
			{
				return true;
			}
		}
	}
}
