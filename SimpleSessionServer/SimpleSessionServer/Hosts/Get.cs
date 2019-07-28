using System;
using System.Collections.Generic;
using System.Text;
using ssr;

namespace SimpleSessionServer.Hosts {

    /// <summary>
    /// 操作交互标识
    /// </summary>
    public class Get : Basic {

        public Get(SsrHost host) : base(host) {
        }

        protected override void OnRecieveData(ServerHostRecieveEventArgs e, string command, string data) {
            base.OnRecieveData(e, command, data);

            switch (command) {
                // 设置名称
                case "$":
                    // 判断名称是否存在
                    if (base.SsrHost.StorageEntity.ContainsKey(data)) {
                        // 修改存储
                        base.SsrHost.SendSuccess(e, base.SsrHost.StorageEntity[data]);
                    } else {
                        // 添加存储
                        base.SsrHost.SendSuccess(e);
                    }
                    // 设置为空业务
                    base.SsrHost.SetHostNone();
                    break;
                default:
                    if (Server.IsDebug) Console.WriteLine($"> 未知命令类型:{command}");
                    base.SsrHost.SendFail(e, "Unknow Command");
                    break;
            }
        }

        protected override void OnRecieveCommand(ServerHostRecieveEventArgs e, string command, string info) {
            base.OnRecieveCommand(e, command, info);

            switch (command) {
                // 设置名称
                case "$":
                    int len = int.Parse(info);
                    if (len <= 0) {
                        if (Server.IsDebug) Console.WriteLine($"> 名称长度为0");
                        base.SsrHost.SendFail(e, "Unknow Name");
                    } else {
                        // 设置读取模式，读取Sid
                        base.Command = command;
                        e.Entity.SetDataMode(len);
                    }
                    break;
                default:
                    if (Server.IsDebug) Console.WriteLine($"> 未知命令类型:{command} 完整语句:{e.Content}");
                    base.SsrHost.SendFail(e, "Unknow Command");
                    break;
            }
        }

    }
}
