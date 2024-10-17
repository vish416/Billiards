using System;
using System.Collections.Generic;

namespace BilliardsGame
{
    public class EmojiManager
    {
        private List<string> availableEmojis = new List<string> { "😄", "😎", "😭", "😠", "👍", "👎" };

        public void ShowQuickReactEmojis()
        {
            Console.WriteLine("Quick React Emojis:");
            for (int i = 0; i < availableEmojis.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {availableEmojis[i]}");
            }
        }

        public void SendEmoji(int emojiIndex, Player sender, Player receiver)
        {
            if (emojiIndex < 1 || emojiIndex > availableEmojis.Count)
            {
                Console.WriteLine("Invalid emoji selection.");
                return;
            }

            string emoji = availableEmojis[emojiIndex - 1];
            Console.WriteLine($"{sender.Name} sent {emoji} to {receiver.Name}.");
            receiver.ReceiveEmoji(emoji, sender);
        }
    }

    public class Player
    {
        public string Name { get; set; }

        public void ReceiveEmoji(string emoji, Player sender)
        {
            Console.WriteLine($"{Name} received {emoji} from {sender.Name}.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Player player1 = new Player { Name = "John" };
            Player player2 = new Player { Name = "Jane" };

            EmojiManager emojiManager = new EmojiManager();

            Console.WriteLine("John wants to send an emoji to Jane during the game.");
            emojiManager.ShowQuickReactEmojis();

            Console.WriteLine("Select an emoji to send (1-6):");
            int emojiIndex = int.Parse(Console.ReadLine());
            emojiManager.SendEmoji(emojiIndex, player1, player2);
        }
    }
}
