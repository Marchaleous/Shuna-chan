using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Lavalink;
using System.Linq;
using System.Threading.Tasks;

namespace Shuna_chan.Commands
{
    // Pause, Stop, Skip, Queue, Clear, NowPlaying
    public class MusicCommands : BaseCommandModule
    {
        [Command("pause"), Description("Pauses the current song.")]
        public async Task Pause(CommandContext ctx)
        {
            if (ctx.Member.VoiceState == null || ctx.Member.VoiceState.Channel == null)
            {
                await ctx.RespondAsync("You are not in a voice channel.");
                return;
            }

            var lava = ctx.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values.First();
            var conn = node.GetGuildConnection(ctx.Member.VoiceState.Guild);

            if (conn == null)
            {
                await ctx.RespondAsync("Lavalink is not connected.");
                return;
            }

            if (conn.CurrentState.CurrentTrack == null)
            {
                await ctx.RespondAsync("There are no tracks loaded.");
                return;
            }

            await conn.PauseAsync();
        }
    }
}
