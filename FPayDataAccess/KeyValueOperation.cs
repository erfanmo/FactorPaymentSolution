using FPayPropertyLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPayDataAccess
{
    public class KeyValueOperation
    {

        public static DataTable GetKeyValueByParameters(KeyValueDB keyValue)
        {
            DataTable table = null;
            table = FPayDataSetTableAdapter.GetKeyValue().GetKeyValueByParameters(keyValue.KEY_VALUE_ID,
                keyValue.KEY_VALUE_KEY,
                keyValue.KEY_VALUE_GROUP);
            return (table);
        }

        public static void InsertKeyValue(KeyValueDB keyValue)
        {
            FPayDataSetTableAdapter.GetKeyValue().InsertKeyValue(keyValue.KEY_VALUE_KEY,
                keyValue.KEY_VALUE_GROUP,
                keyValue.KEY_VALUE_VALUE,
                keyValue.KEY_VALUE_LINK,
                keyValue.KEY_VALUE_DESC);
        }

        public static void UpdateKeyValue(KeyValueDB keyValue)
        {
            FPayDataSetTableAdapter.GetKeyValue().UpdateKeyValue(keyValue.KEY_VALUE_KEY,
                keyValue.KEY_VALUE_GROUP,
                keyValue.KEY_VALUE_VALUE,
                keyValue.KEY_VALUE_LINK,
                keyValue.KEY_VALUE_DESC);
        }

        public static void DeleteKeyValue(KeyValueDB keyValue)
        {
            FPayDataSetTableAdapter.GetKeyValue().DeleteKeyValue(keyValue.KEY_VALUE_KEY,
                keyValue.KEY_VALUE_GROUP);
        }

    }
}
