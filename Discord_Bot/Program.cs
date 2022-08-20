using Discord;
using Discord.Net;
using Discord.WebSocket;
using Newtonsoft.Json;
using System.Text;

namespace Discord_Bot
{
    internal class Program
    {
        public static Task Main(string[] args) => new Program().MainAsync();

        private DiscordSocketClient _client;

        public async Task MainAsync()
        {
            var json = string.Empty;

            using (var fs = File.OpenRead("configjson.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = sr.ReadToEnd();

            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

            _client = new DiscordSocketClient();

            _client.Log += Log;

            await _client.LoginAsync(TokenType.Bot, configJson.Token);
            await _client.StartAsync();
            
            _client.Ready += Client_Ready;
            _client.SlashCommandExecuted += SlashCommandHandler;

            await Task.Delay(-1);
        }

        private async Task SlashCommandHandler(SocketSlashCommand command)
        {
            switch (command.Data.Name)
            {
                case "add-points":
                    await HandleListRoleCommand(command);
                    break;
            }
        }
        private async Task HandleListRoleCommand(SocketSlashCommand command)
        {
            await command.RespondAsync($"Добавленно {command.Data.Options.Last().Value} поинтов пользователю @{command.Data.Options.First().Type}");
        }

        private async Task Client_Ready()
        {
            ulong guildId = 1008457076202295358;

            var guildCommand = new SlashCommandBuilder()
                .WithName("add-points")
                .WithDescription("Добавляет поинты определённому пользователю")
                .AddOption("user", ApplicationCommandOptionType.User, "Введи имя пользователя", isRequired: true)
                .AddOption("points", ApplicationCommandOptionType.Integer, "Введи количество поинтов", isRequired: true);

            try
            {
                await _client.Rest.CreateGuildCommand(guildCommand.Build(), guildId);
            }
            catch (ApplicationCommandException exception)
            {
                var json = JsonConvert.SerializeObject(exception.Errors, Formatting.Indented);

                Console.WriteLine(json);
            }
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}