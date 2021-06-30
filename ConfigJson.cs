using Newtonsoft.Json;

namespace Shuna_chan
{
    public struct ConfigJson
    {
        [JsonProperty("botName")]
        public string BotName { get; private set; }

        [JsonProperty("author")]
        public string Author { get; private set; }

        [JsonProperty("authorId")]
        public string AuthorId { get; private set; }

        [JsonProperty("discordToken")]
        public string DiscordToken { get; private set; }

        [JsonProperty("youtubeApi")]
        public string YoutubeApi { get; private set; }

        [JsonProperty("prefix")]
        public string Prefix { get; private set; }

        [JsonProperty("defaultVolume")]
        public string DefaultVolume { get; private set; }

        [JsonProperty("defaultActivity")]
        public string DefaultActivity { get; private set; }

        [JsonProperty("defaultStatus")]
        public string defaultStatus { get; private set; }
    }
}
