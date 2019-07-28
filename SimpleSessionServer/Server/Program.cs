using System;
using System.Net;

namespace Server {
    class Program {
        static void Main(string[] args) {

            bool debug = false;

            for (int i = 0; i < args.Length; i++) {
                switch (args[i]) {
                    case "-debug":
                        debug = true;
                        break;
                }
            }

            // 以标准端参数口启动服务
            Console.WriteLine($"[+] 服务启动");
            Console.WriteLine($"[+] 调试状态 {debug}");
            SimpleSessionServer.Server.IsDebug = debug;
            SimpleSessionServer.Server.Build(IPAddress.Any, "000000", 8601, 60).Run();

        }
    }
}
