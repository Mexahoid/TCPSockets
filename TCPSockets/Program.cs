using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPSocketsServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerSocket sc = new ServerSocket(1488);
            sc.Start();
            
            sc.ServerWork().GetAwaiter().GetResult();

            sc.Stop();
        }
    }
}
