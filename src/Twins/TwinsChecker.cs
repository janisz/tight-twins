using System.Collections.Generic;
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
        public static bool CheckTwins(ICollection<BoardItem> boardItems)
        {
            var board = boardItems
                .Select(item => item.Color ?? 0)
                .ToArray();

            return CheckTwins(board);
        }

        private static bool CheckTwins(int[] sequence)
        {
            for (int subSequenceLength = 1; subSequenceLength <= sequence.Length / 2; subSequenceLength++)
            {
                for (int index = 0; index + 2 * subSequenceLength <= sequence.Length; index++)
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
        public static bool CheckTightTwins(this int[] sequence, int index, int subSequenceLength)
        {
            for (int i = 0; i < subSequenceLength; i++)
            {
                if (sequence[index + i] != sequence[subSequenceLength + index + i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
