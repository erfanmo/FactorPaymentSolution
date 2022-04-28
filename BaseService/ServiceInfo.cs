using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logging;
using CommonObjects;
using FPayPropertyLib;
using FPayDataAccess;
using System.Security.Cryptography;
using System.Data;
using System.Reflection;
using System.IO;
using System.Drawing;

namespace FrameworkBaseService
{
    public class ServiceInfo
    {

        public List<BaseService> ServiceList = new List<BaseService>();
        public static Dictionary<string, FPayService.CommandHandler> ProcDic = null;

        public static Dictionary<string, MessageObj> FPayMessages = null;
        public static Dictionary<string, MessageObj> FPayParams = null;

        public static LogInfo logInfo = null;
        public static Logger logger = null;

        public static Dictionary<string, long> StatisticsDic = new Dictionary<string, long>();
        public static long TotalCount = 0;
        public static TimeSpan AverageTime = new TimeSpan(0, 0, 0, 0, 0);
        public static List<string> ManageStatistics(string action, TimeSpan duration, Response response)
        {
            List<string> result = null;
            lock (typeof(ServiceInfo))
            {
                try
                {
                    if (action == "SET")
                    {
                        try
                        {
                            StatisticsDic[response.MessageCode]++;
                        }
                        catch
                        {
                            StatisticsDic.Add(response.MessageCode, 1);
                        }
                        AverageTime = TimeSpan.FromTicks((TimeSpan.FromTicks(AverageTime.Ticks * TotalCount) + duration).Ticks / (TotalCount + 1));
                        TotalCount++;
                    }
                    else if (action == "GET")
                    {
                        result = new List<string>();
                        result.Add("TotalCount=" + TotalCount.ToString());
                        TotalCount = 0;
                        result.Add("AverageTime=" + AverageTime.ToString());
                        AverageTime = TimeSpan.FromTicks(0);
                        foreach (string key in StatisticsDic.Keys)
                        {
                            result.Add(key + "=" + StatisticsDic[key].ToString());
                        }
                        StatisticsDic.Clear();
                    }
                }
                catch
                { }
            }
            return (result);
        }

        public static List<string> GetRSAKeys(int keySize)
        {
            List<string> result = new List<string>();
            RSA rsa = new RSACryptoServiceProvider(keySize);
            result.Add(rsa.ToXmlString(false));
            result.Add(rsa.ToXmlString(true));
            return (result);
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            var tb = new DataTable("RESULT_TABLE");
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            try
            {
                foreach (var prop in props)
                {
                    //tb.Columns.Add(prop.Name, prop.PropertyType);
                    tb.Columns.Add(prop.Name, typeof(string));
                }

                foreach (var item in items)
                {
                    var values = new object[props.Length];
                    for (var i = 0; i < props.Length; i++)
                    {
                        values[i] = props[i].GetValue(item, null);
                    }
                    tb.Rows.Add(values);
                }
                return tb;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static string CreateStringTable(DataTable table)
        {
            StringBuilder Result = new StringBuilder();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    Result.Append(row[j].ToString() + '#');
                }
                Result.Append('|');
            }
            return (Result.ToString());
        }

        public static string ImageToString(Image im)
        {
            MemoryStream ms = new MemoryStream();
            im.Save(ms, im.RawFormat);
            byte[] array = ms.ToArray();
            return Convert.ToBase64String(array);
        }

        public static Image StringToImage(string imageString)
        {
            byte[] array = Convert.FromBase64String(imageString);
            Image image = Image.FromStream(new MemoryStream(array));
            return image;
        }

        public static Dictionary<string, string> GetParamDic(string[] arrayParams)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (string item in arrayParams)
            {
                if (item.Trim() == "")
                    continue;
                char[] chList = new char[1];
                chList[0] = '=';
                string[] splited = item.Split(chList, 2);
                result.Add(splited[0].Trim().ToUpper(), splited[1]);
            }
            return (result);
        }

    }
}
