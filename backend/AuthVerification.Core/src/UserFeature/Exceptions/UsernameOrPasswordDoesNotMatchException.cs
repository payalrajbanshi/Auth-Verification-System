using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthVerification.Core.src.UserFeature.Exceptions
{
    public class UsernameOrPasswordDoesNotMatchException : Exception
    {
        public UsernameOrPasswordDoesNotMatchException() : base("Current password doesnot match")
        {

        }
    }
}
