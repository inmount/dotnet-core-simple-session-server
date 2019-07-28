using System;
using System.Collections.Generic;
using System.Text;
using ssr;

namespace SimpleSessionServer.Hosts {

    /// <summary>
    /// 操作交互标识
    /// </summary>
    public class Set : Basic {

        // 名称缓存
        private string _name;

        public Set(SsrHost host) : base(host) {
            _name = null;
        }

        protected override void OnRecieveData(ServerHostRecieveEventArgs e, string command, string data) {
            base.OnRecieveData(e, command, data);

            switch (command) {
                // 设置名称
                case "$":
                    _name = data;
                    break;
                // 设置值
                case "&":
                    // 判断名称是否定义
                    if (_name == null) {
                        if (Server.IsDebug) Console.WriteLine($"> 名称未定义");
                        base.SsrHost.SendFail(e, "None Name");
                        // 设置为空业务
                        base.SsrHost.SetHostNone();
                        return;
                    }

                    // 判断名称是否存
                    if (base.SsrHost.StorageEntity.ContainsKey(_name)) {
                        // 修改存储
                        base.SsrHost.StorageEntity[_name] = data;
                    } else {
                        // 添加存储
                        base.SsrHost.StorageEntity.Add(_name, data);
                    }

                    // 更新有效时间
                    base.SsrHost.StorageEntity.UpdateValidTime();

                    // 向客户端发送结果
                    base.SsrHost.SendSuccess(e);

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
                // 设置值
                case "&":
                    len = int.Parse(info);
                    if (len <= 0) {

                        // 判断名称是否定义
                        if (_name == null) {
                            if (Server.IsDebug) Console.WriteLine($"> 名称未定义");
                            base.SsrHost.SendFail(e, "None Name");
                            // 设置为空业务
                            base.SsrHost.SetHostNone();
                            return;
                        }

                        // 判断名称是否存
                        if (base.SsrHost.StorageEntity.ContainsKey(_name)) {
                            // 修改存储
                            base.SsrHost.StorageEntity[_name] = "";
                        } else {
                            // 添加存储
                            base.SsrHost.StorageEntity.Add(_name, "");
                        }

                        // 更新有效时间
                        base.SsrHost.StorageEntity.UpdateValidTime();

                        // 向客户端发送结果
                        base.SsrHost.SendSuccess(e);

                        // 设置为空业务
                        base.SsrHost.SetHostNone();
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
