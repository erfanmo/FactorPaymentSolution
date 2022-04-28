using CommonObjects;
using FrameworkBaseService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Utilities;

namespace FPayWebInterface.Services
{
    public static class Basic
    {

        public static List<string> Test(Dictionary<string, string> paramDic)
        {
            List<string> lstResult = new List<string>();
            //-----------------------------------------
            string refCode = Utility.GetUniqueId();
            UserCredential uCredential = new UserCredential();
            uCredential.UserName = "";
            uCredential.Password = "";
            uCredential.ExtendedPassword = "";
            CommonObjects.Request request = new CommonObjects.Request();
            request.BinaryData = null;
            request.Command = "FPayServiceTest";
            request.CommandType = "Normal";
            request.Credential = uCredential;
            request.Parameters = DateTime.Now.ToString();
            request.RefrenceNo = refCode;
            //-----------------------------------------
            var ctx = HttpContext.Current;
            FPayService customerService = (FPayService)((ServiceOperation)
                ctx.Cache[Services.Instances.CacheKey]).GetBaseService();
            Response response = customerService.DoJob(request);
            lstResult.Add("ErrorCode=" + response.MessageCode);
            lstResult.Add("ErrorDesc=" + response.Message);
            lstResult.Add("Result=" + response.Result);
            return (lstResult);
        }

        public static List<string> GetDataList(Dictionary<string, string> paramDic)
        {
            List<string> lstResult = new List<string>();
            List<string> paramList = new List<string>();
            foreach (string key in paramDic.Keys)
            {
                paramList.Add(key + "=" + paramDic[key]);
            }
            //-----------------------------------------
            string refCode = Utility.GetUniqueId();
            UserCredential uCredential = new UserCredential();
            uCredential.UserName = "";
            uCredential.Password = "";
            uCredential.ExtendedPassword = "";
            CommonObjects.Request request = new CommonObjects.Request();
            request.BinaryData = null;
            request.Command = "FPayServiceGetDataList";
            request.CommandType = "Normal";
            request.Credential = uCredential;
            request.Parameters = Encoding.Default.GetString(Utility.SerializeXML_Memory(typeof(List<string>), paramList));
            request.RefrenceNo = refCode;
            //-----------------------------------------
            var ctx = HttpContext.Current;
            FPayService customerService = (FPayService)((ServiceOperation)
                ctx.Cache[Services.Instances.CacheKey]).GetBaseService();
            Response response = customerService.DoJob(request);
            if (response.MessageCode == "000000")
            {
                lstResult = (List<string>)Utility.DeserializeXML_Memory(typeof(List<string>),
                    Encoding.Default.GetBytes(response.Result));
                lstResult.Insert(0, "ErrorCode=" + response.MessageCode);
                lstResult.Insert(1, "ErrorDesc=" + response.Message);
            }
            else
            {
                lstResult.Add("ErrorCode=" + response.MessageCode);
                lstResult.Add("ErrorDesc=" + response.Message);
            }
            return (lstResult);
        }

    }
}