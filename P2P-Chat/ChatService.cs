using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace P2P_Chat
{
    public class ChatService
    {
        delegate void AddMessage(string message);

        public string userName;

        const int port = 54545;
        const string broadcastAddress = "255.255.255.255";

        UdpClient receivingClient;
        UdpClient sendingClient;

        public ChatService(string userName)
        {
            this.userName = userName;
        }

        public void Initialize()
        {
            InitSender();
            InitReceiver();
        }

        public void SendMessage(string ms)
        {
            string toSend = $"[{userName}]: {ms}";
            byte[] data = Encoding.ASCII.GetBytes(toSend);
            sendingClient.Send(data, data.Length);
        }

        private void InitReceiver()
        {
            receivingClient = new UdpClient(port);

            ThreadStart start = new ThreadStart(Receiver);
            var receivingThread = new Thread(start);
            receivingThread.IsBackground = true;
            receivingThread.Start();
        }

        private void InitSender()
        {
            sendingClient = new UdpClient(broadcastAddress, port);
            sendingClient.EnableBroadcast = true;
        }

        private void Receiver()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
            AddMessage messageDelegate = MessageReceived;

            while (true)
            {
                byte[] data = receivingClient.Receive(ref endPoint);
                string message = Encoding.ASCII.GetString(data);
                messageDelegate?.Invoke(message);
            }
        }

        private void MessageReceived(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("(room) " + message);
            Console.ResetColor();
        }
    }
}
