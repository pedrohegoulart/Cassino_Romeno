using System;

namespace Cassino.Entities.Exceptions
{
    internal class DomainException : ApplicationException
    {
        public DomainException(string message) : base(message)
        { 
        }
    }
}
