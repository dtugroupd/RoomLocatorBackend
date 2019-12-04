using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public class ForbiddenException : BaseException
    {
        internal ForbiddenException() : base("No access", "You do not have the required permissions to access this resource.") { }
    }
}
