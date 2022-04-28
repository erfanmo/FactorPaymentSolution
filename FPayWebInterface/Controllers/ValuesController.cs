using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.IO;
using System.Web.Http;

namespace FPayWebInterface.Controllers
{
    public class ValuesController : ApiController
    {

        private Dictionary<string, string> GetParamDic(string[] arrayParams, bool hasSign,ref string command)
        {
            string sign = "";
            List<string> lst = arrayParams.ToList<string>();
            if (hasSign)
            {
                sign = lst[lst.Count - 1];
                lst.RemoveAt(lst.Count - 1);
                lst.Add("SIGN=" + sign);
            }
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (string item in lst)
            {
                if (item.Trim() == "")
                    continue;
                char[] chList = new char[1];
                chList[0] = '=';
                string[] splited = item.Split(chList, 2);
                if (splited[0].Trim().ToUpper() == "COMMAND")
                {
                    command = splited[1].Trim().ToUpper();
                }
                result.Add(splited[0].Trim().ToUpper(), splited[1]);
            }
            return (result);
        }

        public string[] Post()
        {
            List<string> respList = null;
            //-- Get Service Instance
            var ctx = System.Web.HttpContext.Current;
            if (ctx.Cache[Services.Instances.CacheKey] == null)
            {
                Services.Instances.CreateInstances();
            }
            //-- Get request parameters
            HttpContent requestContent = Request.Content;
            string jsonContent = requestContent.ReadAsStringAsync().Result;
            JsonSerializerSettings setting = new JsonSerializerSettings();
            string[] content = JsonConvert.DeserializeObject<string[]>(jsonContent);
            string command = "";
            Dictionary<string, string> paramDic = GetParamDic(content, true,ref command);
            //-- Call appropriate service

            //StreamWriter wr = new StreamWriter(@"C:\Tests\AutomationAPI\sample.txt");
            //wr.WriteLine(command);
            //wr.Flush();
            //wr.Close();

            switch (command)
            {
                case "TEST":
                    respList = Services.Basic.Test(paramDic);
                    break;
                case "GETDATALIST":
                    respList = Services.Basic.GetDataList(paramDic);

                    

                    break;
                default:
                    respList = new List<string>();
                    respList.Add("ErrorCode=-1");
                    respList.Add("ErrorDesc=Undefined Command");
                    break;
            }
            return (respList.ToArray());
        }

    }
}