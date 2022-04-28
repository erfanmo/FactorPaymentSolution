using FrameworkBaseService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace FPayWebInterface.Services
{
    public static class Instances
    {

        private static ServiceOperation serviceOperation = null;
        public const string CacheKey = "FPayService";

        public static void CreateInstances()
        {
            var ctx = HttpContext.Current;
            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    serviceOperation = new ServiceOperation();
                    for (int i = 0; i < 10; i++)
                    {
                        int nextId = serviceOperation.GetNextId();
                        List<string> parameters = new List<string>();
                        parameters.Add("Instance" + i.ToString());
                        FPayService service = new FPayService("FPayService" + nextId.ToString(), ConfigurationManager.AppSettings["FrameworkPath"], nextId, parameters);
                        serviceOperation.AddService(service);
                    }
                    FPayService AutomationService = (FPayService)serviceOperation.GetBaseService();
                    AutomationService.InitializeService();
                }
                ctx.Cache[CacheKey] = serviceOperation;
            }
        }

    }
}