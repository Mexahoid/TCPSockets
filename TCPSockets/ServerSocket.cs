using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPSocketsServer
{
    class ServerSocket
    {
        private readonly int port;
        private TcpListener tcpListener;
        private NetworkStream networkStream;

        public ServerSocket(int port)
        {
            this.port = port;
        }

        public void Start()
        {
            tcpListener = TcpListener.Create(port);
            tcpListener.Start();
        }

        public async Task ServerWork()
        {
            string req = null;

            while (req != "Stop")
            {
                req = await Listen();
            }
        }

        public async Task<string> Listen()
        {
            if (networkStream == null)
            {
                var tcpClient = await tcpListener.AcceptTcpClientAsync();
                Console.WriteLine("[Server] Client has connected");
                networkStream = tcpClient.GetStream();
            }

            var buffer = new byte[4096];
            Console.WriteLine("[Server] Reading from client");
            int byteCount = await networkStream.ReadAsync(buffer, 0, buffer.Length);
            string request = Encoding.UTF8.GetString(buffer, 0, byteCount);
            Console.WriteLine("[Server] Client wrote {0}", request);

            byte[] serverResponseBytes = Encoding.UTF8.GetBytes($"{(request == "Stop" ? "End" : $"Your message received : \"{request}\"")}");

            await networkStream.WriteAsync(serverResponseBytes, 0, serverResponseBytes.Length);
            Console.WriteLine("[Server] Response has been written");
            return request;
        }


        public void Stop()
        {
            tcpListener.Stop();
        }

    }
}
