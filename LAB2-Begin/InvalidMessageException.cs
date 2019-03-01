using System;

namespace CessnaActorSystem
{
    public class InvalidMessageException : ApplicationException
    {
        public InvalidMessageException(string msg):base(msg) {

        }
    }
}