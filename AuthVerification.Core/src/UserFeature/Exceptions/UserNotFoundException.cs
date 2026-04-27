using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthVerification.Core.src.UserFeature.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public long UserId { get; private set; }
        public UserNotFoundException(long userId)
        {
            UserId = userId;
        }
        public UserNotFoundException(string message) : base(message)
        {

        }

        public override string Message => "User with id " + UserId + " is not found.";
    }
}
