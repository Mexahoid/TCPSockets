using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPSocketsClient
{
    class Program
    {
        private static ClientSocket cs;

        static void Main(string[] args)
        {
            cs = new ClientSocket("127.0.0.1", 1488);

            cs.Connect();
            
            cs.ClientWork(Console.ReadLine).GetAwaiter().GetResult();


            cs.Disconnect();
        }
    }
}
