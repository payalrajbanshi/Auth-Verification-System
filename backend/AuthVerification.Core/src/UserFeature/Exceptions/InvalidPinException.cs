using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthVerification.Core.src.UserFeature.Exceptions
{
    public class InvalidPinException : Exception
    {
        public InvalidPinException() : base("Invalid vetification pin") { }
    }
}
