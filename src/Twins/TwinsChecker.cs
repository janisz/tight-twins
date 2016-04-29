using System.Collections.Generic;
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
        public static bool CheckTwins(IEnumerable<BoardItem> boardItems)
        {
            // TODO: Zamienić IEnumerable na IList
            int count = 0;
            foreach (var item in boardItems)
            {
                count++;
            }
            string[] board = new string[count];
            foreach (var item in boardItems)
            {
                board[item.Value] = item?.Color.ToString() ?? "";
            }
            return findTwins(System.String.Join("", board)).Length != 0;
        }

        /// <summary>
        /// Zwraca najkrótszego istniejącego ciasnego bliżniaka
        /// </summary>
        /// <param name="sequence">słowo nad alfabetem</param>
        /// <returns>Najkrótszy istnijący ciasny bliźniak lub pusty jeśli nie znaleziono</returns>
        private static string findTwins(string sequence) {
            for (int subSequenceLength = 1; subSequenceLength <= sequence.Length / 2; subSequenceLength++) {
                for (int index = 0; index + 2*subSequenceLength <= sequence.Length; index++) {
                    string a = sequence.Substring(index, subSequenceLength);
                    string b = sequence.Substring(index+subSequenceLength, subSequenceLength);
                    if (a == b) {
                        return a;
                    }
                }
            }
            return "";
        }
    }
}
