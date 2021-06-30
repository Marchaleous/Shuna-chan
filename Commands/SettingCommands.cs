using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;

namespace Shuna_chan.Commands
{
    // Activity, Activity Type, Status, Volume, ChangePrefix
    class SettingCommands : BaseCommandModule
    {
        [Command("setactivity"), Description("Sets bot activity.")]
        public async Task SetActivity(CommandContext ctx, [RemainingText] string name)
        {

            DiscordActivity activity = new DiscordActivity
            {
                Name = name
            };

            DiscordClient discord = ctx.Client;

            await discord.UpdateStatusAsync(activity);
        }
    }
}
