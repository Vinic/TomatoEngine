using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoEngine.Helpers
{
    public static class Helpers
    {
        private static PointFloat _pointfloat = new PointFloat(0,0);
        public static PointFloat OffsetPosition(PointFloat pos, float distance, float angle)
        {
            _pointfloat.x = pos.x + ( (float)Math.Cos(angle) * distance );
            _pointfloat.y = pos.y + ( (float)Math.Sin(angle) * distance );
            return _pointfloat;
        }
    }
}
