﻿using System;
using System.Collections.Generic;
using System.Text;
using ssr;

namespace SimpleSessionServer.Hosts {

    /// <summary>
    /// 操作交互标识
    /// </summary>
    public class Sid : Basic {

        public Sid(SsrHost host) : base(host) {
        }

        protected override void OnRecieveData(ServerHostRecieveEventArgs e, string command, string data) {
            base.OnRecieveData(e, command, data);

            switch (command) {
                case "@":
                    // 清理存储
                    Server.Storages.CleanUp();
                    // 判断交互标识是否存在
                    if (Server.Storages.ContainsKey(data)) {

                        // 设置交互存储对象
                        var entity = Server.Storages[data];
                        base.SsrHost.StorageEntity = entity;

                        // 更新存储对象操作时间
                        entity.UpdateValidTime();

                        if (Server.IsDebug) Console.WriteLine($"[{Time.GetTimeString()}] -+- 当前存储对象失效时间 {base.SsrHost.StorageEntity.ValidTime}");

                        // 返回信息
                        base.SsrHost.SendSuccess(e, entity.Sid);
                        // 设置为空业务
                        base.SsrHost.SetHostNone();
                    } else {
                        // 发送错误信息
                        base.SsrHost.SendFail(e, "Invalid Sid");
                        // 设置为空业务
                        base.SsrHost.SetHostNone();
                    }
                    break;
                default:
                    if (Server.IsDebug) Console.WriteLine($"> 未知命令类型:{command} 完整语句:{e.Content}");
                    string errMsg = "Unknow Command";
                    e.Entity.Send($"-{errMsg.Length}\r\n{errMsg}");
                    break;
            }
        }

        protected override void OnRecieveCommand(ServerHostRecieveEventArgs e, string command, string info) {
            base.OnRecieveCommand(e, command, info);

            switch (command) {
                // 指定交互标识
                case "@":

                    int len = int.Parse(info);
                    if (len <= 0) {
                        // 申请一个新的Sid
                        var entity = Server.Storages.GetNew();
                        base.SsrHost.StorageEntity = entity;
                        // 返回信息
                        base.SsrHost.SendSuccess(e, entity.Sid);
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
                    string errMsg = "Unknow Command";
                    e.Entity.Send($"-{errMsg.Length}\r\n{errMsg}");
                    break;
            }
        }

    }
}
