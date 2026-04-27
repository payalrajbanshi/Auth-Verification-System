using AuthVerification.Core.src.UserFeature.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthVerification.Core.src.UserFeature.Services
{
    public class SmsService : ISmsService
    {
        public Task SendAsync(string mobileNo, string message)
        {
            Console.WriteLine($"Sending sms to {mobileNo}: {message}");
            return Task.CompletedTask;
        }

    }
}
