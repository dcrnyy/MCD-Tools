using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace MCD_Tools
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private List<MemberParam> memberList = new List<MemberParam>
        {
            new MemberParam{Name="测试1",Phone="XXXXXXXXX",IdNumber="XXXXXXXX"},
            new MemberParam{Name="测试2",Phone="XXXXXXXXX",IdNumber="XXXXXXXX"},
            new MemberParam{Name="测试3",Phone="XXXXXXXXX",IdNumber="XXXXXXXX"},
            new MemberParam{Name="测试4",Phone="XXXXXXXXX",IdNumber="XXXXXXXX"},
            new MemberParam{Name="测试5",Phone="XXXXXXXXX",IdNumber="XXXXXXXX"},
            new MemberParam{Name="测试6",Phone="XXXXXXXXX",IdNumber="XXXXXXXX"},
            new MemberParam{Name="测试7",Phone="XXXXXXXXX",IdNumber="XXXXXXXX"},
            new MemberParam{Name="测试8",Phone="XXXXXXXXX",IdNumber="XXXXXXXX"},
            new MemberParam{Name="测试9",Phone="XXXXXXXXX",IdNumber="XXXXXXXX"},
            new MemberParam{Name="测试10",Phone="XXXXXXXXX",IdNumber="XXXXXXXX"},
            new MemberParam{Name="测试11",Phone="XXXXXXXXX",IdNumber="XXXXXXXX"},
            new MemberParam{Name="测试12",Phone="XXXXXXXXX",IdNumber="XXXXXXXX"},
            new MemberParam{Name="测试13",Phone="XXXXXXXXX",IdNumber="XXXXXXXX"},
            new MemberParam{Name="测试14",Phone="XXXXXXXXX",IdNumber="XXXXXXXX"},
            new MemberParam{Name="测试15",Phone="XXXXXXXXX",IdNumber="XXXXXXXX"},
            new MemberParam{Name="测试16",Phone="XXXXXXXXX",IdNumber="XXXXXXXX"},
            new MemberParam{Name="测试17",Phone="XXXXXXXXX",IdNumber="XXXXXXXX"},
            new MemberParam{Name="测试18",Phone="XXXXXXXXX",IdNumber="XXXXXXXX"},
            new MemberParam{Name="测试19",Phone="XXXXXXXXX",IdNumber="XXXXXXXX"},
            new MemberParam{Name="测试20",Phone="XXXXXXXXX",IdNumber="XXXXXXXX"},
            new MemberParam{Name="测试21",Phone="XXXXXXXXX",IdNumber="XXXXXXXX"},
            new MemberParam{Name="测试22",Phone="XXXXXXXXX",IdNumber="XXXXXXXX"},
            new MemberParam{Name="测试23",Phone="XXXXXXXXX",IdNumber="XXXXXXXX"},
            new MemberParam{Name="测试24",Phone="XXXXXXXXX",IdNumber="XXXXXXXX"},
            new MemberParam{Name="测试25",Phone="XXXXXXXXX",IdNumber="XXXXXXXX"},
            new MemberParam{Name="测试26",Phone="XXXXXXXXX",IdNumber="XXXXXXXX"},
            new MemberParam{Name="测试27",Phone="XXXXXXXXX",IdNumber="XXXXXXXX"},
            new MemberParam{Name="测试28",Phone="XXXXXXXXX",IdNumber="XXXXXXXX"},
            new MemberParam{Name="测试29",Phone="XXXXXXXXX",IdNumber="XXXXXXXX"},
            new MemberParam{Name="测试30",Phone="XXXXXXXXX",IdNumber="XXXXXXXX"},
        };

        private List<TaskParam> taskList = null;

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                //StreamReader sr = new StreamReader("Param.txt", Encoding.Default);
                //var json = sr.ReadToEnd();
                //sr.Close();
                //sr.Dispose();

                //memberList = JsonHelper.DeserializeObject<List<MemberParam>>(json);

                lvParam.Items.Clear();
                for (int n = 0; n < memberList.Count; n++)
                {
                    var item = memberList[n];
                    var lvi = new ListViewItem(n.ToString());
                    lvi.SubItems.Add(item.Name);
                    lvi.SubItems.Add(item.IdNumber);
                    lvi.SubItems.Add(item.Phone);
                    lvi.Tag = item;
                    lvParam.Items.Add(lvi);
                }

            }
            catch (Exception ex)
            {

            }
        }


        private void button1_Click(object sender, EventArgs e)
        {

            var dt = dateTimePicker1.Value;
            Read(dt);
        }
        private void Read(DateTime dt)
        {
            int num = 10;//每天次数
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区

            int count = 0;
            lvRead.Items.Clear();
            taskList = new List<TaskParam>();
            for (int x = 0; x < 5; x++)
            {

                var ndt = dt.AddDays(x + 1);
                long stamp = (long)(ndt.Date - startTime).TotalMilliseconds; // 相差毫秒数
                var timeStamp = stamp.ToString();
                for (int n = 0; n < lvParam.Items.Count; n++)
                {
                    VerifyCodeData verifyCode = null;
                    do
                    {
                        try
                        {
                            //verifyCode = GetVerifyCode();
                            verifyCode = new VerifyCodeData { id = 999 };
                        }
                        catch (Exception ex)
                        {

                        }
                    } while (verifyCode == null);
                    // var code = ttshituAPI.GetCode(verifyCode.verifyCode);
                    var code = "1234";

                    var obj = lvParam.Items[n].Tag as MemberParam;
                    count++;
                    var postParam = new PostParam
                    {
                        basReservationNumberDate = timeStamp.ToString(),
                        basReservationNumberIdcard = obj.IdNumber,
                        basReservationNumberName = obj.Name,
                        basReservationNumberPhone = obj.Phone,
                        verifyCodeId = verifyCode.id,
                        verifyCode = code,
                    };
                    taskList.Add(new TaskParam
                    {
                        Name = count + "--" + obj.Name,
                        Param = JsonHelper.Serializer(postParam),
                    });

                    var lvi = new ListViewItem(count + "");
                    lvi.SubItems.Add(obj.Name);
                    lvi.SubItems.Add(ndt.ToShortDateString());
                    lvi.SubItems.Add(code);
                    lvRead.Items.Add(lvi);
                }

            }
            lbCount.Text = $"{lvParam.Items.Count}人，总任务数：" + count;
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            var sdt = DateTime.Now;
            foreach (var item in taskList)
            {
                //Thread t1 = new Thread(new ThreadStart(Go1));
                //Thread t1 = new Thread(new ParameterizedThreadStart(Start));
                //t1.Start(item);

                new Task(() =>
                {
                    Start(item);
                }).Start();
               // Start(item);
            }

            var edt = DateTime.Now;

            var ff = edt - sdt;
            lbTime.Text = $"用时:{ff.TotalSeconds} 秒";
        }

        private void Log(string msg)
        {
            tbLog.Text = $"{msg}\r\n{tbLog.Text}";
            //tbLog.Text = $"{DateTime.Now.ToString("HH:mm:ss")}--{result}----提交参数：{param.Name}-{dt.ToString("yyyy-MM-dd")} \r\n" + tbLog.Text;
        }





        public void Start(object obj)
        {
            TaskParam param = obj as TaskParam;
            //PostParam param = lvi.Tag as PostParam;
            //var result = HttpHelper.Post("http://120.202.98.106:8990/ebsapi/organization/basreservation/public/updateSchedule", postStr);
            var result = param.Name;
            Console.WriteLine(DateTime.Now.ToString() + "   " + cc + "次:" + param.Name);
            //await Log($"{DateTime.Now.ToString("HH:mm:ss:fffff")}--提交参数：{param.Name}----{result} \r\n");
            //FileStream fs1 = new FileStream($"{param.Name}.txt", FileMode.Create, FileAccess.ReadWrite);//创建写入文件
            //fs1.Close();
        }






        private VerifyCodeData GetVerifyCode()
        {
            var param = new
            {
                iamgeHeight = 32,
                imageWidth = 100,
                len = 4,
                type = "0",
            };
            var str = JsonHelper.Serializer(param);
            var base64 = HttpHelper.Post("http://120.202.98.106:8990/ebsapi/message/sendsms/makeVerifyCode", str);
            var VerifyCode = JsonHelper.DeserializeObject<VerifyCode>(base64);
            if (VerifyCode == null)
                return null;
            return VerifyCode.data;
        }


        #region base64转图片
        /// <summary>
        /// 图片上传 Base64解码
        /// </summary>
        /// <param name="dataURL">Base64数据</param>
        /// <param name="imgName">图片名字</param>
        /// <returns>返回一个相对路径</returns>
        public Image decodeBase64ToImage(string base64)
        {
            try//会有异常抛出，try，catch一下
            {

                byte[] arr = Convert.FromBase64String(base64);//将纯净资源Base64转换成等效的8位无符号整形数组

                System.IO.Stream ms = new System.IO.MemoryStream(arr);//转换成无法调整大小的MemoryStream对象
                var image = Image.FromStream(ms);
                ms.Close();//关闭当前流，并释放所有与之关联的资源
                return image;


            }
            catch (Exception e)
            {
                string massage = e.Message;
                return null;
            }
        }

        #endregion


        private string postStrAA = "";
        private void button2_Click(object sender, EventArgs e)
        {
            timer2.Interval = 3000;
            timer2.Start();
            //System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            //long stamp = (long)(DateTime.Now.AddDays(2).Date - startTime).TotalMilliseconds; // 相差毫秒数
            //var timeStamp = stamp.ToString();
            //var verifyCode = GetVerifyCode();
            //var postParam = new PostParam
            //{
            //    basReservationNumberDate = timeStamp,
            //    basReservationNumberIdcard = "450127199003078410",
            //    basReservationNumberName = "李大同",
            //    basReservationNumberPhone = "18655547774",
            //    verifyCodeId = verifyCode.id,
            //    verifyCode = ttshituAPI.GetCode(verifyCode.verifyCode),

            //};
            //postStrAA = JsonHelper.Serializer(postParam);

            //int min = 3;
            //timer1.Interval = (1000 * 58 * min);
            //timer1.Start();
            //Log($"============={min}分钟开始========\r\n ");
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            var result = HttpHelper.Post("http://120.202.98.106:8990/ebsapi/organization/basreservation/public/updateSchedule", postStrAA);
            Log($"=============分钟结束========\r\n {result}");
        }

        Random rd = new Random();
        int cc = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            cc++;
            if (cc == 5)
            {
                timer2.Stop();
            }


            foreach (var item in taskList)
            {
                new Task(() =>
                {
                    Start(item);
                }).Start();
            }

            Console.WriteLine($"mmmmmmmmmm[{cc}次]mmmmmmmmmmmmm");


            //foreach (var item in taskList)
            //{
            //    Thread t1 = new Thread(new ParameterizedThreadStart(Start));
            //    t1.Start(item);
            //    //Start(item);
            //}
        }
    }

}


