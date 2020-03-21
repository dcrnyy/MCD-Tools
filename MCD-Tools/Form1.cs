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

        /*
        蒙威桦	45212219870906573X	18269063966
        梁廷容	452122195206145734	19968336925
        吴斯璞	452122198004261298	15078751336
        何世洲	452122197904152711	13457919568
        宾春英	45252819800529178X	18376856390
        何世裕	452122197211202712	19152617607
        何木	452122199108242715	18977120350
        何昌注	452122196406022733	13324818024
        黄志领	452122196203075739	13768648731
        何增联	452122197003102717	15819381068
        何振伸	452122196312144836	13457037037
        何振佑	452122196610064850	13768600187
        蒙大成	45212219890205571X	18934739405
        王志耀	452122197703215739	13457909225
        韦心伟	452122196905095734	13978192919
        梁建鸿	452122198004104831	18178656862
        */

        private List<MemberParam> memberList = new List<MemberParam>
        {
            new MemberParam{Name="蒙威桦",IdNumber="45212219870906573X",Phone="18269063966"},
            new MemberParam{Name="梁廷容",IdNumber="452122195206145734",Phone="19968336925"},
            new MemberParam{Name="吴斯璞",IdNumber="452122198004261298",Phone="15078751336"},
            new MemberParam{Name="何世洲",IdNumber="452122197904152711",Phone="13457919568"},
            new MemberParam{Name="宾春英",IdNumber="45252819800529178X",Phone="18376856390"},
            new MemberParam{Name="何世裕",IdNumber="452122197211202712",Phone="19152617607"},
            new MemberParam{Name="何木",IdNumber="452122199108242715",Phone="18977120350"},
            new MemberParam{Name="何昌注",IdNumber="452122196406022733",Phone="13324818024"},
            new MemberParam{Name="黄志领",IdNumber="452122196203075739",Phone="13768648731"},
            new MemberParam{Name="何增联",IdNumber="452122197003102717",Phone="15819381068"},
            new MemberParam{Name="何振伸",IdNumber="452122196312144836",Phone="13457037037"},
            new MemberParam{Name="何振佑",IdNumber="452122196610064850",Phone="13768600187"},
            new MemberParam{Name="蒙大成",IdNumber="45212219890205571X",Phone="18934739405"},
            new MemberParam{Name="王志耀",IdNumber="452122197703215739",Phone="13457909225"},
            new MemberParam{Name="韦心伟",IdNumber="452122196905095734",Phone="13978192919"},
            new MemberParam{Name="梁建鸿",IdNumber="452122198004104831",Phone="18178656862"},
        };
        private List<string> okList = new List<string>();
        private List<VerifyCodeData> verifyList = null;

        /// <summary>
        /// 获取的验证码数量
        /// </summary>
        private int count = 0;

        DateTime dt;

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                lvParam.Items.Clear();
                for (int n = 0; n < memberList.Count; n++)
                {
                    var item = memberList[n];
                    var lvi = new ListViewItem((n+1).ToString());
                    lvi.SubItems.Add(item.Name);
                    lvi.SubItems.Add(item.IdNumber);
                    lvi.SubItems.Add(item.Phone);
                    lvi.Tag = item;
                    lvParam.Items.Add(lvi);
                }
                count = memberList.Count * 2 * 50;

            }
            catch (Exception ex)
            {

            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Read(count);
        }

        private void Read(int count)
        {
            //验证码有效时间3分钟

            verifyList = new List<VerifyCodeData>();
            for (int n = 0; n < count; n++)
            {
                new Task(() =>
                {
                    //verifyList.Add(new VerifyCodeData { verifyCode = Guid.NewGuid().ToString() });
                    //button1.Text = $"准  备({verifyList.Count})";

                    try
                    {
                        var obj = GetVerifyCode();
                        if (obj != null)
                        {
                            obj.Code = ttshituAPI.GetCode(obj.verifyCode);
                            verifyList.Add(obj);
                            button1.Text = $"准  备({verifyList.Count})";
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }).Start();

            }

        }

        private static object lockObj = new object();
        private VerifyCodeData GetVerifyCodeData()
        {
            lock (lockObj)
            {
                try
                {
                    if (verifyList != null && verifyList.Count > 0)
                    {
                        var obj = verifyList.FirstOrDefault();
                        verifyList.Remove(obj);
                        button1.Text = $"准  备({verifyList.Count})";
                        return obj;
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            dt = DateTime.Now;
            timer2.Interval = 120; //一秒钟大概7~8次
            timer2.Start();

        }

        private void timer2_Tick(object sender, EventArgs e)
        {

            foreach (var item in memberList)
            {
                if (okList.Contains(item.Name))
                    continue;

                if (verifyList == null && verifyList.Count == 0)
                    return;
                new Task(() =>
                {
                    var verify = GetVerifyCodeData();
                    if (verify != null)
                    {
                        Start(item, verify, dt.AddDays(1).Date);
                    }
                }).Start();
                new Task(() =>
                {
                    var verify = GetVerifyCodeData();
                    if (verify != null)
                    {
                        Start(item, verify, dt.AddDays(2).Date);
                    }
                }).Start();
                //new Task(() =>
                //{
                //    var verify = GetVerifyCodeData();
                //    if (verify != null)
                //    {
                //        Start(item, verify, dt.AddDays(3).Date);
                //    }
                //}).Start();
                //new Task(() =>
                //{
                //    var verify = GetVerifyCodeData();
                //    if (verify != null)
                //    {
                //        Start(item, verify, dt.AddDays(4).Date);
                //    }
                //}).Start();
                //new Task(() =>
                //{
                //    var verify = GetVerifyCodeData();
                //    if (verify != null)
                //    {
                //        Start(item, verify, dt.AddDays(5).Date);
                //    }
                //}).Start();
            }
            //Log("End");
        }





        public void Start(MemberParam memberParam, VerifyCodeData verify, DateTime dt)
        {
            try
            {
                //var json = "{\"code\":1016,\"msg\":\"今日预约已达上限\",\"data\":null}";
                //Log($"{memberParam.Name}--{json}");
                //var msg2 = dt + $" {memberParam.Name}：{json}";
                //SetOKtb(msg2, dt);
                //return;



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
                var dtStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffffff");
                var resultJson = HttpHelper.Post("http://120.202.98.106:8990/ebsapi/organization/basreservation/public/updateSchedule", postStr);

                Log($"{dtStr}-{memberParam.Name}\r\n{resultJson}\r\n");
                var result = JsonHelper.DeserializeObject<PostResult>(resultJson);
                if (result.code == 1018)
                {
                    lock (lockObj)
                    {
                        okList.Add(memberParam.Name);
                        Log(memberParam.Name + " -->已预约");
                    }
                }
                if (result.code != 1016 && result.code != 1018 && result.code != 1023)
                {
                    var msg = dt.Date + $" {memberParam.Name} -->{result.msg}";
                    SetOKtb(msg, dt);
                }
                /*
                 {"code":1016,"msg":"今日预约已达上限","data":null}
                 {"code":1018,"msg":"已预约","data":null}
                 {"code":1023,"msg":"未到预约开始时间","data":null}
                 */
            }
            catch (Exception ex)
            {

            }
        }

        private void SetOKtb(string name, DateTime dt)
        {
            lock (lockObj)
            {
                try
                {
                    var msg = name + " -->" + dt.ToString("yyyy-MM-dd HH：mm:ss ffffff");
                    tbOK.AppendText(msg + "\r\n\r\n");
                    Write("ok.txt", msg);
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void Log(string msg)
        {
            lock (lockObj)
            {
                try
                {
                    Console.WriteLine(msg);
                    tbLog.AppendText($"{msg}\r\n");
                    Write("log.txt", msg);
                }
                catch (Exception ex)
                {

                }
            }

            //tbLog.Text = $"{DateTime.Now.ToString("HH:mm:ss")}--{result}----提交参数：{param.Name}-{dt.ToString("yyyy-MM-dd")} \r\n" + tbLog.Text;
        }

        public void Write(string path, string str)
        {
            File.AppendAllText(path, str + "\r\n");
        }


        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        private VerifyCodeData GetVerifyCode()
        {
            try
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
            catch (Exception ex)
            {
                return null;
            }
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
            //timer2.Interval = 3000;
            if (timer2.Enabled)
            {
                timer2.Stop();
                return;
            }

            var obj = GetVerifyCode();
            if (obj == null)
                return;

            obj.Code = ttshituAPI.GetCode(obj.verifyCode);
            verifyList = new List<VerifyCodeData> { obj };
            var member = new MemberParam { Name = "韦大宏", Phone = "19654783325", IdNumber = "110101199003076931" };
            var verify = GetVerifyCodeData();
            if (verify != null)
            {
                Start(member, verify, DateTime.Now.AddDays(1).Date);
            }

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //cc++;
            //timer1.Stop();
            //var result = HttpHelper.Post("http://120.202.98.106:8990/ebsapi/organization/basreservation/public/updateSchedule", postStrAA);
            //Log($"=============分钟结束========\r\n {result}");
        }

        private void lvParam_DoubleClick(object sender, EventArgs e)
        {
            if (lvParam.SelectedItems.Count == 0)
                return;
            try
            {
                var item = lvParam.SelectedItems[0];
                var name = item.SubItems[1].Text;
                if (okList.Contains(name))
                {
                    okList.Remove(name);
                    item.ForeColor = Color.Black;
                    return;
                }
                item.ForeColor = Color.Blue;
                okList.Add(name);
            }
            catch (Exception ex)
            {

            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (memberList == null || memberList.Count == 0)
                return;
            var dt = DateTime.Now;
            Log("查询：");
            foreach (var item in memberList)
            {
                System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区

                new Task(() =>
                {
                    var dt1 = dt.AddDays(1).Date;
                    try
                    {

                        long stamp1 = (long)(dt1 - startTime).TotalMilliseconds; // 相差毫秒数
                        var param = new
                        {
                            idcard = item.IdNumber,
                            reservationDate = stamp1,
                        };

                        var postStr1 = JsonHelper.Serializer(param);
                        var result1 = HttpHelper.Post("http://120.202.98.106:8990/ebsapi/organization/basreservation/public/detail/get", postStr1);
                        Log($"{item.Name}-->{dt1.ToString("yyyy-MM-dd")}\r\n{result1}\r\n");
                    }
                    catch (Exception ex)
                    {
                        Log($"{item.Name}-->{dt1.ToString("yyyy-MM-dd")}\r\n异常：{ex.Message}\r\n");
                    }
                }).Start();

                new Task(() =>
                {
                    var dt2 = dt.AddDays(1).Date;
                    try
                    {

                        long stamp2 = (long)(dt2 - startTime).TotalMilliseconds; // 相差毫秒数
                        var param = new
                        {
                            idcard = item.IdNumber,
                            reservationDate = stamp2,
                        };

                        var postStr2 = JsonHelper.Serializer(param);
                        var result2 = HttpHelper.Post("http://120.202.98.106:8990/ebsapi/organization/basreservation/public/detail/get", postStr2);
                        Log($"{item.Name}-->{dt2.ToString("yyyy-MM-dd")}\r\n{result2}\r\n");
                    }
                    catch (Exception ex)
                    {
                        Log($"{item.Name}-->{dt2.ToString("yyyy-MM-dd")}\r\n异常：{ex.Message}\r\n");
                    }
                }).Start();
            }
        }

    }

}


