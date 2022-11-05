using System;
using Microsoft.Extensions.Logging;

namespace Common.Bll.Helpers
{
    public static class Extensions
    {
        public static void LogError(this ILogger logger, Exception e)
        {
            logger.LogError(e.Message, e);
        }
    }
}