using DSharpPlus.CommandsNext;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext.Attributes;

namespace Shuna_chan.Commands
{
    class Intro : BaseCommandModule
    {
        [Command("intro")]
        public async Task IntroCommand(CommandContext ctx)
        {
            await ctx.RespondAsync("Hewwo! My name is Shuna, right now I'm pretty useless. I'm basically retarded.");
        }
    }
}
