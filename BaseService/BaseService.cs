using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;
using CommonObjects;

namespace FrameworkBaseService
{
    public class BaseService
    {

        private int _id = 0;
        public string BasePath = "";

        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; }
        }

        private bool _stopped = false;
        public bool Stopped
        {
            get { return _stopped; }
            set { _stopped = value; }
        }
        
        #region Monitoring

        private string _serviceName;
        public string ServiceName
        {
            get { return _serviceName; }
            set { _serviceName = value; }
        }

        private string _objectUniqueId;
        public string ObjectUniqueId
        {
            get { return _objectUniqueId; }
        }

        private Int64 _totalSuccessfull = 0;
        public Int64 TotalSuccessfull
        {
            get { return _totalSuccessfull; }
            set { _totalSuccessfull = value; }
        }

        private Int64 _totalError = 0;
        public Int64 TotalError
        {
            get { return _totalError; }
            set { _totalError = value; }
        }

        private string _lastErrorMsg = "";
        public string LastErrorMsg
        {
            get { return _lastErrorMsg; }
            set { _lastErrorMsg = value; }
        }

        private string _objectName;

        public string ObjectName
        {
            get { return _objectName; }
        }
        private string _totalRunningThreads = "None";

        public string TotalRunningThreads
        {
            get { return _totalRunningThreads; }
        }
        private string _queueLen = "Unknown";

        public string QueueLen
        {
            get { return _queueLen; }
        }
        private string _comment = "";

        public string Comment
        {
            get { return _comment; }
        }
        
        #endregion

        public BaseService(string serviceName, string basePath, int id, List<string> parameters)
        {
            _serviceName = serviceName;
            _objectName = serviceName + id.ToString();
            BasePath = basePath;
            _id = id;
            _objectUniqueId = Utility.GetGUID();
            _comment = parameters[0];
        }

    }
}
