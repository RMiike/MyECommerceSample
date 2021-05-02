using System;

namespace MECS.Core.Helpers
{
    public static class GeneralExtensions
    {
        public static long ToUnixEpochDate(this DateTime date)
         => (long) Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
