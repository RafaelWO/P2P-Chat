using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2P_Chat
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter Username: ");
            string input = "";
            while (string.IsNullOrEmpty(input))
            {
                input = Console.ReadLine();
            }
            Console.WriteLine("Welcome " + input);
            var chatService = new ChatService(input);

            Console.WriteLine("Initializing chat service...");
            chatService.Initialize();

            Console.WriteLine("Done!\n-----------------------------");
            input = "";
            while (!input.Equals("exit"))
            {
                //Console.Write(">");
                input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    chatService.SendMessage(input);
                }
            }
        }
    }
}
