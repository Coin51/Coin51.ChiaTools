using Coin51_chia.Common;
using Plaisted.PowershellHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Coin51_chia.Models
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


        /// <summary>
        /// 静态构造增加循环任务更新进度
        /// </summary>
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
                    System.Threading.Thread.Sleep(20000);
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
                //任务开始前检查硬盘空间状态
                _task.BeforeTaskStart += (sender =>
                {
                    return CheckedTaskHardDiskFreeSpace(sender);
                });
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
                //var source = new System.Threading.CancellationTokenSource();
                //using (var helper = new PowershellHelper(LogerHelper.loggerFactory).WithOptions(o => { o.CleanupMethod = CleanupType.RecursiveAdmin; }))
                //{
                //    helper.AddCommand("Get-Process -Name chia | Stop-Process");
                //    var task = Task.Run(() => helper.RunAsync(source.Token));
                //    task.Wait();
                //    helper.WaitOnCleanup();
                //}
            }
            catch(Exception err)
            {
                LogerHelper.logger.Error(err.Message);
            }
         }

        /// <summary>
        /// 当前奇亚支持的P图大小
        /// </summary>
        public static List<Tuple<int, double, double>> kSizeDictionary = new List<Tuple<int, double, double>>()
        {
            Tuple.Create(25,0.6,1.8),
            Tuple.Create(32,101.4,239.0),
            Tuple.Create(33,208.8,521.0),
            Tuple.Create(34,429.8,1041.0),
            Tuple.Create(35,884.1,2175.0)
        };

        /// <summary>
        /// 获取盘符空间
        /// </summary>
        /// <param name="hardDiskName"></param>
        /// <returns></returns>
        private static long GetHardDiskFreeSpace(string hardDiskName)
        {
            var totalFreeSpace = System.IO.DriveInfo.GetDrives().FirstOrDefault(w1 => w1.Name.Equals(hardDiskName + ":\\", StringComparison.CurrentCultureIgnoreCase)).TotalFreeSpace;
            return totalFreeSpace / (1024 * 1024 * 1024);
        }


        /// <summary>
        /// 检查任务开始在硬盘空间上是否合适
        /// </summary>
        /// <param name="poltPath"></param>
        /// <param name="ksize"></param>
        /// <returns></returns>
        public static bool CheckedTaskHardDiskFreeSpace(ChinPoltTask task)
        {
            var code = task.poltConfig.finalPath.Split(':').FirstOrDefault();
            //最终保存目录剩余空间
            var _freeSpace = GetHardDiskFreeSpace(code);
            //当前任务保存需要的空间
            var _needSize = kSizeDictionary.Where(w1 => w1.Item1 == task.poltConfig.stripeSize).FirstOrDefault().Item2;
            //当前在运行的P图需要的空间
            ChinPoltTasks
                .Where(w1 => w1.poltConfig.finalPath.Split(':').FirstOrDefault().Equals(code) && w1.status== TaskStatusEnum.Runing)
                .ToList()
                .ForEach(f1=> 
                {
                    _needSize+= kSizeDictionary.Where(w1 => w1.Item1 == f1.poltConfig.stripeSize).FirstOrDefault().Item2;
                });
            LogerHelper.logger.Info($"任务编号{task.id}当前系统存储空间{_freeSpace}GB,需要空间{_needSize}GB。");
            return _freeSpace > _needSize;
        }

    }
}
