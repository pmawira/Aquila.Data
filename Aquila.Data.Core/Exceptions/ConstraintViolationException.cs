using System;
using System.Collections.Generic;
using System.Text;

namespace Aquila.Data.Core.Exceptions
{
    public class ConstraintViolationException: Exception
    {
        public ConstraintViolationException(string message): base(message){}
    }
}
