﻿using System;
using System.Threading.Tasks;
#if !FDROID
using Microsoft.AppCenter.Crashes;
#endif

namespace Bit.Core.Utilities
{
    public static class TaskExtensions
    {
        /// <summary>
        /// Fires a task and ignores any exception.
        /// See http://stackoverflow.com/a/22864616/344182
        /// </summary>
        /// <param name="task">The task to be forgotten.</param>
        /// <param name="onException">Action to be called on exception.</param>
        public static async void FireAndForget(this Task task, Action<Exception> onException = null)
        {
            try
            {
                await task.ConfigureAwait(false);
            }
            catch (Exception ex)
            {
#if !FDROID
                Crashes.TrackError(ex);
#endif
                onException?.Invoke(ex);
            }
        }
    }
}
