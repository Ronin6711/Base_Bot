using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base_Bot
{
    public class BotCommands : BaseCommandModule
    {
        [Command("ping")]
        [RequireRoles(RoleCheckMode.Any, "Админ")]
        public async Task Ping(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Pong").ConfigureAwait(false);

        }

        [Command("respondmessage")]
        [RequireRoles(RoleCheckMode.Any, "Админ")]
        public async Task RespondEmoji(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();

            var yes = DiscordEmoji.FromName(ctx.Client, ":+1:");
            var no = DiscordEmoji.FromName(ctx.Client, ":-1:");

            var message = await interactivity.WaitForReactionAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);
            //if (message.Result.Emoji == "{😢}")
            if(message.Result.Emoji == yes)
                await ctx.Channel.SendMessageAsync("Бан нахуй");
            else
                await ctx.Channel.SendMessageAsync("");

        }
        [Command($"base")]
        [RequireRoles(RoleCheckMode.Any, "Админ")]
        public async Task BaseOrCringe(CommandContext ctx, string userName)
        {
            var interactivity = ctx.Client.GetInteractivity();

            var user = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);

            var joinEmbed = new DiscordEmbedBuilder
            {
                Title = "База или кринж?",
                ImageUrl = user.Result.Content,
                Color = DiscordColor.Red
            };

            var joinMassege = await ctx.Channel.SendMessageAsync(embed: joinEmbed).ConfigureAwait(false);

            var yes = DiscordEmoji.FromName(ctx.Client, ":+1:");
            var no = DiscordEmoji.FromName(ctx.Client, ":-1:");

            await joinMassege.CreateReactionAsync(yes).ConfigureAwait(false);
            await joinMassege.CreateReactionAsync(no).ConfigureAwait(false);


        }
    }
}
