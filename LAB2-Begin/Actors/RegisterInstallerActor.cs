using System;
using Akka.Actor;
using CessnaActorSystem.Messages;

namespace CessnaActorSystem.Actors
{
    public class RegisterInstallerActor: ReceiveActor
    {
        public RegisterInstallerActor()
        {
            Receive<PipelineMessage>(message => HandleRegisterInstallerActor(message));
        }

        private void HandleRegisterInstallerActor(PipelineMessage message)
        {
            if (!message.InstallerName.Contains("-")){
                throw new InvalidMessageException("Invalid message");
            }

            ColorConsole.WriteLineGreen($"{DateTime.Now} - Registring installer '{message.InstallerName}'");
        }

       
    }
}