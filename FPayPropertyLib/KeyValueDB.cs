using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPayPropertyLib
{
    public class KeyValueDB
    {
        private string _KEY_VALUE_ID;

        public string KEY_VALUE_ID
        {
            get { return _KEY_VALUE_ID; }
            set { _KEY_VALUE_ID = value; }
        }
        private string _KEY_VALUE_KEY;

        public string KEY_VALUE_KEY
        {
            get { return _KEY_VALUE_KEY; }
            set { _KEY_VALUE_KEY = value; }
        }
        private string _KEY_VALUE_GROUP;

        public string KEY_VALUE_GROUP
        {
            get { return _KEY_VALUE_GROUP; }
            set { _KEY_VALUE_GROUP = value; }
        }
        private string _KEY_VALUE_VALUE;

        public string KEY_VALUE_VALUE
        {
            get { return _KEY_VALUE_VALUE; }
            set { _KEY_VALUE_VALUE = value; }
        }
        private string _KEY_VALUE_LINK;

        public string KEY_VALUE_LINK
        {
            get { return _KEY_VALUE_LINK; }
            set { _KEY_VALUE_LINK = value; }
        }
        private string _KEY_VALUE_DESC;

        public string KEY_VALUE_DESC
        {
            get { return _KEY_VALUE_DESC; }
            set { _KEY_VALUE_DESC = value; }
        }
    }
}
