using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI_Test
{
    public static class Information
    {
        public static string publicKey = "";
        public static string sessionId = "";
        public static int seq = 0;
        public static int nextSeq()
        {
            int result = seq++;
            return (result);
        }
        public static string URL = "";
        public static string PostJson(string DATA, string URL)
        {

            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            client.BaseAddress = new System.Uri(URL);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            System.Net.Http.HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");
            HttpResponseMessage messge = client.PostAsync(URL, content).Result;
            string description = string.Empty;
            if (messge.IsSuccessStatusCode)
            {
                string result = messge.Content.ReadAsStringAsync().Result;
                description = result;
            }
            return description;
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
    }
}
