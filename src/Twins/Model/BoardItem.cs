using PropertyChanged;

namespace Twins.Model
{
    [ImplementPropertyChanged]
    public class BoardItem
    {
        /// <summary>
        /// Na pozycji jest znak alfabetu
        /// </summary>
        public bool IsSelected { get; set; }
        /// <summary>
        /// Indeks w słowie liczony od 1
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// Znak z alfabetu
        /// </summary>
        public int? Color { get; set; }
        /// <summary>
        /// Numer bliźniaka 0/1
        /// </summary>
        public int? TwinIndex { get; set; }

        public static bool operator ==(BoardItem a, BoardItem b)
        {
            return a.Color == b.Color;
        }

        public static bool operator !=(BoardItem a, BoardItem b)
        {
            return !(a == b);
        }
    }
}