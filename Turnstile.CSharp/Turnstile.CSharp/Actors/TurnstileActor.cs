using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

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

        private Coins(int total)
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
        }

        private State<TurnstileStatus, Coins> UnlockedLogic(Event<Coins> fsmevent)
        {
            throw new NotImplementedException();
        }

        private State<TurnstileStatus, Coins> LockedLogic(Event<Coins> fsmevent)
        {
            throw new NotImplementedException();
        }
    }
}
