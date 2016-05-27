using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twins.Helpers
{
    class ColorNormalizer
    {
        public static int NormalizeColor(int color, Dictionary<int, int> colorMap)
        {
            if (colorMap.ContainsKey(color))
            {
                return colorMap[color];
            }

            var normalizedColor = colorMap.Count;
            colorMap[color] = normalizedColor;

            return normalizedColor;
        }
    }
}
