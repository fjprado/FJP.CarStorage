using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FJP.CarStorage.Exceptions
{
    public class CarAlreadyExistsException : Exception
    {
        public CarAlreadyExistsException() : base("This car already exists!")
        {

        }
    }
}
