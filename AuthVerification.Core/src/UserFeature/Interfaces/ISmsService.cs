using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthVerification.Core.src.UserFeature.Interfaces
{
    public interface ISmsService
    {
        Task SendAsync(string mobilenumber, string message);
    }
}
