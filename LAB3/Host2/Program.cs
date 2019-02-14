using CessnaActorSystem;
using System;

namespace Host1
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
