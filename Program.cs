using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.VoiceNext;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shuna_chan.Commands;

namespace Shuna_chan
{
    public class Program
    {
        public readonly EventId BotEventId = new EventId(02, "Shuna-chan");
        public DiscordClient Client { get; set; }
        public CommandsNextExtension Commands { get; set; }
        public VoiceNextExtension Voice { get; set; }

        public static void Main(string[] args)
        {
            // Passes execution to asynchronous code
            var prog = new Program();
            prog.RunBotAsync().GetAwaiter().GetResult();
        }

        public async Task RunBotAsync()
        {
            // Loads configuration file
            var json = "";
            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync();

            // Loads values from configuration
            var cfgjson = JsonConvert.DeserializeObject<ConfigJson>(json);
            var cfg = new DiscordConfiguration
            {
                Token = cfgjson.DiscordToken,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = LogLevel.Debug,
            };

            // Instantiates client
            this.Client = new DiscordClient(cfg);

            // Hooks events
            this.Client.Ready += this.Client_Ready;
            this.Client.GuildAvailable += this.Client_GuildAvailable;
            this.Client.ClientErrored += this.Client_ClientErrored;

            // Sets up commands
            var ccfg = new CommandsNextConfiguration
            {
                // Loads prefix from config.json
                StringPrefixes = new[] {cfgjson.Prefix},

                // Enables responding in direct messages
                EnableDms = true,

                // Enables mentioning the bot as a command prefix
                EnableMentionPrefix = true
            };

            // Hooks commands
            this.Commands = this.Client.UseCommandsNext(ccfg);
            this.Commands.CommandExecuted += this.Commands_CommandExecuted;
            this.Commands.CommandErrored += Commands_CommandsErrored;

            // Registers commands
            // this.Commands.RegisterCommands<MusicCommands>();
            this.Commands.RegisterCommands<SettingCommands>();
            this.Commands.RegisterCommands<UtilityCommands>();
            this.Commands.RegisterCommands<VoiceCommands>();

            // Enables voice
            this.Voice = this.Client.UseVoiceNext();

            // Connects and logs in
            await this.Client.ConnectAsync();

            // Prevents premature quitting
            await Task.Delay(-1);
        }

        private Task Client_Ready(DiscordClient sender, ReadyEventArgs e)
        {
            // Logs ready
            sender.Logger.LogInformation(BotEventId, "Client is ready to process events.");
            return Task.CompletedTask; ;
        }

        private Task Client_GuildAvailable(DiscordClient sender, GuildCreateEventArgs e)
        {
            // Logs guild name sent to client
            sender.Logger.LogInformation(BotEventId, $"Guild available: {e.Guild.Name}");
            return Task.CompletedTask;
        }

        private Task Client_ClientErrored(DiscordClient sender, ClientErrorEventArgs e)
        {
            // Logs error details
            sender.Logger.LogError(BotEventId, e.Exception, "Exception occured");
            return Task.CompletedTask;
        }

        private Task Commands_CommandExecuted(CommandsNextExtension sender, CommandExecutionEventArgs e)
        {
            // Logs command name and user
            e.Context.Client.Logger.LogInformation(BotEventId, $"{e.Context.User.Username} successfully executed '{e.Command.QualifiedName}'");
            return Task.CompletedTask;
        }

        private async Task Commands_CommandsErrored(CommandsNextExtension sender, CommandErrorEventArgs e)
        {
            // Logs error details
            e.Context.Client.Logger.LogError(BotEventId, $"{e.Context.User.Username} tried executing '{e.Command?.QualifiedName ?? "<unknown command>"}' but it errored: {e.Exception.GetType()}: {e.Exception.Message ?? "<no message>"}", DateTime.Now);

            // Check if due to lack of required permissions
            if (e.Exception is ChecksFailedException)
            {
                var emoji = DiscordEmoji.FromName(e.Context.Client, ":no_entry:");

                // Responds with embed
                var embed = new DiscordEmbedBuilder
                {
                    Title = "Access denied",
                    Description = $"{emoji} You do not have the permissions required to execute this command.",
                    Color = new DiscordColor(0xFF0000)
                };

                await e.Context.RespondAsync(embed);
            }
        }
    }
}
