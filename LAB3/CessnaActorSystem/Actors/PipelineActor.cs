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
        /*
        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy (
                10, // maxNumberOfRetries
                TimeSpan.FromSeconds(30), // withinTimeRange
                x => // localOnlyDecider
                {
                    //Maybe we consider InvalidMessageException to not be application critical
                    //so we just ignore the error and keep going.
                    if (x is InvalidMessageException) return Directive.Resume;

                    //Error that we cannot recover from, stop the failing actor
                    else if (x is NotSupportedException) return Directive.Stop;

                    //In all other cases, just restart the failing actor
                    else return Directive.Restart;
                });
        }
         */

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
            ColorConsole.WriteLineBlue($"PipelineActor for '{this.PipelineName}' PostStop");

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineBlue($"PipelineActor for '{this.PipelineName}' PostStop");

            base.PostRestart(reason);
        }
        #endregion
    }
}