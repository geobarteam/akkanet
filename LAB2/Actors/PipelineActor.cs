using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using DeploymentPipeline.Messages;

namespace DeploymentPipeline.Actors
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
    }
}