using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Threading.Tasks;

namespace Shuna_chan.Commands
{
    // Help, About, CoinToss, Purge, Restart, Terminate
    class UtilityCommands : BaseCommandModule
    {
        [Command("cointoss"), Description("Flips a two sided coin.")]
        public async Task CoinToss(CommandContext ctx)
        {
            Random rand = new Random();
            int flipNum = rand.Next(2);

            await ctx.RespondAsync(flipNum == 0 ? "Heads" : "Tails");
        }
    }
}
