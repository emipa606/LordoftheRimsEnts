using System;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Ent.Logic
{
    // Token: 0x02000009 RID: 9
    public static class BodypartUtility
    {
        // Token: 0x0600001C RID: 28 RVA: 0x000021E3 File Offset: 0x000003E3
        public static IEnumerable<BodyPartRecord> GetFirstMatchingBodyparts(this Pawn pawn, BodyPartRecord startingPart, HediffDef hediffDef)
        {
            List<Hediff> hediffs = pawn.health.hediffSet.hediffs;
            List<BodyPartRecord> currentSet = new List<BodyPartRecord>();
            List<BodyPartRecord> nextSet = new List<BodyPartRecord>
        {
            startingPart
        };
            do
            {
                currentSet.AddRange(nextSet);
                nextSet.Clear();
                foreach (BodyPartRecord part in currentSet)
                {
                    bool matchingPart = false;
                    for (int i = hediffs.Count - 1; i >= 0; i--)
                    {
                        Hediff hediff = hediffs[i];
                        if (hediff.Part == part && hediff.def == hediffDef)
                        {
                            matchingPart = true;
                            yield return part;
                        }
                    }
                    if (!matchingPart)
                    {
                        for (int j = 0; j < part.parts.Count; j++)
                        {
                            nextSet.Add(part.parts[j]);
                        }
                    }
                }
                currentSet.Clear();
            }
            while (nextSet.Count > 0);
        }

        // Token: 0x0600001D RID: 29 RVA: 0x00002201 File Offset: 0x00000401
        public static IEnumerable<BodyPartRecord> GetFirstMatchingBodyparts(this Pawn pawn, BodyPartRecord startingPart, HediffDef hediffDef, HediffDef hediffExceptionDef)
        {
            List<Hediff> hediffs = pawn.health.hediffSet.hediffs;
            List<BodyPartRecord> currentSet = new List<BodyPartRecord>();
            List<BodyPartRecord> nextSet = new List<BodyPartRecord>
        {
            startingPart
        };
            do
            {
                currentSet.AddRange(nextSet);
                nextSet.Clear();
                foreach (BodyPartRecord part in currentSet)
                {
                    bool matchingPart = false;
                    for (int i = hediffs.Count - 1; i >= 0; i--)
                    {
                        Hediff hediff = hediffs[i];
                        if (hediff.Part == part)
                        {
                            if (hediff.def == hediffExceptionDef)
                            {
                                matchingPart = true;
                                break;
                            }
                            if (hediff.def == hediffDef)
                            {
                                matchingPart = true;
                                yield return part;
                                break;
                            }
                        }
                    }
                    if (!matchingPart)
                    {
                        for (int j = 0; j < part.parts.Count; j++)
                        {
                            nextSet.Add(part.parts[j]);
                        }
                    }
                }
                currentSet.Clear();
            }
            while (nextSet.Count > 0);
        }

        // Token: 0x0600001E RID: 30 RVA: 0x00002226 File Offset: 0x00000426
        public static IEnumerable<BodyPartRecord> GetFirstMatchingBodyparts(this Pawn pawn, BodyPartRecord startingPart, HediffDef hediffDef, HediffDef hediffExceptionDef, Predicate<Hediff> extraExceptionPredicate)
        {
            List<Hediff> hediffs = pawn.health.hediffSet.hediffs;
            List<BodyPartRecord> currentSet = new List<BodyPartRecord>();
            List<BodyPartRecord> nextSet = new List<BodyPartRecord>
        {
            startingPart
        };
            do
            {
                currentSet.AddRange(nextSet);
                nextSet.Clear();
                foreach (BodyPartRecord part in currentSet)
                {
                    bool matchingPart = false;
                    for (int i = hediffs.Count - 1; i >= 0; i--)
                    {
                        Hediff hediff = hediffs[i];
                        if (hediff.Part == part)
                        {
                            if (hediff.def == hediffExceptionDef || extraExceptionPredicate(hediff))
                            {
                                matchingPart = true;
                                break;
                            }
                            if (hediff.def == hediffDef)
                            {
                                matchingPart = true;
                                yield return part;
                                break;
                            }
                        }
                    }
                    if (!matchingPart)
                    {
                        for (int j = 0; j < part.parts.Count; j++)
                        {
                            nextSet.Add(part.parts[j]);
                        }
                    }
                }
                currentSet.Clear();
            }
            while (nextSet.Count > 0);
        }

        // Token: 0x0600001F RID: 31 RVA: 0x00002253 File Offset: 0x00000453
        public static IEnumerable<BodyPartRecord> GetFirstMatchingBodyparts(this Pawn pawn, BodyPartRecord startingPart, HediffDef hediffDef, HediffDef[] hediffExceptionDefs)
        {
            List<Hediff> hediffs = pawn.health.hediffSet.hediffs;
            List<BodyPartRecord> currentSet = new List<BodyPartRecord>();
            List<BodyPartRecord> nextSet = new List<BodyPartRecord>
        {
            startingPart
        };
            do
            {
                currentSet.AddRange(nextSet);
                nextSet.Clear();
                foreach (BodyPartRecord part in currentSet)
                {
                    bool matchingPart = false;
                    for (int i = hediffs.Count - 1; i >= 0; i--)
                    {
                        Hediff hediff = hediffs[i];
                        if (hediff.Part == part)
                        {
                            if (hediffExceptionDefs.Contains(hediff.def))
                            {
                                matchingPart = true;
                                break;
                            }
                            if (hediff.def == hediffDef)
                            {
                                matchingPart = true;
                                yield return part;
                                break;
                            }
                        }
                    }
                    if (!matchingPart)
                    {
                        for (int j = 0; j < part.parts.Count; j++)
                        {
                            nextSet.Add(part.parts[j]);
                        }
                    }
                }
                currentSet.Clear();
            }
            while (nextSet.Count > 0);
        }

        // Token: 0x06000020 RID: 32 RVA: 0x00002278 File Offset: 0x00000478
        public static IEnumerable<BodyPartRecord> GetFirstMatchingBodyparts(this Pawn pawn, BodyPartRecord startingPart, HediffDef[] hediffDefs)
        {
            List<Hediff> hediffs = pawn.health.hediffSet.hediffs;
            List<BodyPartRecord> currentSet = new List<BodyPartRecord>();
            List<BodyPartRecord> nextSet = new List<BodyPartRecord>
        {
            startingPart
        };
            do
            {
                currentSet.AddRange(nextSet);
                nextSet.Clear();
                foreach (BodyPartRecord part in currentSet)
                {
                    bool matchingPart = false;
                    for (int i = hediffs.Count - 1; i >= 0; i--)
                    {
                        Hediff hediff = hediffs[i];
                        if (hediff.Part == part && hediffDefs.Contains(hediff.def))
                        {
                            matchingPart = true;
                            yield return part;
                            break;
                        }
                    }
                    if (!matchingPart)
                    {
                        for (int j = 0; j < part.parts.Count; j++)
                        {
                            nextSet.Add(part.parts[j]);
                        }
                    }
                }
                currentSet.Clear();
            }
            while (nextSet.Count > 0);
        }

        // Token: 0x06000021 RID: 33 RVA: 0x00002878 File Offset: 0x00000A78
        public static void ReplaceHediffFromBodypart(this Pawn pawn, BodyPartRecord startingPart, HediffDef hediffDef, HediffDef replaceWithDef)
        {
            List<Hediff> hediffs = pawn.health.hediffSet.hediffs;
            List<BodyPartRecord> list = new List<BodyPartRecord>();
            List<BodyPartRecord> list2 = new List<BodyPartRecord>
            {
                startingPart
            };
            do
            {
                list.AddRange(list2);
                list2.Clear();
                foreach (BodyPartRecord item2 in list)
                {
                    for (int num = hediffs.Count - 1; num >= 0; num--)
                    {
                        Hediff val = hediffs[num];
                        if (val.Part == item2 && val.def == hediffDef)
                        {
                            Hediff val2 = hediffs[num];
                            hediffs.RemoveAt(num);
                            val2.PostRemoved();
                            Hediff item = HediffMaker.MakeHediff(replaceWithDef, pawn, item2);
                            hediffs.Insert(num, item);
                        }
                    }
                    for (int i = 0; i < item2.parts.Count; i++)
                    {
                        list2.Add(item2.parts[i]);
                    }
                }
                list.Clear();
            }
            while (list2.Count > 0);
        }
    }
}
