using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace Shuna_chan.Commands
{
    class SetAction : BaseCommandModule
    {
        [Command("setaction"), Description("Sets bot action to Competing, ListeningTo, Playing, Streaming, or Watching.")]
        public async Task SetActionCommand(CommandContext ctx, [RemainingText] string type)
        {
            
        }

    }
}
