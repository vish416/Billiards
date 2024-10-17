using System;
using System.Collections.Generic;

namespace BilliardsGame
{
    public class Player
    {
        public string Name { get; set; }
        public List<Player> Friends { get; private set; } = new List<Player>();
        public List<FriendRequest> PendingRequests { get; private set; } = new List<FriendRequest>();

        public void SendFriendRequest(Player recipient)
        {
            if (recipient == null || recipient == this)
            {
                Console.WriteLine("Invalid recipient.");
                return;
            }

            FriendRequest request = new FriendRequest(this, recipient);
            recipient.PendingRequests.Add(request);
            Console.WriteLine($"{Name} sent a friend request to {recipient.Name}.");
        }

        public void AcceptFriendRequest(FriendRequest request)
        {
            if (PendingRequests.Contains(request))
            {
                Friends.Add(request.Sender);
                request.Sender.Friends.Add(this);
                PendingRequests.Remove(request);
                Console.WriteLine($"{Name} accepted a friend request from {request.Sender.Name}.");
            }
            else
            {
                Console.WriteLine("Friend request not found.");
            }
        }

        public void RejectFriendRequest(FriendRequest request)
        {
            if (PendingRequests.Contains(request))
            {
                PendingRequests.Remove(request);
                Console.WriteLine($"{Name} rejected a friend request from {request.Sender.Name}.");
            }
            else
            {
                Console.WriteLine("Friend request not found.");
            }
        }

        public void ViewPendingRequests()
        {
            Console.WriteLine($"{Name}'s Pending Friend Requests:");
            foreach (var request in PendingRequests)
            {
                Console.WriteLine($"Request from: {request.Sender.Name}");
            }
        }
    }

    public class FriendRequest
    {
        public Player Sender { get; private set; }
        public Player Recipient { get; private set; }

        public FriendRequest(Player sender, Player recipient)
        {
            Sender = sender;
            Recipient = recipient;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Player player1 = new Player { Name = "John" };
            Player player2 = new Player { Name = "Jane" };
            Player player3 = new Player { Name = "Alice" };

            player1.SendFriendRequest(player2);
            player2.ViewPendingRequests();

            FriendRequest request = player2.PendingRequests[0];
            player2.AcceptFriendRequest(request);

            player1.SendFriendRequest(player3);
            player3.ViewPendingRequests();
            player3.RejectFriendRequest(player3.PendingRequests[0]);
        }
    }
}
