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
            new MemberParam{Name="韦大宏",Phone="19654783325",IdNumber="110101199003076931"},

        };
        private List<VerifyCodeData> verifyList = null;

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

            //var dt = dateTimePicker1.Value;
            Read(1);
        }

        private void Read(int count)
        {
            //验证码有效时间3分钟
            verifyList = new List<VerifyCodeData>();
            for (int n = 0; n < count; n++)
            {
                new Task(() =>
                {
                    var obj = GetVerifyCode();
                    if (obj != null)
                    {
                        obj.Code = ttshituAPI.GetCode(obj.verifyCode);
                        verifyList.Add(obj);
                    }
                }).Start();
            }
        }

        private static object lockObj = new object();
        private VerifyCodeData GetVerifyCodeData()
        {
            lock (lockObj)
            {
                if (verifyList != null)
                    return verifyList.FirstOrDefault();
                return null;
            }
        }
        private void Read(DateTime dt)
        {
            //int num = 10;//每天次数
            //System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区

            //int count = 0;
            //lvRead.Items.Clear();
            //taskList = new List<TaskParam>();
            //for (int x = 0; x < 5; x++)
            //{

            //    var ndt = dt.AddDays(x + 1);
            //    long stamp = (long)(ndt.Date - startTime).TotalMilliseconds; // 相差毫秒数
            //    var timeStamp = stamp.ToString();
            //    for (int n = 0; n < lvParam.Items.Count; n++)
            //    {
            //        VerifyCodeData verifyCode = null;
            //        do
            //        {
            //            try
            //            {
            //                //verifyCode = GetVerifyCode();
            //                verifyCode = new VerifyCodeData { id = 999 };
            //            }
            //            catch (Exception ex)
            //            {

            //            }
            //        } while (verifyCode == null);
            //        // var code = ttshituAPI.GetCode(verifyCode.verifyCode);
            //        var code = "1234";

            //        var obj = lvParam.Items[n].Tag as MemberParam;
            //        count++;
            //        var postParam = new PostParam
            //        {
            //            basReservationNumberDate = timeStamp.ToString(),
            //            basReservationNumberIdcard = obj.IdNumber,
            //            basReservationNumberName = obj.Name,
            //            basReservationNumberPhone = obj.Phone,
            //            verifyCodeId = verifyCode.id,
            //            verifyCode = code,
            //        };
            //        taskList.Add(new TaskParam
            //        {
            //            Name = count + "--" + obj.Name,
            //            Param = JsonHelper.Serializer(postParam),
            //        });

            //        var lvi = new ListViewItem(count + "");
            //        lvi.SubItems.Add(obj.Name);
            //        lvi.SubItems.Add(ndt.ToShortDateString());
            //        lvi.SubItems.Add(code);
            //        lvRead.Items.Add(lvi);
            //    }

            //}
            //lbCount.Text = $"{lvParam.Items.Count}人，总任务数：" + count;
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            var dt = dateTimePicker1.Value;
            foreach (var item in memberList)
            {
                new Task(() =>
                {
                    var verify = GetVerifyCodeData();
                    Start(item, verify, dt.AddDays(1).Date);
                }).Start();
                //    new Task(() =>
                //    {
                //        var verify = GetVerifyCodeData();
                //        Start(item, verify, dt.AddDays(2).Date);
                //    }).Start();
                //    new Task(() =>
                //    {
                //        var verify = GetVerifyCodeData();
                //        Start(item, verify, dt.AddDays(3).Date);
                //    }).Start();
                //    new Task(() =>
                //    {
                //        var verify = GetVerifyCodeData();
                //        Start(item, verify, dt.AddDays(4).Date);
                //    }).Start();
                //    new Task(() =>
                //    {
                //        var verify = GetVerifyCodeData();
                //        Start(item, verify, dt.AddDays(5).Date);
                //    }).Start();
            }
        }

        private void Log(string msg)
        {
            tbLog.Text = $"{msg}\r\n{tbLog.Text}";
            //tbLog.Text = $"{DateTime.Now.ToString("HH:mm:ss")}--{result}----提交参数：{param.Name}-{dt.ToString("yyyy-MM-dd")} \r\n" + tbLog.Text;
        }





        public void Start(MemberParam memberParam, VerifyCodeData verify, DateTime dt)
        {
            try
            {
                Console.WriteLine(dt.Date + " jjj");
                System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
                long stamp = (long)(dt - startTime).TotalMilliseconds; // 相差毫秒数
                var postParam = new PostParam
                {
                    basReservationNumberDate = stamp,
                    basReservationNumberIdcard = memberParam.IdNumber,
                    basReservationNumberName = memberParam.Name,
                    basReservationNumberPhone = memberParam.Phone,
                    verifyCodeId = verify.id,
                    verifyCode = verify.Code,
                };
                var postStr = JsonHelper.Serializer(postParam);
                var resultJson = HttpHelper.Post("http://120.202.98.106:8990/ebsapi/organization/basreservation/public/updateSchedule", postStr);
                var result=JsonHelper.DeserializeObject<PostResult>(resultJson);
                if(result.code!=1016&& result.code != 1018 && result.code != 1023)
                {
                    Console.WriteLine(dt.Date+$" {memberParam.Name}：{result.msg}");
                }
                /*
                 {"code":1016,"msg":"今日预约已达上限","data":null}
                 {"code":1018,"msg":"已预约","data":null}
                 {"code":1023,"msg":"未开始","data":null}
                 */
            }
            catch (Exception ex)
            {

            }
        }





        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
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
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            cc++;
            timer1.Stop();
            var result = HttpHelper.Post("http://120.202.98.106:8990/ebsapi/organization/basreservation/public/updateSchedule", postStrAA);
            Log($"=============分钟结束========\r\n {result}");
        }

        Random rd = new Random();
        int cc = 0;

        private void timer2_Tick(object sender, EventArgs e)
        {

            //foreach (var item in taskList)
            //{
            //    new Task(() =>
            //    {
            //        Start(item);
            //    }).Start();
            //}

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


