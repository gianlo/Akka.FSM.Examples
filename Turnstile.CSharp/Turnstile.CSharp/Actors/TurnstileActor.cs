using System;
using Akka.Actor;
using Akka.Event;
using Turnstile.CSharp.Messages;

namespace Turnstile.CSharp.Actors
{
    public enum TurnstileStatus
    {
        Locked,
        Unlocked
    }

    public class Coins
    {
        public int Total { get; }

        public Coins(int total)
        {
            Total = total;
        }

        public Coins AddOne()
        {
            return new Coins(Total + 1);
        }

        public static Coins NoCoins()
        {
            return new Coins(0);
        }
    }

    class TurnstileActor: FSM<TurnstileStatus, Coins>
    {
        public TurnstileActor()
        {
            InitializeFsm();
        }

        private void InitializeFsm()
        {
            StartWith(TurnstileStatus.Locked, Coins.NoCoins());
            When(TurnstileStatus.Locked, LockedLogic);
            When(TurnstileStatus.Unlocked, UnlockedLogic);
            Initialize();
        }

        private State<TurnstileStatus, Coins> UnlockedLogic(Event<Coins> fsmevent)
        {
            throw new NotImplementedException();
        }

        private State<TurnstileStatus, Coins> LockedLogic(Event<Coins> fsmevent)
        {
            if (fsmevent.FsmEvent is InsertCoin)
            {
                return GoTo(TurnstileStatus.Unlocked).Using(StateData.AddOne());
            }
            if (fsmevent.FsmEvent is PushBar)
            {
                Context.GetLogger().Warning($"Seriously Buddy? Before you can pass you need to insert a coin! I'm currently {StateName}.");
                return Stay();
            }
            // next return is required to tell the framework that an event was unhandled
            return null;
        }
    }
}
