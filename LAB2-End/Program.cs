using System;
using Akka.Actor;
using CessnaActorSystem.Actors;
using CessnaActorSystem.Messages;

namespace CessnaActorSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var cessnaActorSystemService = new CessnaActorSystemService();
            cessnaActorSystemService.ListenToConsole();
        }
    }
}
