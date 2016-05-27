using PropertyChanged;
using System;

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

        public override bool Equals(object obj)
        {
           if (obj == null || GetType() != obj.GetType()) return false;
           BoardItem b = (BoardItem)obj;
           return Color == b.Color;
        }
   
        public override int GetHashCode() {
            return ((this.Value * 251) + this.Color ?? 0) * 251 + this.TwinIndex ?? 0;
        }

        public BoardItem() { }
        public BoardItem(int? color) {
            this.Color = color;
        }
    }
}
