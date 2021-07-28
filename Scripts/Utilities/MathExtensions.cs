/*MathExtensions.cs (c) 2021
Author: Justin Abbott (lastmilegames@gmail.com)
Desc: Contains useful math functions that are not part of the standard System.Math library.
Created:  2021-07-28T16:03:19.563Z
Modified: 2021-07-28T16:10:20.104Z
*/

using System;

namespace BlueKnightOne.Utilities
{
    public static class MathExtenstions
    {
        private const float FLOAT_EPSILON = 0.00001f;
        
        /// <summary>
        ///     Determines if two floats are within a given threshold of equality.
        /// </summary>
        /// <param name="a">Base float.</param>
        /// <param name="b">Float to compare.</param>
        /// <param name="epsilon">Equality tolerance (default 0.00001).</param>
        /// <returns>Returns true if floats are within the tolerance threshold for equality.</returns>
        public static bool Approximately(this float a, float b, float epsilon = FLOAT_EPSILON)
        {
            return Math.Abs(a - b) <= epsilon;
        }
    }
}