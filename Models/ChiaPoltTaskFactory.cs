using Coin51_chia.Common;
using Plaisted.PowershellHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Coin51_chia.Entity
{
    public static class ChiaPoltTaskFactory
    {

        /// <summary>
        /// 当前系统任务集合
        /// </summary>
        public static List<ChinPoltTask> ChinPoltTasks = new List<ChinPoltTask>();

        /// <summary>
        /// 任务状态变更
        /// </summary>
        public static event Action<object> TaskStatusChangeEvent = null;


        static ChiaPoltTaskFactory()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    ChinPoltTasks.ForEach(f1 =>
                    {
                        if (f1.status == TaskStatusEnum.Runing)
                        {
                            f1.currentProgress = (DateTime.Now - f1.currentStartTime).TotalHours.ToString("0.00") + "/" + (string.IsNullOrEmpty(f1.lastUseTime) ? "--" : f1.lastUseTime);
                        }
                    });
                    System.Threading.Thread.Sleep(5000);
                    CallStatusChangeEvent(null);
                }
            });
        }
        /// <summary>
        /// 任务发生变化
        /// </summary>
        /// <param name="serder"></param>
        public static void CallStatusChangeEvent(object serder)
        {
            TaskStatusChangeEvent?.Invoke(serder);
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="poltConfig"></param>
        /// <param name="chiaSetting"></param>
        /// <returns></returns>
        public static int SaveTask(string id, PoltConfig poltConfig, ChiaSetting chiaSetting)
        {
            ChinPoltTask _task = null;
            if (string.IsNullOrEmpty(id))
            {
                _task = new ChinPoltTask()
                {
                    completeNumber = 0,
                    currentProgress = "--/--",
                    id = Guid.NewGuid().ToString(),
                    poltConfig = poltConfig,
                    chiaSetting = chiaSetting,
                    status = TaskStatusEnum.Stop
                };
                ChinPoltTasks.Add(_task);
            }
            else
            {
                _task = ChinPoltTasks.FirstOrDefault(f1 => f1.id == id);
                _task.Stop();
                _task.chiaSetting = chiaSetting;
                _task.poltConfig = poltConfig;
            }
            Task.Factory.StartNew(() => _task.Start(new System.Threading.CancellationTokenSource()));
            return 0;
        }


        /// <summary>
        /// 关闭进程
        /// </summary>
        public static void StopChiaThread()
        {
            try
            {
                var source = new System.Threading.CancellationTokenSource();
                using (var helper = new PowershellHelper(LogerHelper.loggerFactory).WithOptions(o => { o.CleanupMethod = CleanupType.RecursiveAdmin; }))
                {
                    helper.AddCommand("Get-Process -Name chia | Stop-Process");
                    var task = Task.Run(() => helper.RunAsync(source.Token));
                    task.Wait();
                    helper.WaitOnCleanup();
                }
            }
            catch(Exception err)
            {
                LogerHelper.logger.Error(err.Message);
            }
         }
    }
}
