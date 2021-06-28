using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.VoiceNext;
using Shuna_chan.Commands;

namespace Shuna_chan
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = Credentials.discordToken,
                TokenType = TokenType.Bot
            });

            discord.UseVoiceNext();

            var commands = discord.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = new[] { Credentials.prefix }
            });

            // commands.RegisterCommands<Play>();
            commands.RegisterCommands<Coin>();
            commands.RegisterCommands<Intro>();
            commands.RegisterCommands<Join>();
            commands.RegisterCommands<Leave>();
            commands.RegisterCommands<SetActivity>();
            // commands.RegisterCommands<SetAction>();
            // commands.RegisterCommands<SetStatus>();

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }

    }
}
