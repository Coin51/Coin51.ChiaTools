using Coin51_chia.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;

namespace Coin51_chia
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// 当前配置信息
        /// </summary>
        ChiaSetting chiaSetting = null;

        /// <summary>
        /// 构造
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
            chiaSetting = ChiaSetting.LoadConfig();
        }

        /// <summary>
        /// 界面加载（加载配置信息）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e)
        {

            chiaSetting = ChiaSetting.LoadConfig();
            ChiaPoltTaskFactory.StopChiaThread();
            chiaSetting?.poltConfig.ForEach(f1 =>
            {
                ChiaPoltTaskFactory.SaveTask(string.Empty, f1, chiaSetting);
            });
            OnShow();
            this.txt_chiapath.Text = chiaSetting.setupPath;
            this.txt_pf.Text = chiaSetting.farmerPublicKey;
            this.txt_pp.Text = chiaSetting.poolPublicKey;
            ChiaPoltTaskFactory.TaskStatusChangeEvent += (data) => {
                this.Invoke((EventHandler)(delegate
                {
                    OnShow();
                }));
            };
        }

        /// <summary>
        /// 显示数据
        /// </summary>
        private void OnShow()
        {
            this.lv_tasks.Items.Clear();
            for (int i = 0; i < ChiaPoltTaskFactory.ChinPoltTasks.Count; i++)
            {
                var item = ChiaPoltTaskFactory.ChinPoltTasks[i];
                ListViewItem _item = new ListViewItem();
                _item.Text = (i + 1).ToString();
                _item.SubItems.Add(item.status == TaskStatusEnum.Runing ? "执行中" : item.status == TaskStatusEnum.Wait ? "等待" : "停止");
                _item.SubItems.Add(item.currentProgress);
                _item.SubItems.Add(item.completeNumber.ToString());
                _item.SubItems.Add(item.poltConfig.tempPath);
                _item.SubItems.Add(item.poltConfig.finalPath);
                _item.SubItems.Add(item.poltConfig.memorySize.ToString());
                _item.SubItems.Add(item.poltConfig.threadNumber.ToString());
                _item.SubItems.Add(item.poltConfig.stripeSize.ToString());
                _item.SubItems.Add(item.poltConfig.bucketsNumber.ToString());
                _item.SubItems.Add(item.poltConfig.isBitfieldPlotting?"是":"否");
                _item.SubItems.Add(item.poltConfig.isKeepWorking ? "是" : "否");
                _item.Tag = item;
                this.lv_tasks.Items.Add(_item);
            }
        }

        /// <summary>
        /// 保存或修改任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_save_Click(object sender, EventArgs e)
        {
            var _TaskConfig = this.but_save.Tag as ChinPoltTask;
            if (_TaskConfig == null)
            {
                var _poltConfig = new PoltConfig()
                {
                    bucketsNumber = Convert.ToInt32(this.txt_buckets.Text),
                    finalPath = this.txt_polt.Text,
                    tempPath = this.txt_temp.Text,
                    memorySize = Convert.ToInt32(this.txt_memory.Text),
                    isBitfieldPlotting = this.cb_isnobyte.Checked,
                    stripeSize = Convert.ToInt32(this.txt_k.Text),
                    threadNumber = Convert.ToInt32(this.txt_thread.Text),
                    isKeepWorking = this.cb_keepWorking.Checked,
                    id = Guid.NewGuid().ToString()
                };
                chiaSetting.poltConfig.Add(_poltConfig);
                ChiaPoltTaskFactory.SaveTask(string.Empty, _poltConfig, chiaSetting);
            }
            else
            {
                _TaskConfig.poltConfig.bucketsNumber = Convert.ToInt32(this.txt_buckets.Text);
                _TaskConfig.poltConfig.finalPath = this.txt_polt.Text;
                _TaskConfig.poltConfig.tempPath = this.txt_temp.Text;
                _TaskConfig.poltConfig.memorySize = Convert.ToInt32(this.txt_memory.Text);
                _TaskConfig.poltConfig.isBitfieldPlotting = this.cb_isnobyte.Checked;
                _TaskConfig.poltConfig.stripeSize = Convert.ToInt32(this.txt_k.Text);
                _TaskConfig.poltConfig.threadNumber = Convert.ToInt32(this.txt_thread.Text);
                _TaskConfig.poltConfig.isKeepWorking = this.cb_keepWorking.Checked;
                ChiaPoltTaskFactory.SaveTask(_TaskConfig.id, _TaskConfig.poltConfig, chiaSetting);
            }
            OnShow();
        }

        /// <summary>
        /// 关闭时保存基础设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            ChiaSetting.SaveConfig(chiaSetting);
            bool isRuning = ChiaPoltTaskFactory.ChinPoltTasks.Any(a1 => a1.status == TaskStatusEnum.Runing);
            ChiaPoltTaskFactory.ChinPoltTasks?.ForEach(f1 => f1.Stop());
            if (isRuning)
            {
                Task.Factory.StartNew(() =>
                {
                    do
                    {
                        System.Threading.Thread.Sleep(1500);
                    }
                    while (ChiaPoltTaskFactory.ChinPoltTasks.Any(a1 => a1.status == TaskStatusEnum.Runing));
                    Application.Exit();
                });
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 处理修改应用新配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_access_Click(object sender, EventArgs e)
        {
            chiaSetting.farmerPublicKey = this.txt_pf.Text;
            chiaSetting.poolPublicKey = this.txt_pp.Text;
            TasksReflash();
            OnShow();
        }

        /// <summary>
        /// 修改指定的任务配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsm_edit_Click(object sender, EventArgs e)
        {
            var _selrctConfig = (ChinPoltTask)this.lv_tasks.SelectedItems[0].Tag;
            this.txt_temp.Text = _selrctConfig.poltConfig.tempPath;
            this.txt_polt.Text = _selrctConfig.poltConfig.finalPath;
            this.txt_memory.Text = _selrctConfig.poltConfig.memorySize.ToString();
            this.txt_thread.Text = _selrctConfig.poltConfig.threadNumber.ToString();
            this.txt_k.Text = _selrctConfig.poltConfig.stripeSize.ToString();
            this.txt_buckets.Text = _selrctConfig.poltConfig.bucketsNumber.ToString();
            this.cb_isnobyte.Checked = _selrctConfig.poltConfig.isBitfieldPlotting;
            this.cb_keepWorking.Checked = _selrctConfig.poltConfig.isKeepWorking;
            this.but_save.Tag = _selrctConfig;
        }

        /// <summary>
        /// 选择程序目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_chiaPath_Click(object sender, EventArgs e)
        {
            //选择文件
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件夹";
            dialog.Filter = "执行程序(*.exe)|*.exe";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string file = dialog.FileName;
                this.txt_chiapath.Text = file;
                chiaSetting.setupPath = file;
                TasksReflash();
                OnShow();
            }
        }

        /// <summary>
        /// 任务重启
        /// </summary>
        private void TasksReflash()
        {
            ChiaPoltTaskFactory.ChinPoltTasks?.ForEach(f1 => f1.Stop());
            ChiaPoltTaskFactory.ChinPoltTasks?.ForEach(f1 => Task.Factory.StartNew(() => f1.Start(new System.Threading.CancellationTokenSource())));
        }

        /// <summary>
        /// 保存零食盘地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_tempPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            //dialog.SelectedPath = path;
            //dialog.RootFolder = Environment.SpecialFolder.Programs;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath;
                this.txt_temp.Text = foldPath;
            }
        }

        /// <summary>
        /// 保存存储盘地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_finalPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            //dialog.SelectedPath = path;
            //dialog.RootFolder = Environment.SpecialFolder.Programs;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath;
                this.txt_polt.Text = foldPath;
            }
        }

        /// <summary>
        /// 停止处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsm_stop_Click(object sender, EventArgs e)
        {
            var _selrctConfig = (ChinPoltTask)this.lv_tasks.SelectedItems[0].Tag;
            _selrctConfig?.Stop();
        }

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsm_del_Click(object sender, EventArgs e)
        {
            var _selrctConfig = (ChinPoltTask)this.lv_tasks.SelectedItems[0].Tag;
            chiaSetting.poltConfig.RemoveAll(r1 => r1.id == _selrctConfig.poltConfig.id);
            ChiaPoltTaskFactory.ChinPoltTasks.Remove(_selrctConfig);
        }

        /// <summary>
        /// 开始工作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsm_start_Click(object sender, EventArgs e)
        {
            var _selrctConfig = (ChinPoltTask)this.lv_tasks.SelectedItems[0].Tag;
            Task.Factory.StartNew(() => _selrctConfig.Start(new System.Threading.CancellationTokenSource()));
        }

    }
}
