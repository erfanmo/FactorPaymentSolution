using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace WebAPI_Test
{
    public partial class frm_APITest : Form
    {
        public frm_APITest()
        {
            InitializeComponent();
        }

        private void cmb_Domain_SelectedIndexChanged(object sender, EventArgs e)
        {
            Information.URL = cmb_Domain.Text + "/api/Values";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string endPoint = Information.URL;
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<string> lst = new List<string>();
            lst.Add("Command=test");
            lst.Add("");
            string reqStr = js.Serialize(lst.ToArray());
            string[] result = js.Deserialize<string[]>(Information.PostJson(reqStr, endPoint));
            MessageBox.Show(result[0] + "\r\n" +
                result[1]);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string endPoint = Information.URL;
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<string> lst = new List<string>();
            lst.Add("Command=GetDataList");
            //lst.Add("UserName=" + "administrator");
            lst.Add("SEQUENCE=" + "SEQUENCE");

            //string encriptedAC = SecurityManagement.EncryptRSA("DateTime=" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") +
            //    ";" + "SessionID=" + Information.sessionId + ";" + "NextSeq=" + Information.nextSeq().ToString() + ";", Information.publicKey, 2048);
            //lst.Add(encriptedAC);
            string encriptedAC = "";
            lst.Add(encriptedAC);

            string reqStr = js.Serialize(lst.ToArray());
            string[] result = js.Deserialize<string[]>(Information.PostJson(reqStr, endPoint));
            MessageBox.Show(result[0] + "\r\n" +
                result[1]);
            if (result[0].Replace("ErrorCode=", "") == "000000")
            {
                DataTable table = Information.CreateTable(result[2], "KEY|VALUE|");
            }
        }
    }
}
