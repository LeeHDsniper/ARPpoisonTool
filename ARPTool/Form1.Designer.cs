namespace ARPTool
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.Host_ListBox = new System.Windows.Forms.ListBox();
            this.Output_Textbox = new System.Windows.Forms.TextBox();
            this.targetIP_lable = new System.Windows.Forms.Label();
            this.Start_Btn = new System.Windows.Forms.Button();
            this.Stop_Btn = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.Device_checkboxlist = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.IpAddress_textBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Mask_textBox = new System.Windows.Forms.TextBox();
            this.StartSniffer_Btn = new System.Windows.Forms.Button();
            this.StopSniffer_Btn = new System.Windows.Forms.Button();
            this.targetMac_lable = new System.Windows.Forms.Label();
            this.gatewayIP_lable = new System.Windows.Forms.Label();
            this.gatewayMac_lable = new System.Windows.Forms.Label();
            this.targetIP_textBox = new System.Windows.Forms.TextBox();
            this.targetMac_textBox = new System.Windows.Forms.TextBox();
            this.gatewayIP_textBox = new System.Windows.Forms.TextBox();
            this.gatewayMac_textBox = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.fliter_lable = new System.Windows.Forms.Label();
            this.Fliter_textBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Host_ListBox
            // 
            this.Host_ListBox.FormattingEnabled = true;
            this.Host_ListBox.ItemHeight = 12;
            this.Host_ListBox.Location = new System.Drawing.Point(44, 263);
            this.Host_ListBox.Name = "Host_ListBox";
            this.Host_ListBox.Size = new System.Drawing.Size(210, 256);
            this.Host_ListBox.TabIndex = 1;
            this.Host_ListBox.SelectedIndexChanged += new System.EventHandler(this.Host_ListBox_SelectedIndexChanged);
            // 
            // Output_Textbox
            // 
            this.Output_Textbox.Enabled = false;
            this.Output_Textbox.Location = new System.Drawing.Point(308, 158);
            this.Output_Textbox.Multiline = true;
            this.Output_Textbox.Name = "Output_Textbox";
            this.Output_Textbox.Size = new System.Drawing.Size(596, 361);
            this.Output_Textbox.TabIndex = 2;
            // 
            // targetIP_lable
            // 
            this.targetIP_lable.AutoSize = true;
            this.targetIP_lable.Font = new System.Drawing.Font("宋体", 10F);
            this.targetIP_lable.Location = new System.Drawing.Point(305, 25);
            this.targetIP_lable.Name = "targetIP_lable";
            this.targetIP_lable.Size = new System.Drawing.Size(49, 14);
            this.targetIP_lable.TabIndex = 3;
            this.targetIP_lable.Text = "目标IP";
            // 
            // Start_Btn
            // 
            this.Start_Btn.Location = new System.Drawing.Point(739, 100);
            this.Start_Btn.Name = "Start_Btn";
            this.Start_Btn.Size = new System.Drawing.Size(75, 23);
            this.Start_Btn.TabIndex = 4;
            this.Start_Btn.Text = "启动";
            this.Start_Btn.UseVisualStyleBackColor = true;
            this.Start_Btn.Click += new System.EventHandler(this.Start_Btn_Click);
            // 
            // Stop_Btn
            // 
            this.Stop_Btn.Location = new System.Drawing.Point(829, 100);
            this.Stop_Btn.Name = "Stop_Btn";
            this.Stop_Btn.Size = new System.Drawing.Size(75, 23);
            this.Stop_Btn.TabIndex = 5;
            this.Stop_Btn.Text = "停止";
            this.Stop_Btn.UseVisualStyleBackColor = true;
            this.Stop_Btn.Click += new System.EventHandler(this.Stop_Btn_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(740, 526);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(113, 12);
            this.linkLabel1.TabIndex = 6;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "点击这里查看文件夹";
            // 
            // Device_checkboxlist
            // 
            this.Device_checkboxlist.CheckOnClick = true;
            this.Device_checkboxlist.FormattingEnabled = true;
            this.Device_checkboxlist.Location = new System.Drawing.Point(44, 25);
            this.Device_checkboxlist.Margin = new System.Windows.Forms.Padding(0);
            this.Device_checkboxlist.Name = "Device_checkboxlist";
            this.Device_checkboxlist.Size = new System.Drawing.Size(210, 132);
            this.Device_checkboxlist.TabIndex = 0;
            this.Device_checkboxlist.SelectedIndexChanged += new System.EventHandler(this.Device_checkboxlist_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 185);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "本机IP";
            // 
            // IpAddress_textBox
            // 
            this.IpAddress_textBox.Location = new System.Drawing.Point(92, 182);
            this.IpAddress_textBox.Name = "IpAddress_textBox";
            this.IpAddress_textBox.Size = new System.Drawing.Size(162, 21);
            this.IpAddress_textBox.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 212);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "掩码";
            // 
            // Mask_textBox
            // 
            this.Mask_textBox.Location = new System.Drawing.Point(92, 209);
            this.Mask_textBox.Name = "Mask_textBox";
            this.Mask_textBox.Size = new System.Drawing.Size(162, 21);
            this.Mask_textBox.TabIndex = 10;
            // 
            // StartSniffer_Btn
            // 
            this.StartSniffer_Btn.Location = new System.Drawing.Point(92, 236);
            this.StartSniffer_Btn.Name = "StartSniffer_Btn";
            this.StartSniffer_Btn.Size = new System.Drawing.Size(75, 23);
            this.StartSniffer_Btn.TabIndex = 11;
            this.StartSniffer_Btn.Text = "UDP嗅探";
            this.StartSniffer_Btn.UseVisualStyleBackColor = true;
            this.StartSniffer_Btn.Click += new System.EventHandler(this.StartSniffer_Btn_Click);
            // 
            // StopSniffer_Btn
            // 
            this.StopSniffer_Btn.Location = new System.Drawing.Point(173, 236);
            this.StopSniffer_Btn.Name = "StopSniffer_Btn";
            this.StopSniffer_Btn.Size = new System.Drawing.Size(75, 23);
            this.StopSniffer_Btn.TabIndex = 12;
            this.StopSniffer_Btn.Text = "停止嗅探";
            this.StopSniffer_Btn.UseVisualStyleBackColor = true;
            this.StopSniffer_Btn.Click += new System.EventHandler(this.StopSniffer_Btn_Click);
            // 
            // targetMac_lable
            // 
            this.targetMac_lable.AutoSize = true;
            this.targetMac_lable.Font = new System.Drawing.Font("宋体", 10F);
            this.targetMac_lable.Location = new System.Drawing.Point(305, 57);
            this.targetMac_lable.Name = "targetMac_lable";
            this.targetMac_lable.Size = new System.Drawing.Size(56, 14);
            this.targetMac_lable.TabIndex = 13;
            this.targetMac_lable.Text = "目标MAC";
            // 
            // gatewayIP_lable
            // 
            this.gatewayIP_lable.AutoSize = true;
            this.gatewayIP_lable.Font = new System.Drawing.Font("宋体", 10F);
            this.gatewayIP_lable.Location = new System.Drawing.Point(638, 25);
            this.gatewayIP_lable.Name = "gatewayIP_lable";
            this.gatewayIP_lable.Size = new System.Drawing.Size(49, 14);
            this.gatewayIP_lable.TabIndex = 14;
            this.gatewayIP_lable.Text = "网关IP";
            // 
            // gatewayMac_lable
            // 
            this.gatewayMac_lable.AutoSize = true;
            this.gatewayMac_lable.Font = new System.Drawing.Font("宋体", 10F);
            this.gatewayMac_lable.Location = new System.Drawing.Point(638, 57);
            this.gatewayMac_lable.Name = "gatewayMac_lable";
            this.gatewayMac_lable.Size = new System.Drawing.Size(56, 14);
            this.gatewayMac_lable.TabIndex = 15;
            this.gatewayMac_lable.Text = "网关MAC";
            // 
            // targetIP_textBox
            // 
            this.targetIP_textBox.Enabled = false;
            this.targetIP_textBox.Location = new System.Drawing.Point(399, 23);
            this.targetIP_textBox.Name = "targetIP_textBox";
            this.targetIP_textBox.Size = new System.Drawing.Size(165, 21);
            this.targetIP_textBox.TabIndex = 16;
            // 
            // targetMac_textBox
            // 
            this.targetMac_textBox.Enabled = false;
            this.targetMac_textBox.Location = new System.Drawing.Point(399, 55);
            this.targetMac_textBox.Name = "targetMac_textBox";
            this.targetMac_textBox.Size = new System.Drawing.Size(165, 21);
            this.targetMac_textBox.TabIndex = 17;
            // 
            // gatewayIP_textBox
            // 
            this.gatewayIP_textBox.Enabled = false;
            this.gatewayIP_textBox.Location = new System.Drawing.Point(739, 23);
            this.gatewayIP_textBox.Name = "gatewayIP_textBox";
            this.gatewayIP_textBox.Size = new System.Drawing.Size(165, 21);
            this.gatewayIP_textBox.TabIndex = 18;
            // 
            // gatewayMac_textBox
            // 
            this.gatewayMac_textBox.Enabled = false;
            this.gatewayMac_textBox.Location = new System.Drawing.Point(739, 55);
            this.gatewayMac_textBox.Name = "gatewayMac_textBox";
            this.gatewayMac_textBox.Size = new System.Drawing.Size(165, 21);
            this.gatewayMac_textBox.TabIndex = 19;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(399, 129);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(505, 23);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(305, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 21;
            this.label1.Text = "抓取进度";
            // 
            // fliter_lable
            // 
            this.fliter_lable.AutoSize = true;
            this.fliter_lable.Font = new System.Drawing.Font("宋体", 10F);
            this.fliter_lable.Location = new System.Drawing.Point(305, 88);
            this.fliter_lable.Name = "fliter_lable";
            this.fliter_lable.Size = new System.Drawing.Size(49, 14);
            this.fliter_lable.TabIndex = 22;
            this.fliter_lable.Text = "Fliter";
            // 
            // Fliter_textBox
            // 
            this.Fliter_textBox.Location = new System.Drawing.Point(399, 86);
            this.Fliter_textBox.Name = "Fliter_textBox";
            this.Fliter_textBox.Size = new System.Drawing.Size(165, 21);
            this.Fliter_textBox.TabIndex = 23;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 562);
            this.Controls.Add(this.Fliter_textBox);
            this.Controls.Add(this.fliter_lable);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.gatewayMac_textBox);
            this.Controls.Add(this.gatewayIP_textBox);
            this.Controls.Add(this.targetMac_textBox);
            this.Controls.Add(this.targetIP_textBox);
            this.Controls.Add(this.gatewayMac_lable);
            this.Controls.Add(this.gatewayIP_lable);
            this.Controls.Add(this.targetMac_lable);
            this.Controls.Add(this.StopSniffer_Btn);
            this.Controls.Add(this.StartSniffer_Btn);
            this.Controls.Add(this.Mask_textBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.IpAddress_textBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.Stop_Btn);
            this.Controls.Add(this.Start_Btn);
            this.Controls.Add(this.targetIP_lable);
            this.Controls.Add(this.Output_Textbox);
            this.Controls.Add(this.Host_ListBox);
            this.Controls.Add(this.Device_checkboxlist);
            this.Name = "Form1";
            this.Text = "ARPTool";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox Host_ListBox;
        private System.Windows.Forms.TextBox Output_Textbox;
        private System.Windows.Forms.Label targetIP_lable;
        private System.Windows.Forms.Button Start_Btn;
        private System.Windows.Forms.Button Stop_Btn;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.CheckedListBox Device_checkboxlist;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox IpAddress_textBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Mask_textBox;
        private System.Windows.Forms.Button StartSniffer_Btn;
        private System.Windows.Forms.Button StopSniffer_Btn;
        private System.Windows.Forms.Label targetMac_lable;
        private System.Windows.Forms.Label gatewayIP_lable;
        private System.Windows.Forms.Label gatewayMac_lable;
        private System.Windows.Forms.TextBox targetIP_textBox;
        private System.Windows.Forms.TextBox targetMac_textBox;
        private System.Windows.Forms.TextBox gatewayIP_textBox;
        private System.Windows.Forms.TextBox gatewayMac_textBox;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label fliter_lable;
        private System.Windows.Forms.TextBox Fliter_textBox;


    }
}

