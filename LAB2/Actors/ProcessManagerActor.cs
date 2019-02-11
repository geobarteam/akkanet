using Akka.Actor;
using DeploymentPipeline.Messages;
using System.Collections.Generic;

namespace DeploymentPipeline.Actors
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

                ColorConsole.WriteLineCyan($"ProcessManager created new pipeline for '{pipelineName}' (Total Pipelines: '{_pipelines.Count}')");
            }
        }

        #region Lifecycle hooks
        protected override void PreStart()
        {
            ColorConsole.WriteLineGreen("ProcessManager PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineGreen("ProcessManager PostStop");
        }

        #endregion
    }
}