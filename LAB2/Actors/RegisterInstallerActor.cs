using System;
using Akka.Actor;
using DeploymentPipeline.Messages;

namespace DeploymentPipeline.Actors
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
                throw new ApplicationException("Invalid version name!");
            }

            ColorConsole.WriteLineGreen($"{DateTime.Now} - Registring installer '{message.InstallerName}'");
        }
    }
}