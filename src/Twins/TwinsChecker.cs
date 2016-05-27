using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Twins.Helpers;
using Twins.Model;

namespace Twins
{
    public class TwinsChecker
    {
        static Dictionary<string, bool> Cache = new Dictionary<string,bool>(1000);

        /// <summary>
        /// Zwraca prawdę jeśli istnieją ciasne bliźniaki
        /// </summary>
        /// <param name="boardItems"></param>
        /// <returns></returns>
        public static bool CheckTwins(ICollection<BoardItem> sequence)
        {
            var sequenceString = SequenceToNormalizedString(sequence);
            if (Cache.ContainsKey(sequenceString))
            {
                return Cache[sequenceString];
            }
            var result = FindTightTwins(sequence) != null;

            Cache[sequenceString] = result;
            return result;
        }

        public static string SequenceToNormalizedString(ICollection<BoardItem> sequence)
        {
            var colorMap = new Dictionary<int, int>(100);

            return string.Join(" ", sequence.Select(x => ColorNormalizer.NormalizeColor(x.Color ?? -1, colorMap).ToString()));
        }

        public static Tuple<IEnumerable<BoardItem>, IEnumerable<BoardItem>> FindTightTwins(ICollection<BoardItem> sequence)
        {
            for (int n = 2; n <= sequence.Count; n += 2)
            {
                for (int startIndex = 0; startIndex <= sequence.Count - n; startIndex++)
                {
                    var twins = sequence.CheckTightTwins(startIndex, n);
                    if (twins != null)
                    {
                        return twins;
                    }
                }
            }
            return null;
        }
    }    

    public static class ArrayExtensions
    {
        private const int maxLenght = 123456;
        private static readonly int[] onesCount = new int[maxLenght];

        static ArrayExtensions()
        {
            for (int i = 0; i < maxLenght; i++)
            {
                onesCount[i] = Convert.ToString(i, 2).Split('1').Length - 1;
            }
        }

        public static Tuple<IEnumerable<BoardItem>, IEnumerable<BoardItem>> CheckTightTwins(this ICollection<BoardItem> sequence, int index, int subSequenceLength)
        {
            for (int i = index; i <= sequence.Count - subSequenceLength; i++)
            {
                var subsets = Subsets(sequence.Skip(i).Take(subSequenceLength));
                foreach (var pair in subsets)
                {
                    if (pair.Item1.Count() != subSequenceLength / 2)
                    {
                        continue;
                    }
                    bool equal = true;
                    var y = pair.Item2.GetEnumerator();
                    y.MoveNext();
                    foreach (var x in pair.Item1)
                    {
                        if (!x.Equals(y.Current))
                        {
                            equal = false;
                            break;
                        }
                        y.MoveNext();
                    }                    

                    if (equal)
                    {
                        return pair;
                    }
                }
            }

            return null;
        }

        public static IEnumerable<Tuple<IEnumerable<BoardItem>, IEnumerable<BoardItem>>> Subsets(IEnumerable<BoardItem> source)
        {
            List<BoardItem> list = source.ToList();
            int length = list.Count;

            if (length > maxLenght)
            {
                throw new ArgumentOutOfRangeException("length", length, "Lenght must be less than " + maxLenght);
            }

            foreach (int count in NumbersWithOnes(length / 2))
            {
                List<BoardItem> first = new List<BoardItem>();
                List<BoardItem> second = new List<BoardItem>();
                uint rs = 0;
                while (rs < length)
                {
                    if ((count & (1u << (int)rs)) > 0)
                    {
                        first.Add(list[(int)rs]);
                    }
                    else
                    {
                        second.Add(list[(int)rs]);
                    }
                    rs++;
                }
                yield return new Tuple<IEnumerable<BoardItem>, IEnumerable<BoardItem>>(first, second);
            }
        }
        private static IEnumerable<int> NumbersWithOnes(int n)
        {
            for (int i = 0; i < onesCount.Length; i++)
            {
                if (onesCount[i] == n)
                {
                    yield return i;
                }
            }
        }
    }
}