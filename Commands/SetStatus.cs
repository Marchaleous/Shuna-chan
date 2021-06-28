using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace Shuna_chan.Commands
{
    class SetStatus : BaseCommandModule
    {
        [Command("setstatus"), Description("Sets bot activity.")]
        public async Task SetStatusCommand(CommandContext ctx, [RemainingText] string name)
        {

        }

    }
}
