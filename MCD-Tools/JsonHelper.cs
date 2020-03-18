using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCD_Tools
{
    public class JsonHelper
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="DateConvert">时间戳是否转换成日期类型</param>
        /// <returns></returns>
        public static string Serializer(object obj, bool DateConvert = false)
        {
            if (obj == null)
                return "";
            try
            {

                var str = JsonConvert.SerializeObject(obj);


                if (DateConvert)
                {
                    str = System.Text.RegularExpressions.Regex.Replace(str, @"\\/Date\((\d+)\)\\/", match =>
                    {
                        DateTime dt = new DateTime(1970, 1, 1);
                        dt = dt.AddMilliseconds(long.Parse(match.Groups[1].Value));
                        dt = dt.ToLocalTime();
                        return dt.ToString("yyyy-MM-dd HH:mm:ss");
                    });
                }
                return str;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <param name="objType"></param>
        /// <returns></returns>
        public static object DeserializeObject(string jsonString, Type objType)
        {

            try
            {
                return JsonConvert.DeserializeObject(jsonString, objType);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string json) where T : class
        {
            try
            {
                JsonSerializer serializer = new JsonSerializer();
                StringReader sr = new StringReader(json);
                object o = serializer.Deserialize(new JsonTextReader(sr), typeof(T));
                T t = o as T;
                return t;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        ///// <summary>
        ///// Json2Dictionary(string, object)
        ///// </summary>
        ///// <param name="jsonData"></param>
        ///// <returns></returns>
        //public static Dictionary<T1, T2> JsonToDictionary<T1, T2>(string jsonData) where T1 : class 
        //{
        //    return DeserializeObject<Dictionary<T1, T2>>(jsonData);
        //}

        /// <summary>
        /// Json2Dictionary(string, object)
        /// </summary>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        public static Dictionary<string, object> JsonToDictionary(string jsonData)
        {
            return DeserializeObject<Dictionary<string, object>>(jsonData);
        }
    }
}
