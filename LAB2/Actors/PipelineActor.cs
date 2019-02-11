using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using CessnaActorSystem.Messages;

namespace CessnaActorSystem.Actors
{
    public class PipelineActor : ReceiveActor
    {
        public string PipelineName { get; }
        public IList<IActorRef> Filters { get; private set; }
    
        public PipelineActor(string pipelineName)
        {
            PipelineName = pipelineName;
            this.Filters = BuildPipeline(pipelineName);
            Receive<PipelineMessage>(message => this.Filters.First().Tell(message));
        }

        public static IList<IActorRef> BuildPipeline(string pipelineName)
        {
            var registerInstallerActorRef =  Context.ActorOf(Props.Create(() => new RegisterInstallerActor()), "RegisterInstallerActor");

            var pipeline = new List<IActorRef>();
            pipeline.Add(registerInstallerActorRef);

            return pipeline;
        }

        #region Lifecycle hooks
        protected override void PreStart()
        {
            ColorConsole.WriteLineBlue($"PipelineActor for '{this.PipelineName}' PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineBlue($"PipelineActor for '{this.PipelineName}' PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineBlue($"PipelineActor for '{this.PipelineName}' PreRestart");

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineBlue($"PipelineActor for '{this.PipelineName}' PostRestart");

            base.PostRestart(reason);
        }
        #endregion
    }
}