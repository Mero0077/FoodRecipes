using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    public class HangfireJobs
    {
        public void Log()
        {
            Console.WriteLine("🔥 STATIC LOG TRIGGERED FROM HANGFIRE");
        }
    }


}
