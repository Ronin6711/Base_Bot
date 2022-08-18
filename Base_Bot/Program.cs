using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace Base_Bot
{
    class Program
    {
        //DiscordSocketClient client;
        //static void Main(string[] args)
        //    => new Program().MainAsync().GetAwaiter().GetResult();

        //private async Task MainAsync()
        //{
        //    client = new DiscordSocketClient();
        //    client.MessageReceived += ComandsHandl;
        //    client.Log += Log;

        //    var token = "MTAwODQ1NjU1NDAzMjQwNjY0OQ.GeERGz.2AuCWXmr3ZkWrxrWRTK4jBD9kHqfjWhOPx2Nqs";

        //    await client.LoginAsync(TokenType.Bot, token);
        //    await client.StartAsync();
        //    Console.ReadLine();
        //}

        //private Task Log(LogMessage msg)
        //{
        //    Console.WriteLine(msg.ToString());
        //    return Task.CompletedTask;
        //}

        //private Task ComandsHandl(SocketMessage msg)
        //{
        //    if (!msg.Author.IsBot)
        //        switch (msg.Content)
        //        {
        //            case "Дота":
        //                {
        //                    msg.Channel.SendMessageAsync("Ну и говно");
        //                    break;
        //                }
        //        }
        //    return Task.CompletedTask;
        //}
        static void Main(string[] args)
        {
            var bot = new Bot();
            bot.RunAsync().GetAwaiter().GetResult();
        }
    }
}