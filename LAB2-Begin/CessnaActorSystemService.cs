using System;
using Akka.Actor;
using CessnaActorSystem;
using CessnaActorSystem.Actors;
using CessnaActorSystem.Messages;

namespace CessnaActorSystem
{
    public class CessnaActorSystemService
    {
        private ActorSystem cessnaActorSystem;

        public CessnaActorSystemService()
        {
            ColorConsole.WriteLineBlue("Starting CessnaActorSystem");
            this.cessnaActorSystem = ActorSystem.Create("CessnaActorSystem"); 
            this.cessnaActorSystem.ActorOf(Props.Create<ProcessManagerActor>(), "ProcessManager"); 
        }
       
        public void ListenToConsole()
        {
            do
            {
                try
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    ColorConsole.WriteLineGray("enter a command and hit enter");

                    var command = Console.ReadLine();
                    if (command.StartsWith("register"))
                    {
                        string pipelineName = command.Split(',')[1];
                        string installerName = command.Split(',')[2];

                        if (pipelineName == null || installerName == null){
                            throw new ApplicationException("register command must have pipelineName followed by installername provided!");
                        }

                        var message = new PipelineMessage(pipelineName, installerName);
                        //this.cessnaActorSystem.ActorSelection("/user/ProcessManager").Tell(message);
                    }
                    
                    if (command == "exit")
                    {
                        ColorConsole.WriteLineGray("Actor system shutdown");
                        this.cessnaActorSystem.Terminate().Wait();
                        Console.ReadKey();
                        Environment.Exit(1);
                    }
                }
                catch (Exception e)
                {
                    ColorConsole.WriteMagenta(e.Message);
                }

            } while (true);
        }
    }
}