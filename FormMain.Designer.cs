namespace Coin51_chia
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cb_keepWorking = new System.Windows.Forms.CheckBox();
            this.cb_isnobyte = new System.Windows.Forms.CheckBox();
            this.but_save = new System.Windows.Forms.Button();
            this.txt_buckets = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txt_k = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_thread = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_memory = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.but_finalPath = new System.Windows.Forms.Button();
            this.txt_polt = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.but_tempPath = new System.Windows.Forms.Button();
            this.txt_temp = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cms_tasks = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsm_start = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_edit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_stop = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_del = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.but_access = new System.Windows.Forms.Button();
            this.txt_pp = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_pf = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_chiapath = new System.Windows.Forms.TextBox();
            this.but_chiaPath = new System.Windows.Forms.Button();
            this.lv_tasks = new Coin51_chia.Common.ListViewEx();
            this.id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_poss = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_complate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_temp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_file = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_meny = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_thread = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_k = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_buckets = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_nobyte = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_keep = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TasksBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox2.SuspendLayout();
            this.cms_tasks.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TasksBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cb_keepWorking);
            this.groupBox2.Controls.Add(this.cb_isnobyte);
            this.groupBox2.Controls.Add(this.but_save);
            this.groupBox2.Controls.Add(this.txt_buckets);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.txt_k);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txt_thread);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txt_memory);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.but_finalPath);
            this.groupBox2.Controls.Add(this.txt_polt);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.but_tempPath);
            this.groupBox2.Controls.Add(this.txt_temp);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(10, 301);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(630, 157);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "耕种任务";
            // 
            // cb_keepWorking
            // 
            this.cb_keepWorking.AutoSize = true;
            this.cb_keepWorking.Checked = true;
            this.cb_keepWorking.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_keepWorking.Location = new System.Drawing.Point(190, 123);
            this.cb_keepWorking.Name = "cb_keepWorking";
            this.cb_keepWorking.Size = new System.Drawing.Size(72, 16);
            this.cb_keepWorking.TabIndex = 22;
            this.cb_keepWorking.Text = "是否连续";
            this.cb_keepWorking.UseVisualStyleBackColor = true;
            // 
            // cb_isnobyte
            // 
            this.cb_isnobyte.AutoSize = true;
            this.cb_isnobyte.Location = new System.Drawing.Point(78, 123);
            this.cb_isnobyte.Name = "cb_isnobyte";
            this.cb_isnobyte.Size = new System.Drawing.Size(90, 16);
            this.cb_isnobyte.TabIndex = 21;
            this.cb_isnobyte.Text = "禁用位域P图";
            this.cb_isnobyte.UseVisualStyleBackColor = true;
            // 
            // but_save
            // 
            this.but_save.Location = new System.Drawing.Point(489, 119);
            this.but_save.Name = "but_save";
            this.but_save.Size = new System.Drawing.Size(120, 23);
            this.but_save.TabIndex = 20;
            this.but_save.Text = "添加/修改任务";
            this.but_save.UseVisualStyleBackColor = true;
            this.but_save.Click += new System.EventHandler(this.but_save_Click);
            // 
            // txt_buckets
            // 
            this.txt_buckets.Location = new System.Drawing.Point(534, 82);
            this.txt_buckets.Name = "txt_buckets";
            this.txt_buckets.Size = new System.Drawing.Size(75, 21);
            this.txt_buckets.TabIndex = 19;
            this.txt_buckets.Text = "128";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(488, 86);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 12);
            this.label12.TabIndex = 18;
            this.label12.Text = "桶大小";
            // 
            // txt_k
            // 
            this.txt_k.Location = new System.Drawing.Point(382, 82);
            this.txt_k.Name = "txt_k";
            this.txt_k.Size = new System.Drawing.Size(75, 21);
            this.txt_k.TabIndex = 17;
            this.txt_k.Text = "32";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(366, 86);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(11, 12);
            this.label11.TabIndex = 16;
            this.label11.Text = "K";
            // 
            // txt_thread
            // 
            this.txt_thread.Location = new System.Drawing.Point(230, 82);
            this.txt_thread.Name = "txt_thread";
            this.txt_thread.Size = new System.Drawing.Size(75, 21);
            this.txt_thread.TabIndex = 15;
            this.txt_thread.Text = "2";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(199, 86);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 14;
            this.label10.Text = "线程";
            // 
            // txt_memory
            // 
            this.txt_memory.Location = new System.Drawing.Point(78, 82);
            this.txt_memory.Name = "txt_memory";
            this.txt_memory.Size = new System.Drawing.Size(75, 21);
            this.txt_memory.TabIndex = 12;
            this.txt_memory.Text = "4096";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(41, 82);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 11;
            this.label8.Text = "内存";
            // 
            // but_finalPath
            // 
            this.but_finalPath.Location = new System.Drawing.Point(549, 51);
            this.but_finalPath.Name = "but_finalPath";
            this.but_finalPath.Size = new System.Drawing.Size(61, 23);
            this.but_finalPath.TabIndex = 10;
            this.but_finalPath.Text = "选择";
            this.but_finalPath.UseVisualStyleBackColor = true;
            this.but_finalPath.Click += new System.EventHandler(this.but_finalPath_Click);
            // 
            // txt_polt
            // 
            this.txt_polt.Location = new System.Drawing.Point(78, 51);
            this.txt_polt.Name = "txt_polt";
            this.txt_polt.ReadOnly = true;
            this.txt_polt.Size = new System.Drawing.Size(465, 21);
            this.txt_polt.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 8;
            this.label7.Text = "目标路径";
            // 
            // but_tempPath
            // 
            this.but_tempPath.Location = new System.Drawing.Point(549, 23);
            this.but_tempPath.Name = "but_tempPath";
            this.but_tempPath.Size = new System.Drawing.Size(61, 23);
            this.but_tempPath.TabIndex = 7;
            this.but_tempPath.Text = "选择";
            this.but_tempPath.UseVisualStyleBackColor = true;
            this.but_tempPath.Click += new System.EventHandler(this.but_tempPath_Click);
            // 
            // txt_temp
            // 
            this.txt_temp.Location = new System.Drawing.Point(78, 23);
            this.txt_temp.Name = "txt_temp";
            this.txt_temp.ReadOnly = true;
            this.txt_temp.Size = new System.Drawing.Size(465, 21);
            this.txt_temp.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "缓存路径";
            // 
            // cms_tasks
            // 
            this.cms_tasks.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsm_start,
            this.tsm_edit,
            this.tsm_stop,
            this.tsm_del});
            this.cms_tasks.Name = "cms_tasks";
            this.cms_tasks.Size = new System.Drawing.Size(101, 92);
            // 
            // tsm_start
            // 
            this.tsm_start.Name = "tsm_start";
            this.tsm_start.Size = new System.Drawing.Size(100, 22);
            this.tsm_start.Text = "开始";
            this.tsm_start.Click += new System.EventHandler(this.tsm_start_Click);
            // 
            // tsm_edit
            // 
            this.tsm_edit.Name = "tsm_edit";
            this.tsm_edit.Size = new System.Drawing.Size(100, 22);
            this.tsm_edit.Text = "修改";
            this.tsm_edit.Click += new System.EventHandler(this.tsm_edit_Click);
            // 
            // tsm_stop
            // 
            this.tsm_stop.Name = "tsm_stop";
            this.tsm_stop.Size = new System.Drawing.Size(100, 22);
            this.tsm_stop.Text = "停止";
            this.tsm_stop.Click += new System.EventHandler(this.tsm_stop_Click);
            // 
            // tsm_del
            // 
            this.tsm_del.Name = "tsm_del";
            this.tsm_del.Size = new System.Drawing.Size(100, 22);
            this.tsm_del.Text = "删除";
            this.tsm_del.Click += new System.EventHandler(this.tsm_del_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.but_access);
            this.groupBox1.Controls.Add(this.txt_pp);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txt_pf);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(12, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(628, 82);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基础信息";
            // 
            // but_access
            // 
            this.but_access.Location = new System.Drawing.Point(533, 49);
            this.but_access.Name = "but_access";
            this.but_access.Size = new System.Drawing.Size(84, 23);
            this.but_access.TabIndex = 9;
            this.but_access.Text = "应用修改";
            this.but_access.UseVisualStyleBackColor = true;
            this.but_access.Click += new System.EventHandler(this.but_access_Click);
            // 
            // txt_pp
            // 
            this.txt_pp.Location = new System.Drawing.Point(79, 49);
            this.txt_pp.Name = "txt_pp";
            this.txt_pp.Size = new System.Drawing.Size(434, 21);
            this.txt_pp.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "矿池公匙";
            // 
            // txt_pf
            // 
            this.txt_pf.Location = new System.Drawing.Point(79, 19);
            this.txt_pf.Name = "txt_pf";
            this.txt_pf.Size = new System.Drawing.Size(434, 21);
            this.txt_pf.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "矿工公匙";
            // 
            // txt_chiapath
            // 
            this.txt_chiapath.Location = new System.Drawing.Point(12, 11);
            this.txt_chiapath.Name = "txt_chiapath";
            this.txt_chiapath.ReadOnly = true;
            this.txt_chiapath.Size = new System.Drawing.Size(527, 21);
            this.txt_chiapath.TabIndex = 5;
            // 
            // but_chiaPath
            // 
            this.but_chiaPath.Location = new System.Drawing.Point(544, 10);
            this.but_chiaPath.Name = "but_chiaPath";
            this.but_chiaPath.Size = new System.Drawing.Size(96, 23);
            this.but_chiaPath.TabIndex = 6;
            this.but_chiaPath.Text = "chia.exe 目录";
            this.but_chiaPath.UseVisualStyleBackColor = true;
            this.but_chiaPath.Click += new System.EventHandler(this.but_chiaPath_Click);
            // 
            // lv_tasks
            // 
            this.lv_tasks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.id,
            this.ch_status,
            this.ch_poss,
            this.ch_complate,
            this.ch_temp,
            this.ch_file,
            this.ch_meny,
            this.ch_thread,
            this.ch_k,
            this.ch_buckets,
            this.ch_nobyte,
            this.ch_keep});
            this.lv_tasks.ContextMenuStrip = this.cms_tasks;
            this.lv_tasks.FullRowSelect = true;
            this.lv_tasks.GridLines = true;
            this.lv_tasks.Location = new System.Drawing.Point(12, 132);
            this.lv_tasks.Name = "lv_tasks";
            this.lv_tasks.Size = new System.Drawing.Size(628, 161);
            this.lv_tasks.TabIndex = 2;
            this.lv_tasks.UseCompatibleStateImageBehavior = false;
            this.lv_tasks.View = System.Windows.Forms.View.Details;
            // 
            // id
            // 
            this.id.Text = "序号";
            this.id.Width = 50;
            // 
            // ch_status
            // 
            this.ch_status.Text = "状态";
            // 
            // ch_poss
            // 
            this.ch_poss.Text = "进度";
            // 
            // ch_complate
            // 
            this.ch_complate.Text = "完成数";
            this.ch_complate.Width = 50;
            // 
            // ch_temp
            // 
            this.ch_temp.Text = "缓存路径";
            // 
            // ch_file
            // 
            this.ch_file.Text = "目标路径";
            // 
            // ch_meny
            // 
            this.ch_meny.Text = "内存";
            this.ch_meny.Width = 50;
            // 
            // ch_thread
            // 
            this.ch_thread.Text = "线程";
            this.ch_thread.Width = 40;
            // 
            // ch_k
            // 
            this.ch_k.Text = "K";
            this.ch_k.Width = 40;
            // 
            // ch_buckets
            // 
            this.ch_buckets.Text = "桶大小";
            this.ch_buckets.Width = 50;
            // 
            // ch_nobyte
            // 
            this.ch_nobyte.Text = "禁用域";
            this.ch_nobyte.Width = 50;
            // 
            // ch_keep
            // 
            this.ch_keep.Text = "连续";
            this.ch_keep.Width = 40;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 469);
            this.Controls.Add(this.but_chiaPath);
            this.Controls.Add(this.txt_chiapath);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lv_tasks);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(668, 470);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "币无忧奇亚耕地工具（coin51.cn）";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.cms_tasks.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TasksBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button but_tempPath;
        private System.Windows.Forms.TextBox txt_temp;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button but_save;
        private System.Windows.Forms.TextBox txt_buckets;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txt_k;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_thread;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_memory;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button but_finalPath;
        private System.Windows.Forms.TextBox txt_polt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox cb_keepWorking;
        private System.Windows.Forms.CheckBox cb_isnobyte;
        private Common.ListViewEx lv_tasks;
        private System.Windows.Forms.ColumnHeader id;
        private System.Windows.Forms.ColumnHeader ch_status;
        private System.Windows.Forms.ColumnHeader ch_temp;
        private System.Windows.Forms.ColumnHeader ch_file;
        private System.Windows.Forms.ColumnHeader ch_meny;
        private System.Windows.Forms.ColumnHeader ch_thread;
        private System.Windows.Forms.ColumnHeader ch_k;
        private System.Windows.Forms.ColumnHeader ch_buckets;
        private System.Windows.Forms.ColumnHeader ch_nobyte;
        private System.Windows.Forms.ColumnHeader ch_keep;
        private System.Windows.Forms.ColumnHeader ch_poss;
        private System.Windows.Forms.ColumnHeader ch_complate;
        private System.Windows.Forms.ContextMenuStrip cms_tasks;
        private System.Windows.Forms.ToolStripMenuItem tsm_edit;
        private System.Windows.Forms.ToolStripMenuItem tsm_stop;
        private System.Windows.Forms.ToolStripMenuItem tsm_del;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_pp;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_pf;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_chiapath;
        private System.Windows.Forms.Button but_chiaPath;
        private System.Windows.Forms.Button but_access;
        private System.Windows.Forms.ToolStripMenuItem tsm_start;
        private System.Windows.Forms.BindingSource TasksBindingSource;
    }
}

