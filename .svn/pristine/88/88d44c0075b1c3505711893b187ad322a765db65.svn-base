using FPayDataAccess;
using FPayPropertyLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace FrameworkBaseService
{
    public static class SequenceGenerator
    {

        public static string NextSeq(string target,
            string prefix,
            bool includeCurrentDateTime)
        {
            string result = "";
            lock (typeof(SequenceGenerator))
            {
                result = prefix;
                if (includeCurrentDateTime)
                {
                    result += Utility.PersianDate().Replace("/", "") + Utility.GetTime().Replace(":", "");
                }
                KeyValueDB keyValue = new KeyValueDB();
                keyValue.KEY_VALUE_KEY = target;
                keyValue.KEY_VALUE_GROUP = "SEQUENCE";
                DataTable table = KeyValueOperation.GetKeyValueByParameters(keyValue);
                long seq = long.Parse(table.Rows[0]["KEY_VALUE_VALUE"].ToString());
                result += seq.ToString();
                seq++;
                keyValue.KEY_VALUE_VALUE = seq.ToString();
                keyValue.KEY_VALUE_LINK = "";
                keyValue.KEY_VALUE_DESC = "";
                KeyValueOperation.UpdateKeyValue(keyValue);
            }
            return (result);
        }

    }
}
