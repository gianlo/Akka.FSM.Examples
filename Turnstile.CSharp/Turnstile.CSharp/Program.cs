using System;
using Akka.Actor;
using Turnstile.CSharp.Actors;
using Turnstile.CSharp.Messages;

namespace Turnstile.CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var system = ActorSystem.Create("TurnstileSimulator");
            var turnstileActor = system.ActorOf<TurnstileActor>("Turnstile");
            turnstileActor.Tell(new PushBar());
            Console.ReadLine();
        }
    }
}
