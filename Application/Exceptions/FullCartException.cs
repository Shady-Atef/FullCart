using Application.Enums;

namespace Application.Exceptions
{
    public class FullCartException : Exception
    {
        public FullCartException(ValidationKey message)
            : base(((int)message).ToString())
        {
        }

        public FullCartException(string message)
            : base(message)
        {
        }
    }
}
