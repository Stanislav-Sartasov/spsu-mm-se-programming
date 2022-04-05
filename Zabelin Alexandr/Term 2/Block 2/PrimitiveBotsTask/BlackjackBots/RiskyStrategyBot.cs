using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackBots
{
    public class RiskyStrategyBot : APlayer
    {
        public RiskyStrategyBot(float startBalance) : base(startBalance)
        {
        }

        public override bool DoesHit(byte croupierCard)
        {
            return this.Score < 17;
        }
    }
}
