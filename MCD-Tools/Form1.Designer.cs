namespace MCD_Tools
{
    partial class MainForm
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
            this.lvParam = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btStart = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lvRead = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lbCount = new System.Windows.Forms.Label();
            this.lbTime = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lvParam
            // 
            this.lvParam.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvParam.FullRowSelect = true;
            this.lvParam.GridLines = true;
            this.lvParam.Location = new System.Drawing.Point(12, 12);
            this.lvParam.MultiSelect = false;
            this.lvParam.Name = "lvParam";
            this.lvParam.Size = new System.Drawing.Size(460, 313);
            this.lvParam.TabIndex = 0;
            this.lvParam.UseCompatibleStateImageBehavior = false;
            this.lvParam.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "排序";
            this.columnHeader1.Width = 47;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "姓名";
            this.columnHeader2.Width = 81;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "身份证";
            this.columnHeader3.Width = 159;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "手机号";
            this.columnHeader4.Width = 169;
            // 
            // btStart
            // 
            this.btStart.Location = new System.Drawing.Point(489, 125);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(127, 58);
            this.btStart.TabIndex = 1;
            this.btStart.Text = "启  动";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(489, 34);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(127, 21);
            this.dateTimePicker1.TabIndex = 3;
            // 
            // tbLog
            // 
            this.tbLog.Location = new System.Drawing.Point(12, 331);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.Size = new System.Drawing.Size(614, 265);
            this.tbLog.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(489, 61);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 58);
            this.button1.TabIndex = 5;
            this.button1.Text = "准  备";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lvRead
            // 
            this.lvRead.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.lvRead.FullRowSelect = true;
            this.lvRead.GridLines = true;
            this.lvRead.Location = new System.Drawing.Point(632, 12);
            this.lvRead.MultiSelect = false;
            this.lvRead.Name = "lvRead";
            this.lvRead.Size = new System.Drawing.Size(456, 584);
            this.lvRead.TabIndex = 6;
            this.lvRead.UseCompatibleStateImageBehavior = false;
            this.lvRead.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "排序";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "姓名";
            this.columnHeader6.Width = 93;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "日期";
            this.columnHeader7.Width = 121;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "验证码";
            this.columnHeader8.Width = 169;
            // 
            // lbCount
            // 
            this.lbCount.AutoSize = true;
            this.lbCount.Location = new System.Drawing.Point(493, 268);
            this.lbCount.Name = "lbCount";
            this.lbCount.Size = new System.Drawing.Size(0, 12);
            this.lbCount.TabIndex = 7;
            // 
            // lbTime
            // 
            this.lbTime.AutoSize = true;
            this.lbTime.Location = new System.Drawing.Point(494, 299);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(0, 12);
            this.lbTime.TabIndex = 8;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(489, 189);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(127, 58);
            this.button2.TabIndex = 9;
            this.button2.Text = "测  试";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 604);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.lbTime);
            this.Controls.Add(this.lbCount);
            this.Controls.Add(this.lvRead);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbLog);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.btStart);
            this.Controls.Add(this.lvParam);
            this.Name = "MainForm";
            this.Text = "MCD 20200315";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvParam;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListView lvRead;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Label lbCount;
        private System.Windows.Forms.Label lbTime;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
    }
}

