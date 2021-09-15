using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FJP.CarStorage.Exceptions
{
    public class CarDoesntExistsException : Exception
    {
        public CarDoesntExistsException() : base("This car doesn't exists!")
        {

        }
    }
}
