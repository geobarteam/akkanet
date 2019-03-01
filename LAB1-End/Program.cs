using System;
using Akka.Actor;

namespace LAB1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initialize ActorSystem & Actor
            var labActorSystem = ActorSystem.Create("LabActorSystem"); 
            var helloActor = labActorSystem.ActorOf(Props.Create(() => new HelloActor()));
            
            Console.WriteLine("Say something nice:");            
            var input = Console.ReadLine();
            
            //Send message to actor
            helloActor.Tell(input);
            
            Console.WriteLine("Type enter to exit!");
            Console.ReadLine();
        }
    }
}
