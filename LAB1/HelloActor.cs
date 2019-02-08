using System;
using Akka.Actor;

namespace LAB1
{
    public class HelloActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            Console.WriteLine($"HelloActor just received your nice messages that says '{message.ToString()}'");
        }
    }
}