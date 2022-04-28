using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonObjects;

namespace FrameworkBaseService
{
    public interface BaseServiceInterface
    {

        Response DoJob(Request request);

        bool InitializeService();

    }
}
