using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomatoEngine.Helpers
{
    public static class PhysicsAndPositions
    {
        public static float PI = (float)Math.PI;
        private static PointFloat _pointfloat = new PointFloat(0,0);
        public static PointFloat OffsetPosition(PointFloat pos, float distance, float angle)
        {
            _pointfloat.x = pos.x + ( (float)Math.Cos(-angle) * -distance );
            _pointfloat.y = pos.y + ( (float)Math.Sin(-angle) * -distance );
            return _pointfloat;
        }
        public static PointFloat GetDirection(float deg)
        {
            deg = deg + ( PI / 2.0f );
            _pointfloat.x = (float)Math.Cos(deg);
            _pointfloat.y = (float)Math.Sin(deg);
            return _pointfloat;
        }
        public static float GetSpeed(PointFloat vel)
        {
            return (float)Math.Pow(vel.x + vel.y,2);
        }
    }
}
