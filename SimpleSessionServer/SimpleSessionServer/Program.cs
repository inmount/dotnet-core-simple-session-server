using System;
using System.Net;

namespace SimpleSessionServer {
    internal class Program {

        /// <summary>
        /// 存储集合
        /// </summary>
        internal static Storages.Entities Storages;

        /// <summary>
        /// 连接密码
        /// </summary>
        internal static string Password;

        static void Main(string[] args) {

            Storages = new Storages.Entities(3600);

            Password = "000000";

            // 建立一个新的服务端
            ssr.Server.Build(
                // 关联宿主对象类型
                typeof(SsrHost),
                // 绑定IP
                IPAddress.Any,
                // 设置服务端口,默认使用8601
                8601
                // 以独占模式运行服务，如果需要配合其他服务运行，可使用Start开始同步服务模式
                ).Run();

        }
    }
}
