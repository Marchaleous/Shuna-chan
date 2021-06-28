using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Threading.Tasks;

namespace Shuna_chan.Commands
{
    class Coin : BaseCommandModule
    {
        [Command("coin")]
        public async Task CoinCommand(CommandContext ctx)
        {
            Random rand = new Random();
            int flipNum = rand.Next(2);

            await ctx.RespondAsync(flipNum == 0 ? "Heads" : "Tails");
        }
    }
}
