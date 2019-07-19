using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SimpleSessionServer {

    /// <summary>
    /// 服务端
    /// </summary>
    public class Server : ssr.Server {

        /// <summary>
        /// 存储集合
        /// </summary>
        internal static Storages.Entities Storages;

        /// <summary>
        /// 连接密码
        /// </summary>
        internal static string Password;

        // 服务对象
        private ssr.Server _server;

        /// <summary>
        /// 创建一个服务端实例
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="pwd"></param>
        /// <param name="port"></param>
        /// <param name="timeout"></param>
        public Server(IPAddress ip, string pwd, int port = 8601, int timeout = 3600) : base(typeof(SsrHost), ip, port) {

            Server.Storages = new Storages.Entities(timeout);
            Server.Password = pwd;

        }

        /// <summary>
        /// 创建一个服务端实例
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="pwd"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static Server Build(IPAddress ip, string pwd, int port = 8601, int timeout = 3600) {
            return new Server(ip, pwd, port, timeout);
        }

    }
}
