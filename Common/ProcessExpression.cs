using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Coin51_chia.Common
{

    public static class ProcessExpression
    {

        public static async Task<int> WaitForExitAsync(this Process process, CancellationToken cancellationToken = default(CancellationToken))
        {
            var tcs = new TaskCompletionSource<int>();
            EventHandler exitHandler = (s, e) =>
            {
                tcs.TrySetResult(process.ExitCode);
            };
            try
            {
                process.EnableRaisingEvents = true;
                process.Exited += exitHandler;
                if (process.HasExited)
                {
                    // Allow for the race condition that the process has already exited.
                    tcs.TrySetResult(process.ExitCode);
                }

                using (cancellationToken.Register(() => tcs.TrySetCanceled(cancellationToken)))
                {
                    return await tcs.Task.ConfigureAwait(false);
                }
            }
            finally
            {
                process.Exited -= exitHandler;
            }
        }


        //public async static Task WaitForExitAsync(this Process process, CancellationTokenSource cancellationToken = default(CancellationTokenSource))
        //{


        //    var tcs = new TaskCompletionSource<object>();
        //    process.EnableRaisingEvents = true;
        //    process.Exited += (sender, args) => tcs.TrySetResult(null);
        //    cancellationToken.Token.Register(() => tcs.TrySetCanceled());
        //    await (tcs.Task).ConfigureAwait(false);

        //    //var tcs = new TaskCompletionSource<bool>();
        //    //var Process_Exited = new EventHandler((sender, e) =>
        //    //{
        //    //    Task.Run(() => tcs.TrySetResult(true));
        //    //});
        //    //process.EnableRaisingEvents = true;
        //    //process.Exited += Process_Exited;
        //    //try
        //    //{
        //    //    if (process.HasExited)
        //    //    {
        //    //        return;
        //    //    }
        //    //    cancellationToken.Register(() => Task.Run(
        //    //    () =>
        //    //    {
        //    //        tcs.TrySetCanceled();
        //    //    }));
        //    //    await tcs.Task;
        //    //    Console.WriteLine("等待结束！！");
        //    //}
        //    //finally
        //    //{
        //    //    process.Exited -= Process_Exited;
        //    //}
        //}

    }
}
