using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPSocketsClient
{
    class ClientSocket
    {
        private readonly string addr;
        private readonly int port;
        private TcpClient tcpClient;
        private NetworkStream networkStream;

        public ClientSocket(string addr, int port)
        {
            this.addr = addr;
            this.port = port;
        }

        public async void Connect()
        {
            tcpClient = new TcpClient();
            Console.WriteLine("[Client] Connecting to server");
            await tcpClient.ConnectAsync(addr, port);
            Console.WriteLine("[Client] Connected to server");
            networkStream = tcpClient.GetStream();
        }

        public async Task ClientWork(Func<string> messageGetter)
        {
            string res = null;
            while (res != "End")
            {
                res = await SendMessage(messageGetter.Invoke());
                Console.WriteLine("[Client] Server response was {0}", res);
            }
        }

        public async Task<string> SendMessage(string message)
        {

            Console.WriteLine($"[Client] Writing request {message}");
            var bytes = Encoding.UTF8.GetBytes(message);
            await networkStream.WriteAsync(bytes, 0, bytes.Length);

            var buffer = new byte[4096];
            var byteCount = await networkStream.ReadAsync(buffer, 0, buffer.Length);
            //networkStream.Flush();
            return Encoding.UTF8.GetString(buffer, 0, byteCount);

        }

        public void Disconnect()
        {
            if (networkStream != null)
            {
                networkStream.Flush();
                networkStream.Close();
            }

            if (tcpClient != null && tcpClient.Connected)
                tcpClient.Close();
        }
    }
}
