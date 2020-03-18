using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCD_Tools
{
    public class ttshituAPI
    {

        public static string GetCode(string base64Str)
        {
            var param = new
            {
                username = "dcrnyy",
                password = "dama666",
                typeid = "1",
                image = base64Str,
            };
            var json = JsonHelper.Serializer(param);
            var resultJson = HttpHelper.Post("http://api.ttshitu.com/base64", json);
            var resuly = JsonHelper.DeserializeObject<Result>(resultJson);
            if (resuly == null)
                return "";
            return resuly.data.result;
        }



        public class Data
        {
            /// <summary>
            /// 
            /// </summary>
            public string result { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string id { get; set; }
        }

        public class Result
    {
            /// <summary>
            /// 
            /// </summary>
            public bool success { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string code { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string message { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Data data { get; set; }
        }
    }
}

