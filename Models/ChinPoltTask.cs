using Coin51_chia.Common;
using Plaisted.PowershellHelper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Coin51_chia.Models
{

    /// <summary>
    /// 任务信息
    /// </summary>
    public class ChinPoltTask
    {
        /// <summary>
        /// 任务编号
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 当前任务状态
        /// </summary>
        public TaskStatusEnum status { get; set; }

        /// <summary>
        /// 当前任务进度
        /// </summary>
        public string currentProgress { get; set; }

        /// <summary>
        /// 当前开始时间
        /// </summary>
        public DateTime currentStartTime { get; set; }

        /// <summary>
        /// 最后花费时间
        /// </summary>
        public string lastUseTime { get; set; }

        /// <summary>
        /// 任务完成数量
        /// </summary>
        public int completeNumber { get; set; }

        /// <summary>
        /// P图配置信息
        /// </summary>
        public PoltConfig poltConfig { get; set; }

        /// <summary>
        /// 基本配置信息
        /// </summary>
        public ChiaSetting chiaSetting { get; set; }

        /// <summary>
        /// 当前上下文处理标记
        /// </summary>
        public CancellationTokenSource source { get; set; }

        /// <summary>
        /// 任务执行开始前
        /// </summary>
        public event Func<ChinPoltTask, bool> BeforeTaskStart = null; 

        /// <summary>
        /// 开始执行处理
        /// </summary>
        /// <param name="setting"></param>
        public async Task StartByPS(CancellationTokenSource _source)
        {
            if (BeforeTaskStart.Invoke(this))
            {
                source = _source;
                var poltCommend = $"{chiaSetting.setupPath} plots create -k {poltConfig.stripeSize} -b {poltConfig.memorySize} -t {poltConfig.tempPath} -d {poltConfig.finalPath} -f {chiaSetting.farmerPublicKey} -p {chiaSetting.poolPublicKey}";
                if (poltConfig.isBitfieldPlotting)
                {
                    poltCommend = poltCommend + " -e";
                }
                if (poltConfig.stripeSize < 32)
                {
                    poltCommend = poltCommend + " --override-k";
                }
                using (var helper = new PowershellHelper(LogerHelper.loggerFactory).WithOptions(o => { o.CleanupMethod = CleanupType.Recursive; }))
                {
                    var stopwatch = new Stopwatch();
                    stopwatch.Start();
                    this.currentStartTime = DateTime.Now;
                    helper.AddCommand(poltCommend);
                    this.status = TaskStatusEnum.Runing;
                    ChiaPoltTaskFactory.CallStatusChangeEvent(this);
                    LogerHelper.logger.Info($"任务编号【{this.id}】指令开始执行Powershell指令【{poltCommend}】！！");
                    var result = await Task.Run(() => helper.RunAsync(source.Token));
                    this.status = TaskStatusEnum.Wait;
                    if (result == PowershellStatus.Exited)
                    {
                        stopwatch.Stop();
                        var useTime = TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds);
                        this.lastUseTime = useTime.TotalHours.ToString("0.00");
                        this.completeNumber++;
                        LogerHelper.logger.Info($"任务编号【{this.id}】指令执行完成！");
                    }
                    else
                    {
                        LogerHelper.logger.Error($"任务编号【{this.id}】指令异常终止退出码【{helper.ExitCode}】");
                    }
                    helper.WaitOnCleanup();
                }
                ChiaPoltTaskFactory.CallStatusChangeEvent(this);
                if (poltConfig.isKeepWorking && !source.IsCancellationRequested)
                {
                    await StartByPS(source);
                }
                else
                {
                    this.status = TaskStatusEnum.Stop;
                    ChiaPoltTaskFactory.CallStatusChangeEvent(this);
                }
            }
            else
            {
                this.status = TaskStatusEnum.Stop;
                ChiaPoltTaskFactory.CallStatusChangeEvent(this);
            }
        }

        public async Task Start(CancellationTokenSource _source)
        {
            if (BeforeTaskStart.Invoke(this))
            {
                source = _source;
                string processPath = chiaSetting.setupPath;
                List<string> arguments = new List<string>()
                {
                    "plots",
                    "create",
                    "-k",poltConfig.stripeSize.ToString(),
                    "-b",poltConfig.memorySize.ToString(),
                    "-t",poltConfig.tempPath,
                    "-d",poltConfig.finalPath,
                    "-f",chiaSetting.farmerPublicKey,
                    "-p",chiaSetting.poolPublicKey
                };
                if (poltConfig.isBitfieldPlotting)
                {
                    arguments.Add("-e");
                }
                if (poltConfig.stripeSize < 32)
                {
                    arguments.Add("--override-k");
                }

                var executor = new ProcessExecutor(processPath)
                {
                    Args = arguments.ToArray(),
                    StdoutHandler = (sender, e) => { LogerHelper.logger.Info(e.Data); },
                    StderrHandler = (sender, e) => { LogerHelper.logger.Info(e.Data); },
                };
                executor.Mode = ProcessExecutor.RedirectionMode.UseHandlers;
                

                #region 开始处理标记
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                this.currentStartTime = DateTime.Now;
                this.status = TaskStatusEnum.Runing;
                ChiaPoltTaskFactory.CallStatusChangeEvent(this);
                LogerHelper.logger.Info($"任务编号【{this.id}】指令开始执行CHIA指令【{ string.Join(" ", arguments) }】！！");
                #endregion

                var process = executor.Execute();
                try
                {
                    await process.WaitForExitAsync(source.Token);
                    stopwatch.Stop();
                    var useTime = TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds);
                    this.lastUseTime = useTime.TotalHours.ToString("0.00");
                    this.completeNumber++;
                    LogerHelper.logger.Info($"任务编号【{this.id}】指令执行完成！");
                }
                catch (Exception)
                {
                    stopwatch.Stop();
                    if (!process.HasExited)
                    {
                        process.Kill();
                    }
                    LogerHelper.logger.Error($"任务编号【{this.id}】指令异常终止退出码【{process.ExitCode}】");
                }

                ChiaPoltTaskFactory.CallStatusChangeEvent(this);
                if (poltConfig.isKeepWorking && !source.IsCancellationRequested)
                {
                    await Start(source);
                }
                else
                {
                    this.status = TaskStatusEnum.Stop;
                    ChiaPoltTaskFactory.CallStatusChangeEvent(this);
                }
            }
            else
            {
                this.status = TaskStatusEnum.Stop;
                ChiaPoltTaskFactory.CallStatusChangeEvent(this);
            }
        }

        /// <summary>
        /// 停止运行
        /// </summary>
        /// <param name="setting"></param>
        public void Stop()
        {
            source?.Cancel();
        }

    }



    /// <summary>
    /// P图基础配置
    /// </summary>
    public class PoltConfig
    {
        /// <summary>
        /// 配置编号
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 定义P盘时的临时目录
        /// </summary>
        public string tempPath { get; set; }

        /// <summary>
        /// 定义存储plot文件的最终位置
        /// </summary>
        public string finalPath { get; set; }

        /// <summary>
        /// 内存大小
        /// </summary>
        public int memorySize { get; set; }

        /// <summary>
        /// 线程数
        /// </summary>
        public int threadNumber { get; set; }

        /// <summary>
        /// K
        /// </summary>
        public int stripeSize { get; set; }

        /// <summary>
        /// 桶数量
        /// </summary>
        public int bucketsNumber { get; set; }

        /// <summary>
        /// 是否位域
        /// </summary>
        public bool isBitfieldPlotting { get; set; }


        /// <summary>
        /// 是否持续执行
        /// </summary>
        public bool isKeepWorking { get; set; }
    }

    /// <summary>
    /// 任务状态
    /// </summary>
    public enum TaskStatusEnum
    {
        Runing,
        Wait,
        Stop
    }
}
