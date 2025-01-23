namespace WeyChatAuto
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.lbFriendList = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.Btn_mouseClick = new System.Windows.Forms.Button();
            this.Btn_GetContent = new System.Windows.Forms.Button();
            this.Btn_SendContent = new System.Windows.Forms.Button();
            this.Btn_SendMsg = new System.Windows.Forms.Button();
            this.lb_msgContent = new System.Windows.Forms.ListBox();
            this.Btn_MouseScro = new System.Windows.Forms.Button();
            this.Btn_SaveAsFile = new System.Windows.Forms.Button();
            this.Btn_GetFriend = new System.Windows.Forms.Button();
            this.lbFriend = new System.Windows.Forms.ListBox();
            this.Btn_CreateData = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(231, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "获得焦点";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbFriendList
            // 
            this.lbFriendList.FormattingEnabled = true;
            this.lbFriendList.ItemHeight = 12;
            this.lbFriendList.Location = new System.Drawing.Point(8, 40);
            this.lbFriendList.Name = "lbFriendList";
            this.lbFriendList.Size = new System.Drawing.Size(209, 532);
            this.lbFriendList.TabIndex = 1;
            this.lbFriendList.SelectedIndexChanged += new System.EventHandler(this.lbFriendList_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(334, 14);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(130, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "获取聊天好友列表";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Btn_mouseClick
            // 
            this.Btn_mouseClick.Location = new System.Drawing.Point(231, 73);
            this.Btn_mouseClick.Name = "Btn_mouseClick";
            this.Btn_mouseClick.Size = new System.Drawing.Size(130, 23);
            this.Btn_mouseClick.TabIndex = 0;
            this.Btn_mouseClick.Text = "鼠标单击事件";
            this.Btn_mouseClick.UseVisualStyleBackColor = true;
            this.Btn_mouseClick.Click += new System.EventHandler(this.Btn_mouseClick_Click);
            // 
            // Btn_GetContent
            // 
            this.Btn_GetContent.Location = new System.Drawing.Point(231, 153);
            this.Btn_GetContent.Name = "Btn_GetContent";
            this.Btn_GetContent.Size = new System.Drawing.Size(130, 23);
            this.Btn_GetContent.TabIndex = 0;
            this.Btn_GetContent.Text = "获取聊天内容";
            this.Btn_GetContent.UseVisualStyleBackColor = true;
            this.Btn_GetContent.Click += new System.EventHandler(this.Btn_GetContent_Click);
            // 
            // Btn_SendContent
            // 
            this.Btn_SendContent.Location = new System.Drawing.Point(231, 252);
            this.Btn_SendContent.Name = "Btn_SendContent";
            this.Btn_SendContent.Size = new System.Drawing.Size(130, 23);
            this.Btn_SendContent.TabIndex = 0;
            this.Btn_SendContent.Text = "发送内容";
            this.Btn_SendContent.UseVisualStyleBackColor = true;
            this.Btn_SendContent.Click += new System.EventHandler(this.Btn_SendContent_Click);
            // 
            // Btn_SendMsg
            // 
            this.Btn_SendMsg.Location = new System.Drawing.Point(231, 317);
            this.Btn_SendMsg.Name = "Btn_SendMsg";
            this.Btn_SendMsg.Size = new System.Drawing.Size(130, 23);
            this.Btn_SendMsg.TabIndex = 0;
            this.Btn_SendMsg.Text = "发送文本";
            this.Btn_SendMsg.UseVisualStyleBackColor = true;
            this.Btn_SendMsg.Click += new System.EventHandler(this.Btn_SendMsg_Click);
            // 
            // lb_msgContent
            // 
            this.lb_msgContent.FormattingEnabled = true;
            this.lb_msgContent.ItemHeight = 12;
            this.lb_msgContent.Location = new System.Drawing.Point(381, 40);
            this.lb_msgContent.Name = "lb_msgContent";
            this.lb_msgContent.Size = new System.Drawing.Size(209, 532);
            this.lb_msgContent.TabIndex = 2;
            // 
            // Btn_MouseScro
            // 
            this.Btn_MouseScro.Location = new System.Drawing.Point(231, 200);
            this.Btn_MouseScro.Name = "Btn_MouseScro";
            this.Btn_MouseScro.Size = new System.Drawing.Size(130, 23);
            this.Btn_MouseScro.TabIndex = 0;
            this.Btn_MouseScro.Text = "鼠标滚动";
            this.Btn_MouseScro.UseVisualStyleBackColor = true;
            this.Btn_MouseScro.Click += new System.EventHandler(this.Btn_MouseScro_Click);
            // 
            // Btn_SaveAsFile
            // 
            this.Btn_SaveAsFile.Location = new System.Drawing.Point(231, 367);
            this.Btn_SaveAsFile.Name = "Btn_SaveAsFile";
            this.Btn_SaveAsFile.Size = new System.Drawing.Size(130, 23);
            this.Btn_SaveAsFile.TabIndex = 0;
            this.Btn_SaveAsFile.Text = "文件另存为";
            this.Btn_SaveAsFile.UseVisualStyleBackColor = true;
            this.Btn_SaveAsFile.Click += new System.EventHandler(this.Btn_SaveAsFile_Click);
            // 
            // Btn_GetFriend
            // 
            this.Btn_GetFriend.Location = new System.Drawing.Point(231, 405);
            this.Btn_GetFriend.Name = "Btn_GetFriend";
            this.Btn_GetFriend.Size = new System.Drawing.Size(130, 23);
            this.Btn_GetFriend.TabIndex = 0;
            this.Btn_GetFriend.Text = "获取好友";
            this.Btn_GetFriend.UseVisualStyleBackColor = true;
            this.Btn_GetFriend.Click += new System.EventHandler(this.Btn_GetFriend_Click);
            // 
            // lbFriend
            // 
            this.lbFriend.FormattingEnabled = true;
            this.lbFriend.ItemHeight = 12;
            this.lbFriend.Location = new System.Drawing.Point(606, 40);
            this.lbFriend.Name = "lbFriend";
            this.lbFriend.Size = new System.Drawing.Size(209, 532);
            this.lbFriend.TabIndex = 2;
            // 
            // Btn_CreateData
            // 
            this.Btn_CreateData.Location = new System.Drawing.Point(231, 474);
            this.Btn_CreateData.Name = "Btn_CreateData";
            this.Btn_CreateData.Size = new System.Drawing.Size(130, 23);
            this.Btn_CreateData.TabIndex = 0;
            this.Btn_CreateData.Text = "创建数据";
            this.Btn_CreateData.UseVisualStyleBackColor = true;
            this.Btn_CreateData.Click += new System.EventHandler(this.Btn_CreateData_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(965, 609);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lb_msgContent);
            this.tabPage1.Controls.Add(this.lbFriend);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.lbFriendList);
            this.tabPage1.Controls.Add(this.Btn_mouseClick);
            this.tabPage1.Controls.Add(this.Btn_CreateData);
            this.tabPage1.Controls.Add(this.Btn_GetContent);
            this.tabPage1.Controls.Add(this.Btn_GetFriend);
            this.tabPage1.Controls.Add(this.Btn_MouseScro);
            this.tabPage1.Controls.Add(this.Btn_SaveAsFile);
            this.tabPage1.Controls.Add(this.Btn_SendContent);
            this.tabPage1.Controls.Add(this.Btn_SendMsg);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(957, 583);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(948, 583);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 641);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "微信自动化监测";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox lbFriendList;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button Btn_mouseClick;
        private System.Windows.Forms.Button Btn_GetContent;
        private System.Windows.Forms.Button Btn_SendContent;
        private System.Windows.Forms.Button Btn_SendMsg;
        private System.Windows.Forms.ListBox lb_msgContent;
        private System.Windows.Forms.Button Btn_MouseScro;
        private System.Windows.Forms.Button Btn_SaveAsFile;
        private System.Windows.Forms.Button Btn_GetFriend;
        private System.Windows.Forms.ListBox lbFriend;
        private System.Windows.Forms.Button Btn_CreateData;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}

