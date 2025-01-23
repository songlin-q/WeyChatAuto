using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using FlaUI.Core.WindowsAPI;
using FlaUI.UIA3;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Interop;
using WeyChatAuto.Common;
using WeyChatAuto.DBHelper;
using WeyChatAuto.Model;
using WindowsInput;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WeyChatAuto
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitWechat();
        }




        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            GC.Collect();
        }
        private CancellationToken FriendCancellationToken { get; set; }
        private CancellationTokenSource FriendTokenSource { get; set; }
        private CancellationToken ChatListCancellationToken { get; set; }
        private CancellationTokenSource ChatListTokenSource { get; set; }
        private CancellationToken GetFriendCancellationToken { get; set; }
        private CancellationTokenSource GetFriendTokenSource { get; set; }
        /// <summary>
        /// 微信的进程ID
        /// </summary>
        private int ProcessId { get; set; }
        /// <summary>
        /// 微信窗体
        /// </summary>
        private FlaUI.Core.AutomationElements.Window wxWindow { get; set; }
        private bool IsInit { get; set; }
        /// <summary>
        /// 获取
        /// </summary>
        void GetWxHandle()
        {
            var process = Process.GetProcessesByName("Wechat").FirstOrDefault();
            if (process != null)
            {
                ProcessId = process.Id;
            }
        }
        /// <summary>
        /// 加载微信
        /// </summary>
        void InitWechat()
        {
            IsInit = true;
            GetWxHandle();
            GetFriendTokenSource = new CancellationTokenSource();
            GetFriendCancellationToken = GetFriendTokenSource.Token;
            ChatListTokenSource = new CancellationTokenSource();
            ChatListCancellationToken = ChatListTokenSource.Token;
            FriendTokenSource = new CancellationTokenSource();
            FriendCancellationToken = FriendTokenSource.Token;
            //根据微信进程ID绑定FLAUI
            try
            {
                var application = FlaUI.Core.Application.Attach(ProcessId);
                var automation = new UIA3Automation();
                //获取微信window自动化操作对象
                wxWindow = application.GetMainWindow(automation);
            }
            catch (Exception ex)
            {
                if (MessageBox.Show(ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                    this.Close();
            }



        }
        void GetFocus()
        {
            if (wxWindow.Patterns.Window.PatternOrDefault != null)
            {
                //将微信窗体设置为默认焦点状态
                wxWindow.Patterns.Window.Pattern.SetWindowVisualState(FlaUI.Core.Definitions.WindowVisualState.Normal);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetFocus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(() =>
            {
                var all = wxWindow.FindAllDescendants(x => x.ByControlType(FlaUI.Core.Definitions.ControlType.ListItem));


                var allItem = all.AsParallel().Where(s => s.Parent.Name == "会话").ToList();
                foreach (var item in allItem)
                {
                    if (!string.IsNullOrWhiteSpace(item.Name) && !lbFriendList.Items.Contains(item.Name.ToString()))
                    {
                        lbFriendList.Items.Add(item.Name.Replace("Pinned", "").Trim());
                    }
                }
            })
            { IsBackground = true };
            th.Start();
        }

        private void Btn_mouseClick_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(() =>
            {
                wxWindow.FindFirstDescendant(s => s.ByName("邵林")).Click();


            })
            { IsBackground = true };
            th.Start();
        }

        private void lbFriendList_SelectedIndexChanged(object sender, EventArgs e)
        {


            var kkk = lbFriendList.SelectedItem.ToString();
            wxWindow.FindFirstDescendant(s => s.ByName(kkk)).Click();

            Keyboard.Type($"群发消息,请忽略:{DateTime.Now}");

            var sendButton = wxWindow.FindFirstDescendant(cf => cf.ByName("发送(S)"));
            sendButton.Click();


        }

        private void Btn_GetContent_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(() =>
            {
                var all = wxWindow.FindAllDescendants(x => x.ByControlType(FlaUI.Core.Definitions.ControlType.ListItem));

                var ll = all.AsParallel();


                var allItem = all.AsParallel().Where(s => s.Parent.Name == "消息").ToList();


                foreach (var item in allItem)
                {
                    var msgItem = item.FindAllDescendants(x => x.ByControlType(FlaUI.Core.Definitions.ControlType.Text));

                    var msgbb = item.FindAllDescendants(x => x.ByControlType(FlaUI.Core.Definitions.ControlType.Button));

                    if (msgbb.Length > 0)
                    {

                        if (item.Name == "[文件]")
                        {

                            var msg = "";
                            for (var i = 1; i < msgItem.Length; i++)
                            {
                                msg += $"\n{msgItem[i].Name}";
                            }
                            var mgs = $"{msgbb[0].Name}:{msg}";
                            lb_msgContent.Items.Add(mgs);
                        }
                        else
                        {

                            var mgs = $"{msgbb[0].Name}:{item.Name}";
                            lb_msgContent.Items.Add(mgs);
                        }


                    }
                    else
                    {
                        var msgcontent = "\r\n";

                        foreach (var msg in msgItem)
                        {
                            msgcontent += "\n" + msg.Name;

                        }
                        lb_msgContent.Items.Add(msgcontent);
                    }






                }


                //var alssl = wxWindow.FindAllDescendants(x => x.ByControlType(FlaUI.Core.Definitions.ControlType.Text));


                //var files = wxWindow.FindAllDescendants(cf => cf.ByName("[文件]"));



                //var fileMessages = wxWindow.FindAllDescendants(cf => cf.ByClassName("FileMessage"));

                //foreach (var item in files)
                //{
                //    var fileNameElement = item.FindFirstChild(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Text));

                //    var kkkk = item.Parent;
                //}
                //var fileMessage = wxWindow.FindFirstDescendant(cf => cf.ByName("吉林仓-20250117.xlsx"));


                //fileMessage.RightClick();

                //// 等待上下文菜单弹出
                //Retry.WhileException(() => wxWindow.FindFirstDescendant(cf => cf.ByName("另存为...")), timeout: TimeSpan.FromSeconds(5));

                //// 点击“另存为”选项
                //var saveAsOption = wxWindow.FindFirstDescendant(cf => cf.ByName("另存为..."));
                //if (saveAsOption == null)
                //{
                //    Console.WriteLine("未找到“另存为”选项。");
                //    return;
                //}
                //saveAsOption.Click();

                //Keyboard.Press(FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
                //Keyboard.Release(FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);


            })
            { IsBackground = true };
            th.Start();
        }

        private void Btn_SendContent_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(() =>
            {
                var fileMessage = wxWindow.FindFirstDescendant(cf => cf.ByName("发送文件"));
                fileMessage.Click();
                Thread.Sleep(500);
                //0x601790
                IntPtr windowHandle = FindWindow(null, "打开");
                // 创建 UIA3 自动化对象
                var automation = new UIA3Automation();
                // 通过句柄获取窗口对象
                var Openwindow = automation.FromHandle(windowHandle);
                Thread.Sleep(500);
                var fff = Openwindow.FindFirstDescendant(w => w.ByAutomationId("1001"));
                fff.Click();
                //输入文件存放的文件夹地址
                Keyboard.Type(@"H:\auto");
                Thread.Sleep(1000);
                SendKeys.SendWait("{ENTER}");
                Thread.Sleep(500);
                SendKeys.SendWait("{ENTER}");
                var fg = Openwindow.FindFirstDescendant(w => w.ByName("文件1.txt"));
                fg.Click();
                Thread.Sleep(500);
                var fileDialog = Openwindow.FindFirstDescendant(w => w.ByName("打开(O)"));
                fileDialog.Click();
                Thread.Sleep(500);
                var enOk = wxWindow.FindFirstDescendant(w => w.ByName("发送（1）"));
                enOk.Click();

            })
            { IsBackground = true };
            th.Start();
        }

        private void Btn_SendMsg_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(() =>
            {
                wxWindow.FindFirstDescendant(s => s.ByName("微信自动化测试群")).Click();
                Keyboard.Type(@"@");
                Thread.Sleep(1000);
                Keyboard.Type(@"qs");
                wxWindow.FindFirstDescendant(s => s.ByName("qs")).Click();
                Thread.Sleep(500);
                Keyboard.Type(@"这是一个自动化测试！");
                Thread.Sleep(500);
                wxWindow.FindFirstDescendant(s => s.ByName("发送(S)")).Click();
            })
            { IsBackground = true };
            th.Start();
        }

        private void Btn_MouseScro_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(MouseScro) { IsBackground = true };
            th.Start();
        }
        void MouseScro()
        {

            wxWindow.FindFirstDescendant(s => s.ByName("消息")).Click();
            var inputSimulator = new InputSimulator();

            while (true)
            {
                inputSimulator.Mouse.VerticalScroll(120);
            }


        }

        private void Btn_SaveAsFile_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(SaveAsFile) { IsBackground = true };
            th.Start();
        }
        void SaveAsFile()
        {


            var files = wxWindow.FindAllDescendants(cf => cf.ByName("[文件]"));

            var msgItem = files[0].FindAllDescendants(x => x.ByControlType(FlaUI.Core.Definitions.ControlType.Text));

            var msgbb = files[0].FindAllDescendants(x => x.ByControlType(FlaUI.Core.Definitions.ControlType.Button));


            var fielName = msgItem[1].Name;

            var fileMessage = wxWindow.FindFirstDescendant(cf => cf.ByName(fielName));


            fileMessage.RightClick();

            // 点击“另存为”选项
            var saveAsOption = wxWindow.FindFirstDescendant(cf => cf.ByName("另存为..."));

            saveAsOption.Click();

            //这里添加延迟一秒使用窗口打开需要时间，如果不加延迟会定位窗口时会失效
            Thread.Sleep(1000);

            IntPtr windowHandle = FindWindow(null, "另存为...");

            // 创建 UIA3 自动化对象
            var automation = new UIA3Automation();
            // 通过句柄获取窗口对象
            var Openwindow = automation.FromHandle(windowHandle);


            Openwindow.FindFirstDescendant(w => w.ByName("正在加载")).Click();


            var floder = @"H:\filesave";

            if (!Directory.Exists(floder))
            {
                Directory.CreateDirectory(floder);
            }
            Keyboard.Type(floder);
            Thread.Sleep(1000);
            SendKeys.SendWait("{ENTER}");
            Thread.Sleep(500);
            SendKeys.SendWait("{ENTER}");

            //判定当前这个文件夹是否存在相同名字的文件 存在的话就删除只保留最新的 ps:也可以通过修改当前文件的名字进行保存
            var filePath = $@"{floder}\{fielName}";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }





            Openwindow.FindFirstDescendant(w => w.ByName("保存(S)")).Click();

        }
        public int GetIndexByName(AutomationElement[] elements, string targetName)
        {
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i].Name == targetName)
                {
                    return i; // 返回匹配的索引
                }
            }
            return -1; // 如果没有找到匹配的元素，返回 -1
        }
        private void Btn_GetFriend_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(() =>
            {


                List<string> namsList = new List<string>();
                wxWindow.FindFirstDescendant(cf => cf.ByName("通讯录")).Click();

                var contentSize = wxWindow.FindFirstDescendant(s => s.ByName("联系人"));

                var contentHeight = contentSize.BoundingRectangle.Height;
                var contentY = contentSize.BoundingRectangle.Y;

                wxWindow.FindFirstDescendant(s => s.ByName("联系人")).Click();
                var inputSimulator = new InputSimulator();

                while (true)
                {


                    var all = wxWindow.FindAllDescendants(x => x.ByControlType(FlaUI.Core.Definitions.ControlType.ListItem));

                    var allHeight = contentY + contentHeight;

                    var allItem = all.AsParallel().Where(s => s.Parent.Name == "联系人" && !string.IsNullOrWhiteSpace(s.Name)).ToList();

                    var namess = allItem.Where(s => s.Name != "新的朋友" && !string.IsNullOrWhiteSpace(s.Name)).Select(s => s.Name).ToList();


                    foreach (var item in allItem)
                    {
                        var itemHeight = (item.BoundingRectangle.Y + item.BoundingRectangle.Height);
                        if (!string.IsNullOrWhiteSpace(item.Name) && item.Name != "新的朋友" && namsList.Where(s => s == item.Name).Any() == false)
                        {

                            if (itemHeight <= allHeight)
                            {

                                wxWindow.FindFirstDescendant(s => s.ByName(item.Name)).Click();

                                var kkk = wxWindow.FindAllDescendants(x => x.ByControlType(FlaUI.Core.Definitions.ControlType.Text));
                                var wxIndex = GetIndexByName(kkk, ConfigurationManager.AppSettings["codeName"].ToString());
                                var code = wxIndex > 0 ? kkk[wxIndex + 1].Name : "";
                                namsList.Add(item.Name);
                                lbFriend.Items.Add($"{item.Name}:{code}");
                            }

                        }

                        var hh = allHeight - itemHeight;
                        if (hh == 0)
                        {
                            return;
                        }

                    }

                    inputSimulator.Mouse.VerticalScroll(-80);
                    //这里必须要有延迟，不然会导致鼠标滚动过快，导致获取不到元素
                    Thread.Sleep(1000);
                }
            })
            { IsBackground = true };
            th.Start();
        }

        private void Btn_CreateData_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(() =>
            {
                //DbHelper.CreateDataBase();
                // DbHelper.NewTable("T_FriendList", new List<string> { "name", "code" });


                Expression<Func<FriendMod, bool>> wherelamdba = s => s.name == "" && s.code == "";


                var kk = DbHelper.QueryDt<FriendMod>("T_FriendList", wherelamdba);

            })
            {
                IsBackground = true
            };
            th.Start();
        }
    }
}
