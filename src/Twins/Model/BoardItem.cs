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
        /// Indeks w słowie
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// Znak z alfabetu
        /// </summary>
        public int? Color { get; set; }
    }
}