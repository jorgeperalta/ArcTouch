using System;
using System.Diagnostics;

namespace ArcTouch.Core.Helpers
{
	public static class ErrorLog
	{
		public static void LogError(string message, Exception ex)
		{
            Logs.Instance.Log(MvvmCross.Logging.MvxLogLevel.Error, () => { return message; }, ex, "{0}. {1}");
		}
    }
}
