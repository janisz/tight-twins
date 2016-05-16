using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Twins.Model;

namespace Twins
{
    public class TwinsChecker
    {
        /// <summary>
        /// Zwraca prawdę jeśli istnieją ciasne bliźniaki
        /// </summary>
        /// <param name="boardItems"></param>
        /// <returns></returns>
        public static bool CheckTwins(Collection<BoardItem> sequence)
        {
            for (int n = 2; n <= sequence.Count; n += 2)
            {
                for (int startIndex = 0; startIndex < sequence.Count - n; startIndex++)
                {
                    if (sequence.CheckTightTwins(startIndex, n))
                    {
                        return true;
                    }                   
                }
            }
            return false;
        }
    }
     public static class ArrayExtensions
     {
        public static bool CheckTightTwins(this Collection<BoardItem> sequence, int index, int subSequenceLength)
        {
            for (int i = index; i < sequence.Count - subSequenceLength; i++)
            {
                var subsets = Subsets(sequence.Skip(i).Take(subSequenceLength));
                foreach ( var pair in subsets)
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
                        if (x != y.Current)
                        {
                            equal = false;
                            break;
                        }
                        y.MoveNext();
                    }
                   
                    if (equal) 
                    {
                        foreach (var item in sequence)
                        {
                            item.TwinIndex = null;
                        }
                        foreach (var item in pair.Item1)
                        {
                            item.TwinIndex = 0;
                        }
                        foreach (var item in pair.Item2)
                        {
                            item.TwinIndex = 1;
                        }
                        return true;
                    }
                }
            }

            return false;
        }

        public static IEnumerable<Tuple<IEnumerable<BoardItem>, IEnumerable<BoardItem>>> Subsets(IEnumerable<BoardItem> source)
        {
            List<BoardItem> list = source.ToList();
            int length = list.Count;
            int max = (int)Math.Pow(2, list.Count);

            for (int count = 0; count < max; count++)
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
    }
}
