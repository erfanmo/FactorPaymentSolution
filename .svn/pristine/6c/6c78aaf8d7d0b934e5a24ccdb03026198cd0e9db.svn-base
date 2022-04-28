using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonObjects;
using Utilities;
using Logging;
using System.IO;
using System.Data;
using FPayDataAccess;
using FPayPropertyLib;
using System.Security.Cryptography;
using System.Drawing;
using System.Net.Mail;
using System.Net;

namespace FrameworkBaseService
{
    public class FPayService : BaseService, BaseServiceInterface
    {

        public FPayService(string serviceName, string basePath, int id, List<string> parameters)
            : base(serviceName, basePath, id, parameters)
        {

        }

        #region BaseServiceInterface Members

        public delegate Response CommandHandler(Request request);

        public Response DoJob(Request request)
        {
            Response response = null;
            CommandHandler handler = null;

            DateTime startAction = DateTime.Now;
            try
            {                
                try
                {
                    handler = ServiceInfo.ProcDic[request.Command];
                }
                catch
                {
                    handler = null;
                }
                if (handler != null)
                {
                    response = handler(request);
                }
                else
                {
                    response = CreateResponse(request, "FPay300001");
                }
                if (response.MessageCode != "000000")
                {
                    throw new Exception(response.MessageCode + ": " + response.Message);
                }
                else
                {
                    TotalSuccessfull++;
                    //****************** Do Log Action
                    LogItem item = new LogItem();
                    item.Action = "DoJob";
                    item.Date = Utility.PersianDate();
                    item.Description = request.Command + ": Do Job Successfully";
                    item.Duration = "";
                    item.LogLevel = LogInfo.LogLevel.LOW_DETAIL;
                    item.ModuleName = "FPayService";
                    item.OutputMessageType = LogInfo.OutputMessageType.NOTIFICATION;
                    item.OutputType = LogInfo.OutputType.FILE;
                    item.SubModule = "FPayService";
                    item.Time = Utility.GetTime();
                    item.UniqueId = request.RefrenceNo;
                    LogWorker worker = ServiceInfo.logger.GetLogWorker(ServiceInfo.logInfo);
                    worker.WriteLog(item, ServiceInfo.logInfo);
                    //********************************
                }
            }
            catch (Exception ex)
            {
                TotalError++;
                LastErrorMsg = ex.Message;
                //****************** Do Log Action
                LogItem item = new LogItem();
                item.Action = "DoJob";
                item.Date = Utility.PersianDate();
                item.Description = "Exception: " + request.Command + ", " + ex.Message;
                item.Duration = "";
                item.LogLevel = LogInfo.LogLevel.LOW_DETAIL;
                item.ModuleName = "FPayService";
                item.OutputMessageType = LogInfo.OutputMessageType.ERROR;
                item.OutputType = LogInfo.OutputType.FILE;
                item.SubModule = "FPayService";
                item.Time = Utility.GetTime();
                item.UniqueId = request.RefrenceNo;
                LogWorker worker = ServiceInfo.logger.GetLogWorker(ServiceInfo.logInfo);
                worker.WriteLog(item, ServiceInfo.logInfo);
                //********************************
            }
            DateTime endAction = DateTime.Now;
            try
            {
                if (bool.Parse(ServiceInfo.FPayParams["EnableStatistics"].Value))
                    ServiceInfo.ManageStatistics("SET", endAction - startAction, response);
            }
            catch
            {}
            return (response);
        }

        public bool InitializeService()
        {
            bool result = true;
            try
            {
                //-------------------- Load Parameters
                List<MessageObj> listParams = (List<MessageObj>)Utility.DeserializeXML_File(typeof(List<MessageObj>),
                    BasePath + "\\FPayParams.xml");
                ServiceInfo.FPayParams = new Dictionary<string, MessageObj>();
                foreach (MessageObj p in listParams)
                {
                    ServiceInfo.FPayParams.Add(p.Key, p);
                }
                //-------------------- Ramona Logging Settings
                ServiceInfo.logger = new Logger();
                ServiceInfo.logInfo = new LogInfo();
                if (ServiceInfo.FPayParams["LogType"].Value == "DAILY_YEAR_MONTH")
                    ServiceInfo.logInfo.loggingType = LogInfo.LogType.DAILY_YEAR_MONTH;
                else if (ServiceInfo.FPayParams["LogType"].Value == "MONTHLY_SIMPLE")
                    ServiceInfo.logInfo.loggingType = LogInfo.LogType.MONTHLY_SIMPLE;
                else if (ServiceInfo.FPayParams["LogType"].Value == "MONTHLY_YEAR")
                    ServiceInfo.logInfo.loggingType = LogInfo.LogType.MONTHLY_YEAR;
                if (ServiceInfo.FPayParams["LogLevel"].Value == "DETAIL")
                    ServiceInfo.logInfo.loggingLevel = LogInfo.LogLevel.DETAIL;
                else if (ServiceInfo.FPayParams["LogLevel"].Value == "HIGH_DETAIL")
                    ServiceInfo.logInfo.loggingLevel = LogInfo.LogLevel.HIGH_DETAIL;
                else if (ServiceInfo.FPayParams["LogLevel"].Value == "LOW_DETAIL")
                    ServiceInfo.logInfo.loggingLevel = LogInfo.LogLevel.LOW_DETAIL;
                ServiceInfo.logger.AddLogWorker(BasePath + "\\" + "Log", ServiceInfo.logInfo);
                //-------------------- Make Delegate Dictionary
                ServiceInfo.ProcDic = new Dictionary<string, CommandHandler>();
                CommandHandler handler = new CommandHandler(FPayServiceTest);
                ServiceInfo.ProcDic.Add("FPayServiceTest", handler);
                handler = new CommandHandler(FPayServiceGetDataList);
                ServiceInfo.ProcDic.Add("FPayServiceGetDataList", handler);
                handler = new CommandHandler(FPayServiceRegistrationRequest);
                ServiceInfo.ProcDic.Add("FPayServiceRegistrationRequest", handler);
                
                //-------------------- Other Initialization
                List<MessageObj> listMsg = (List<MessageObj>)Utility.DeserializeXML_File(typeof(List<MessageObj>),
                    BasePath + "\\FPayMessages.xml");
                ServiceInfo.FPayMessages = new Dictionary<string, MessageObj>();
                foreach (MessageObj msg in listMsg)
                {
                    ServiceInfo.FPayMessages.Add(msg.Key, msg);
                }
                //****************** Do Log Action
                LogItem item = new LogItem();
                item.Action = "InitializeService";
                item.Date = Utility.PersianDate();
                item.Description = "Initialize Service Successfully";
                item.Duration = "";
                item.LogLevel = LogInfo.LogLevel.LOW_DETAIL;
                item.ModuleName = "FPayService";
                item.OutputMessageType = LogInfo.OutputMessageType.NOTIFICATION;
                item.OutputType = LogInfo.OutputType.FILE;
                item.SubModule = "FPayService";
                item.Time = Utility.GetTime();
                item.UniqueId = "FPayService";
                LogWorker worker = ServiceInfo.logger.GetLogWorker(ServiceInfo.logInfo);
                worker.WriteLog(item, ServiceInfo.logInfo);
                //********************************
            }
            catch (Exception ex)
            {
                result = false;
                //****************** Do Log Action
                LogItem item = new LogItem();
                item.Action = "InitializeService";
                item.Date = Utility.PersianDate();
                item.Description = "Exception: " + ex.Message;
                item.Duration = "";
                item.LogLevel = LogInfo.LogLevel.LOW_DETAIL;
                item.ModuleName = "FPayService";
                item.OutputMessageType = LogInfo.OutputMessageType.ERROR;
                item.OutputType = LogInfo.OutputType.FILE;
                item.SubModule = "FPayService";
                item.Time = Utility.GetTime();
                item.UniqueId = "FPayService";
                LogWorker worker = ServiceInfo.logger.GetLogWorker(ServiceInfo.logInfo);
                worker.WriteLog(item, ServiceInfo.logInfo);
                //********************************
            }
            return (result);
        }

        #endregion

        #region General

        private Response FPayServiceTest(Request request)
        {
            Response response = new Response();
            response.Message = "Ok";
            response.MessageCode = "000000";
            response.Result = "FPayService - Test: " + request.Parameters;
            response.BinaryData = null;
            response.Command = "Complete";
            response.CommandType = request.CommandType;
            response.Schema = "";
            response.RefrenceNo = request.RefrenceNo;
            return (response);
        }

        private Dictionary<string, string> GetParamDic(List<string> arrayParams)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (string par in arrayParams)
            {
                if (par.Trim() == "")
                    continue;
                char[] chList = new char[1];
                chList[0] = '=';
                string[] splited = par.Split(chList, 2);
                result.Add(splited[0].Trim().ToUpper(), splited[1]);
            }
            return (result);
        }

        private Dictionary<string, string> GetParamDic(string[] arrayParams)
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

        private string CorrectDateTimeToPersian(string InputDT)
        {
            string result = "";
            DateTime convertedDT = DateTime.Parse(InputDT);
            result = Utility.EnglishToPersianDate(convertedDT.Year.ToString().PadLeft(4, '0') + "/" +
                convertedDT.Month.ToString().PadLeft(2, '0') + "/" +
                convertedDT.Day.ToString().PadLeft(2, '0')) + " " +
                convertedDT.Hour.ToString().PadLeft(2, '0') + ":" +
                convertedDT.Minute.ToString().PadLeft(2, '0') + ":" +
                convertedDT.Second.ToString().PadLeft(2, '0');
            return (result);
        }

        #endregion

        #region FPay Service

        private Response FPayServiceGetDataList(Request request)
        {
            Response response = CreateResponse(request, "000000");
            MessageObj msg = null;
            try
            {
                //-- Get Parameters
                List<string> paramList = (List<string>)Utility.DeserializeXML_Memory(typeof(List<string>),
                    Encoding.Default.GetBytes(request.Parameters));
                Dictionary<string, string> paramDic = GetParamDic(paramList);
                string encryptedAC = paramDic["SIGN"];
                paramDic.Remove("SIGN");
                //-- Create Response for each item in input params
                List<string> resultList = new List<string>();
                foreach (string key in paramDic.Keys)
                {
                    if (key == "USERNAME" || key == "COMMAND")
                        continue;
                    KeyValueDB kv = new KeyValueDB();
                    kv.KEY_VALUE_GROUP = paramDic[key];
                    DataTable table = KeyValueOperation.GetKeyValueByParameters(kv);
                    var query = (from tableRow in table.AsEnumerable()
                                 select new
                                 {
                                     KEY_VALUE_KEY = tableRow.Field<string>("KEY_VALUE_KEY"),
                                     KEY_VALUE_VALUE = tableRow.Field<string>("KEY_VALUE_VALUE"),
                                 }).ToList();
                    DataTable resultTable = ServiceInfo.ToDataTable(query);
                    resultList.Add(ServiceInfo.CreateStringTable(resultTable));
                }
                //-- Serialize and return data list
                response.Result = Encoding.Default.GetString(Utility.SerializeXML_Memory(typeof(List<string>), resultList));
            }
            catch (Exception ex)
            {
                if (msg == null)
                    msg = ServiceInfo.FPayMessages["ES100001"];
                response.MessageCode = msg.Key;
                response.Message = msg.Value;
                //****************** Do Log Action
                LogItem item = new LogItem();
                item.Action = "FPayServiceGetDataList";
                item.Date = Utility.PersianDate();
                item.Description = "User=" + request.Credential.UserName + " ,Exception: " + ex.Message;
                item.Duration = "";
                item.LogLevel = LogInfo.LogLevel.LOW_DETAIL;
                item.ModuleName = "FPayService";
                item.OutputMessageType = LogInfo.OutputMessageType.ERROR;
                item.OutputType = LogInfo.OutputType.FILE;
                item.SubModule = "FPayService";
                item.Time = Utility.GetTime();
                item.UniqueId = request.RefrenceNo;
                LogWorker worker = ServiceInfo.logger.GetLogWorker(ServiceInfo.logInfo);
                worker.WriteLog(item, ServiceInfo.logInfo);
                //********************************
            }
            return (response);
        }

        private Response FPayServiceRegistrationRequest(Request request)
        {
            Response response = CreateResponse(request, "000000");
            MessageObj msg = null;
            try
            {
                List<string> resultList = new List<string>();
                //-- Get Parameters
                List<string> paramList = (List<string>)Utility.DeserializeXML_Memory(typeof(List<string>),
                    Encoding.Default.GetBytes(request.Parameters));
                Dictionary<string, string> paramDic = GetParamDic(paramList);
                string encryptedAC = paramDic["SIGN"];
                paramDic.Remove("SIGN");
                //-- Check Email and Mobile not exists
                CustomerDB customer = new CustomerDB();
                customer.CUSTOMER_MOBILE = paramDic["MOBILENO"];
                DataTable table = CustomerOperation.GetCustomerByParameters(customer);
                if (table.Rows.Count > 0)
                {
                    msg = ServiceInfo.FPayMessages["FP100006"];
                    throw new Exception("Mobile No has registred before," + customer.CUSTOMER_MOBILE);
                }
                customer.CUSTOMER_MOBILE = "";
                customer.CUSTOMER_EMAIL = paramDic["EMAIL"];
                table = CustomerOperation.GetCustomerByParameters(customer);
                if (table.Rows.Count > 0)
                {
                    msg = ServiceInfo.FPayMessages["FP100007"];
                    throw new Exception("Email has registred before," + customer.CUSTOMER_EMAIL);
                }
                //-- Regioster Customer

                //-- Serialize and return data list
                response.Result = Encoding.Default.GetString(Utility.SerializeXML_Memory(typeof(List<string>), resultList));
            }
            catch (Exception ex)
            {
                if (msg == null)
                    msg = ServiceInfo.FPayMessages["FP100002"];
                response.MessageCode = msg.Key;
                response.Message = msg.Value;
                //****************** Do Log Action
                LogItem item = new LogItem();
                item.Action = "FPayServiceRegistrationRequest";
                item.Date = Utility.PersianDate();
                item.Description = "User=" + request.Credential.UserName + " ,Exception: " + ex.Message;
                item.Duration = "";
                item.LogLevel = LogInfo.LogLevel.LOW_DETAIL;
                item.ModuleName = "FPayService";
                item.OutputMessageType = LogInfo.OutputMessageType.ERROR;
                item.OutputType = LogInfo.OutputType.FILE;
                item.SubModule = "FPayService";
                item.Time = Utility.GetTime();
                item.UniqueId = request.RefrenceNo;
                LogWorker worker = ServiceInfo.logger.GetLogWorker(ServiceInfo.logInfo);
                worker.WriteLog(item, ServiceInfo.logInfo);
                //********************************
            }
            return (response);
        }

        #endregion

        private Response CreateResponse(Request request, string code)
        {
            MessageObj msg = ServiceInfo.FPayMessages[code];
            Response response = new Response();
            response.Message = msg.Value;
            response.MessageCode = msg.Key;
            response.Result = "";
            response.BinaryData = null;
            response.Command = "Complete";
            response.CommandType = request.CommandType;
            response.Schema = "";
            response.RefrenceNo = request.RefrenceNo;
            string test = this.ServiceName;
            return (response);
        }

    }
}
