using System;

namespace XlsxSpawnCSCode
{
    public static class RandomHelper
    {
        public static Random _R = new Random();

        public static int Next(int min, int max)
        {
            return _R.Next(min, max);
        }
    }
}