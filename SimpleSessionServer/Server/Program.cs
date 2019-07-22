using System;
using System.Net;

namespace Server {
    class Program {
        static void Main(string[] args) {

            // 以标准端参数口启动服务
            SimpleSessionServer.Server.Build(IPAddress.Any, "000000", 8601, 60).Run();

        }
    }
}
