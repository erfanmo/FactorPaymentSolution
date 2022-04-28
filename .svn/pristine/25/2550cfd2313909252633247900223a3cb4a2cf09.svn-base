using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;
using CommonObjects;

namespace FrameworkBaseService
{
    public class ServiceOperation
    {

        public ServiceOperation()
        {

        }

        ServiceInfo _serviceInfo = new ServiceInfo();

        private int _pivot = 0;
        public BaseService GetBaseService()
        {
            BaseService result = null;
            try
            {
                lock (typeof(ServiceOperation))
                {
                    int index = _pivot;
                    while (true)
                    {
                        if (_pivot >= _serviceInfo.ServiceList.Count)
                            _pivot = 0;
                        if (_serviceInfo.ServiceList.Count == 0)
                            throw new Exception("There is no item in the list");
                        if (_serviceInfo.ServiceList[_pivot].Stopped == false)
                        {
                            result = _serviceInfo.ServiceList[_pivot];
                            break;
                        }
                        _pivot++;
                        if (_pivot == index)
                            throw new Exception("No worker exist");
                    }
                    _pivot++;
                    if (_pivot >= _serviceInfo.ServiceList.Count)
                        _pivot = 0;
                }
            }
            catch (Exception ex)
            {
                result = null;
            }
            return (result);
        }

        public bool StopService(string uniqueId)
        {
            bool result = true;
            try
            {
                lock (typeof(ServiceOperation))
                {
                    BaseService selected = null;
                    foreach (BaseService worker in _serviceInfo.ServiceList)
                    {
                        if (worker.ObjectUniqueId == uniqueId)
                        {
                            selected = worker;
                            break;
                        }
                    }
                    if (selected == null)
                        throw new Exception("Could not find");
                    selected.Stopped = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return (result);
        }

        public bool StartService(string uniqueId)
        {
            bool result = true;
            try
            {
                lock (typeof(ServiceOperation))
                {
                    BaseService selected = null;
                    foreach (BaseService worker in _serviceInfo.ServiceList)
                    {
                        if (worker.ObjectUniqueId == uniqueId)
                        {
                            selected = worker;
                            break;
                        }
                    }
                    if (selected == null)
                        throw new Exception("Could not find");
                    selected.Stopped = false;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return (result);
        }

        public int ReleaseService(string uniqueId)
        {
            int result = 0;
            try
            {
                lock (typeof(ServiceOperation))
                {
                    BaseService selected = null;
                    foreach (BaseService worker in _serviceInfo.ServiceList)
                    {
                        if (worker.ObjectUniqueId == uniqueId)
                        {
                            selected = worker;
                            break;
                        }
                        result++;
                    }
                    if (selected == null)
                        throw new Exception("Could not find");
                    _serviceInfo.ServiceList.Remove(selected);
                }
            }
            catch (Exception ex)
            {
                result = -1;
            }
            return (result);
        }

        private int _workerCounter = 0;
        public int GetNextId()
        {
            int result = _workerCounter;
            _workerCounter++;
            return (result);
        }

        public bool AddService(BaseService service)
        {
            bool result = true;
            try
            {
                lock (typeof(ServiceOperation))
                {
                    _serviceInfo.ServiceList.Add(service);
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return (result);
        }

        public List<MonitoringItem> MonitorObject()
        {
            List<MonitoringItem> result = null;
            try
            {
                result = new List<MonitoringItem>();
                foreach (BaseService service in _serviceInfo.ServiceList)
                {
                    MonitoringItem item = new MonitoringItem();
                    item.Comment = service.Comment;
                    item.LastErrorMsg = service.LastErrorMsg;
                    item.ObjectName = service.ObjectName;
                    item.ObjectUniqueId = service.ObjectUniqueId;
                    item.QueueLen = service.QueueLen;
                    item.TotalError = service.TotalError.ToString();
                    item.TotalRunningThreads = service.TotalRunningThreads;
                    item.TotalSuccessfull = service.TotalSuccessfull.ToString();
                    item.ServiceName = service.ServiceName;
                    item.MultiInstance = "true";
                    item.IsBusy = service.IsBusy.ToString().ToLower();
                    item.Stopped = service.Stopped.ToString().ToLower();
                    result.Add(item);
                }
            }
            catch (Exception ex)
            {
                result = null;
            }
            return (result);
        }

        public string Ping(string instanceUniqueId)
        {
            string result = "";
            try
            {
                if (String.IsNullOrEmpty(instanceUniqueId))
                {
                    List<string> resultList = ServiceInfo.ManageStatistics("GET",TimeSpan.FromTicks(0),null);
                    result = Encoding.Default.GetString(Utility.SerializeXML_Memory(typeof(List<string>), resultList));
                }
                else
                {
                    BaseService selected = null;
                    foreach (BaseService service in _serviceInfo.ServiceList)
                    {
                        if (service.ObjectUniqueId == instanceUniqueId)
                        {
                            selected = service;
                            break;
                        }
                    }
                    if (selected == null)
                        throw new Exception();
                    result = "Echo " + Utility.PersianDate() + " - " + Utility.GetTime() +
                        " ,Instance Name: " + selected.ObjectName;
                }
            }
            catch (Exception ex)
            {
                result = "Error";
            }
            return (result);
        }

        public string ClearErrorMessage(string instanceUniqueId)
        {
            string result = "";
            try
            {
                BaseService selected = null;
                foreach (BaseService service in _serviceInfo.ServiceList)
                {
                    if (service.ObjectUniqueId == instanceUniqueId)
                    {
                        selected = service;
                        break;
                    }
                }
                if (selected == null)
                    throw new Exception();
                selected.LastErrorMsg = "";
                result = "Error Message Cleared" + " ,Instance Name: " + selected.ObjectName;
            }
            catch (Exception ex)
            {
                result = "Could Not Clear Error Message";
            }
            return (result);
        }

    }
}
