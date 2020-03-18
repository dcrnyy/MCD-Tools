using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCD_Tools
{


    public class VerifyCode
    {
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public VerifyCodeData data { get; set; }


    }
    public class VerifyCodeData
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        ///  验证码图片base64
        /// </summary>
        public string verifyCode { get; set; }
    }


    public class MemberParam
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string IdNumber { get; set; }
    }

    public class TaskParam
    {
        public string Name { get; set; }
        public string Param { get; set; }

    }
    public class PostParam
    {
        /// <summary>
        /// 
        /// </summary>
        public string basReservationNumberDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string basReservationNumberIdcard { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string basReservationNumberName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string basReservationNumberPhone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string verifyCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int verifyCodeId { get; set; }
    }
}
