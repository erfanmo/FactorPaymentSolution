using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;
using CommonObjects;
using System.Data;
using System.IO;
using System.Drawing;

namespace ServiceTest
{
    public static class Information
    {

        public static string publicKey = "";

        public static void SecureCredential(UserCredential uCredential, string refCode)
        {
            string encKey = Utility.GetComplicatedPassword(20);
            uCredential.Password = SecurityManagement.EncryptRSA("Password=" + uCredential.Password + ";" +
                "EncKey=" + encKey + ";" +
                "Date=" + Utility.PersianDate() + ";" +
                "RefrenceCode=" + refCode + ";",
                publicKey,
                1024);
            uCredential.ExtendedPassword = SecurityManagement.EncryptRSA("Password=" + uCredential.ExtendedPassword + ";" +
                "EncKey=" + encKey + ";" +
                "Date=" + Utility.PersianDate() + ";" +
                "RefrenceCode=" + refCode + ";",
                publicKey,
                1024);
            uCredential.UserName = SecurityManagement.Encrypt3DES(uCredential.UserName, encKey);
            uCredential.CellNo = SecurityManagement.Encrypt3DES(uCredential.CellNo, encKey);
            uCredential.Data = SecurityManagement.Encrypt3DES(uCredential.Data, encKey);
            uCredential.FingerPrint = SecurityManagement.Encrypt3DES(uCredential.FingerPrint, encKey);
            uCredential.Ip = SecurityManagement.Encrypt3DES(uCredential.Ip, encKey);
            uCredential.Mac = SecurityManagement.Encrypt3DES(uCredential.Mac, encKey);
            uCredential.Otp = SecurityManagement.Encrypt3DES(uCredential.Otp, encKey);
            uCredential.Serial = SecurityManagement.Encrypt3DES(uCredential.Serial, encKey);
        }

        public static DataTable CreateTable(string str)
        {
            DataTable table = new DataTable("Table");
            string[] strRows = str.Split('|');
            int colCount = 0;
            if (strRows.Count() > 1)
            {
                colCount = strRows[0].Split('#').Count();
                for (int i = 0; i < colCount - 1; i++)
                {
                    table.Columns.Add("COL" + i.ToString());
                }
            }
            foreach (string row in strRows)
            {
                if (row == "")
                    continue;
                string[] strCols = row.Split('#');
                object[] objects = new object[colCount - 1];
                for (int j = 0; j < colCount - 1; j++)
                {
                    objects[j] = strCols[j];
                }
                table.Rows.Add(objects);
            }
            return (table);
        }

        public static DataTable CreateTable(string str, string columns)
        {
            DataTable table = new DataTable("Table");
            string[] strRows = str.Split('|');
            string[] strColumns = columns.Split('|');
            int colCount = 0;
            if (strColumns.Count() > 1)
            {
                colCount = columns.Split('|').Count();
                for (int i = 0; i < colCount - 1; i++)
                {
                    table.Columns.Add(strColumns[i]);
                }
            }
            foreach (string row in strRows)
            {
                if (row == "")
                    continue;
                string[] strCols = row.Split('#');
                object[] objects = new object[colCount - 1];
                for (int j = 0; j < colCount - 1; j++)
                {
                    objects[j] = strCols[j];
                }
                table.Rows.Add(objects);
            }
            return (table);
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

        //public static List<MenuItem> GetMenuItems(string language, string menuStr, int level)
        //{
        //    List<MenuItem> result = new List<MenuItem>();
        //    string levelMark = "(L" + level.ToString() + ")";
        //    if (menuStr.Trim() == "")
        //    {
        //        return null;
        //    }
        //    if (language.Trim() == "")
        //    {
        //        language = "FA";
        //    }
        //    string[] lst = menuStr.Replace(levelMark, "$").Split('$');
        //    foreach (string str in lst)
        //    {
        //        int indexP1 = str.IndexOf('(');
        //        int indexP2 = str.IndexOf(')');
        //        int indexB1 = str.IndexOf('[');
        //        MenuItem item = new MenuItem();
        //        item.Key = str.Substring(0, indexP1);
        //        string parameters = str.Substring(indexP1 + 1, indexP2 - indexP1 - 1);
        //        Dictionary<string, string> paramDic = GetParamDic(parameters.Split('|'));
        //        item.Title = paramDic["TITLE" + language];
        //        item.Link = paramDic["LINK"];
        //        item.Icon = paramDic["ICON"];
        //        string recursive = str.Substring(indexB1 + 1, str.Length - indexB1 - 2);
        //        item.SubMenu = GetMenuItems(language, recursive, level + 1);
        //        result.Add(item);
        //    }
        //    return (result);
        //}

    }
}
