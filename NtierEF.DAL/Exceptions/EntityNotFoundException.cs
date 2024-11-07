using System;

namespace NtierEF.DAL.Exceptions 
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message)
            : base(message)
        {
        }

        public EntityNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public EntityNotFoundException()
            : base("The requested entity was not found.")
        {
        }
    }
}
