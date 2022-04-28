using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPayDataAccess
{
    public class FPayDataSetTableAdapter
    {

        public static FPayDataSetTableAdapters.tbl_KeyValueTableAdapter GetKeyValue()
        {
            return (new FPayDataSetTableAdapters.tbl_KeyValueTableAdapter());
        }

        public static FPayDataSetTableAdapters.tbl_CustomerTableAdapter GetCustomer()
        {
            return (new FPayDataSetTableAdapters.tbl_CustomerTableAdapter());
        }

    }
}
