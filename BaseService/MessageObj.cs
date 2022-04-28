using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrameworkBaseService
{
    public class MessageObj
    {

        private string _key;

        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }
        private string _value;

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
        private string _type;

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

    }
}
