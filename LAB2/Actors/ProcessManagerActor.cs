using Akka.Actor;
using CessnaActorSystem.Messages;
using System;
using System.Collections.Generic;

namespace CessnaActorSystem.Actors
{
    public class ProcessManagerActor : ReceiveActor
    {
        private readonly Dictionary<string, IActorRef> _pipelines;
        public ProcessManagerActor()
        {
             _pipelines = new Dictionary<string, IActorRef>();

            Receive<PipelineMessage>(message =>
            {
                CreatePipeLineIfNotExist(message.PipelineName);
                IActorRef pipelineActorRef = _pipelines[message.PipelineName];
                pipelineActorRef.Tell(message);
            });
        }

        public void CreatePipeLineIfNotExist(string pipelineName)
        {
            if (!_pipelines.ContainsKey(pipelineName))
            {
                IActorRef newChildActorRef =
                    Context.ActorOf(Props.Create(() => new PipelineActor(pipelineName)), "Pipeline" + pipelineName);

                _pipelines.Add(pipelineName, newChildActorRef);

                ColorConsole.WriteLineGreen($"ProcessManager created new pipeline for '{pipelineName}' (Total Pipelines: '{_pipelines.Count}')");
            }
        }       

        #region Lifecycle hooks
        protected override void PreStart()
        {
            ColorConsole.WriteLineBlue("ProcessManagerActor PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineBlue("ProcessManagerActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineBlue("ProcessManager PreRestart because: " + reason);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineBlue("ProcessManager PostRestart because: " + reason);

            base.PostRestart(reason);
        }

        #endregion
    }
}