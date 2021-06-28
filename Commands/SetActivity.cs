using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace Shuna_chan.Commands
{
    class SetActivity : BaseCommandModule
    {
        [Command("setactivity"), Description("Sets bot activity.")]
        public async Task SetActivityCommand(CommandContext ctx, [RemainingText] string name)
        {
            DiscordActivity activity = new DiscordActivity();
            DiscordClient discord = ctx.Client;
            activity.Name = name;

            await discord.UpdateStatusAsync(activity);
        }

    }
}
