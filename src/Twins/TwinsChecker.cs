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
            for (int subSequenceLength = 1; subSequenceLength <= sequence.Count / 2; subSequenceLength++)
            {
                for (int index = 0; index + 2 * subSequenceLength <= sequence.Count; index++)
                {
                    if (sequence.CheckTightTwins(index, subSequenceLength))
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
            for (int i = 0; i < subSequenceLength; i++)
            {
                if (sequence[index + i].Color != sequence[subSequenceLength + index + i].Color)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
