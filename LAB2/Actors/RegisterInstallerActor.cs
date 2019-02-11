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
                throw new ApplicationException("Invalid version name!");
            }

            ColorConsole.WriteLineGreen($"{DateTime.Now} - Registring installer '{message.InstallerName}'");
        }

        #region Lifecycle hooks
        protected override void PreStart()
        {
            ColorConsole.WriteLineBlue("RegisterInstallerActor PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineBlue("RegisterInstallerActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineBlue("RegisterInstallerActor PreRestart because: " + reason);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineBlue("RegisterInstallerActor PostRestart because: " + reason);

            base.PostRestart(reason);
        }
        
        #endregion
    }
}