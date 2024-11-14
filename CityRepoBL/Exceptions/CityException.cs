using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityRepoBL.Exceptions
{
    public class CityException : Exception
    {
        public CityException(string? message) : base(message)
        {
        }

        public CityException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
