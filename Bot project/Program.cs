using System.Net.Security;
using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Microsoft.Extensions.Configuration;
using Bot_project;
using static System.Runtime.InteropServices.JavaScript.JSType;
//امیرمهدی علی پورتاجانی
TelegramBotClient myBot;
int updatesOffset = 0;
string filePath = Path.Combine("Token.txt");

//about secrets.json
IConfigurationBuilder configBuilder = new ConfigurationBuilder();
configBuilder.AddUserSecrets<Program>();
IConfiguration config = configBuilder.Build();
string TOKEN = config["Token"];
//end
 int lastMessageId = 0;
int lastMessageId2 = 0;
 bool isactive = false;
myBot = new TelegramBotClient(TOKEN);
while (true)
{
    var updates = await myBot.GetUpdatesAsync(updatesOffset);

    foreach (var update in updates)
    {
        updatesOffset = update.Id + 1;

        var message = update.Message != null ? update.Message : null;
        var text = update?.Message?.Text != null ? update.Message.Text : null;
        var chatId = update?.Message?.Chat.Id;
        var messageId = update?.Message?.MessageId;

        int number;
        
        if (message != null && text != null)
        {
            

            if (Regex.IsMatch(text, @"\/[SsTtAaRrTt]"))
            {
                isactive = true;
                await myBot.SendTextMessageAsync(chatId, $"hello:)", replyToMessageId: messageId);
            }
            else if (text == "/crypto" && isactive)
            {
                lastMessageId = message.MessageId;
                await myBot.SendTextMessageAsync(chatId, await Cryptocurrency.CryptoAsync(), replyToMessageId: messageId);

            }
            else if ((int.TryParse(text, out number) && isactive) && message.MessageId == lastMessageId + 2)
            {

                await myBot.SendTextMessageAsync(chatId, await Cryptocurrency.CryptoAsync(int.Parse(text)), replyToMessageId: messageId);

            }
            else if (text == "/movie" && isactive)
            {
                lastMessageId2 = message.MessageId;
                await myBot.SendTextMessageAsync(chatId, await Movie.PupMovie(), replyToMessageId: messageId);
            }
            else if ((int.TryParse(text, out number) && isactive) && message.MessageId == lastMessageId2 + 2)
            {
                await myBot.SendTextMessageAsync(chatId, await Movie.PupMovie(int.Parse(text)), replyToMessageId: messageId);
            }
            else if (text == "/joke" && isactive)
            {
                var joke = await Jokes.RandomJokes();
                string? chert = "";
               
                    chert += $"joke:{joke.value}\n{joke.icon_url}";
               
                await myBot.SendTextMessageAsync(chatId, chert , replyToMessageId: messageId);
            }
            else if (text == "/now" && isactive)   
            {
                await myBot.SendTextMessageAsync(chatId, DateTime.Now.ToString(), replyToMessageId: messageId);
            }
            else if (text == "/exit" && isactive)
            {
                isactive = false;
                await myBot.SendTextMessageAsync(chatId, "bot stoped!", replyToMessageId: messageId);

            }
            else if (isactive)
            {
                await myBot.SendTextMessageAsync(chatId, "invalid", replyToMessageId: messageId);
            }
            

        }

    }
}



