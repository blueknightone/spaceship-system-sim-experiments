using System;

namespace BlueKnightOne.Utilities
{
    public static class Compare
    {
        private const float FLOAT_EPSILON = 0.00001f;
        public static bool FloatEquality(float a, float b, float epsilon = FLOAT_EPSILON)
        {
            return Math.Abs(a - b) <= epsilon;
        }
    }
}