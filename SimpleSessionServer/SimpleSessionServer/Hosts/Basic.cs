using System;
using System.Collections.Generic;
using System.Text;
using ssr;

namespace SimpleSessionServer.Hosts {

    /// <summary>
    /// 业务宿主基类
    /// </summary>
    public abstract class Basic : IDisposable, ssr.IHost {

        /// <summary>
        /// 相关的SsrHost
        /// </summary>
        protected SsrHost SsrHost { get; private set; }

        /// <summary>
        /// 获取或设置关联命令
        /// </summary>
        protected string Command { get; set; }

        /// <summary>
        /// 实例化一个业务宿主
        /// </summary>
        /// <param name="host"></param>
        public Basic(SsrHost host) {
            this.SsrHost = host;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose() {
            this.SsrHost = null;
        }

        /// <summary>
        /// 获取命令
        /// </summary>
        /// <param name="e"></param>
        /// <param name="command"></param>
        /// <param name="data"></param>
        protected virtual void OnRecieveCommand(ServerHostRecieveEventArgs e, string command, string info) { }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="e"></param>
        /// <param name="command"></param>
        /// <param name="info"></param>
        protected virtual void OnRecieveData(ServerHostRecieveEventArgs e, string command, string data) { }

        public void OnRecieve(HostRecieveEventArgs e) {
            // 此demo进行了一次简单的定长数据获取示例
            ServerHostRecieveEventArgs args = (ServerHostRecieveEventArgs)e;
            if (args.Entity.DataMode) {
                // 数据模式
                OnRecieveData(args, this.Command, e.Content);
            } else {
                // 命令模式
                OnRecieveCommand(args, e.Content.Substring(0, 1), e.Content.Substring(1));
            }
        }

    }
}
