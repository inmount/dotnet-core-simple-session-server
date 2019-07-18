using System;
using System.Collections.Generic;
using System.Text;
using ssr;

namespace SimpleSessionServer.Hosts {

    /// <summary>
    /// 连接密码验证
    /// </summary>
    public class Pwd : Basic {

        public Pwd(SsrHost host) : base(host) {
        }

        protected override void OnRecieveData(ServerHostRecieveEventArgs e, string command, string data) {
            base.OnRecieveData(e, command, data);

            switch (command) {
                case "$":
                    // 检测密码
                    if (data == Program.Password) {
                        // 设置登录状态
                        base.SsrHost.IsLogin = true;
                        base.SsrHost.SendSuccess(e);
                        // 重置业务
                        base.SsrHost.SetHostNone();
                    } else {
                        // 发送错误信息
                        base.SsrHost.SendFail(e, "Invalid Pwd");
                        System.Threading.Thread.Sleep(10);
                        e.Entity.Close();
                    }
                    break;
                default:
                    Console.WriteLine($"> 未知命令类型:{command} 完整语句:{e.Content}");
                    string errMsg = "Unknow Command";
                    e.Entity.Send($"-{errMsg.Length}\r\n{errMsg}");
                    break;
            }
        }

        protected override void OnRecieveCommand(ServerHostRecieveEventArgs e, string command, string info) {
            base.OnRecieveCommand(e, command, info);

            switch (command) {
                // 指定连接密码
                case "$":
                    int len = int.Parse(info);
                    if (len <= 0) {
                        // 发送错误信息
                        base.SsrHost.SendFail(e, "Invalid Pwd");
                        System.Threading.Thread.Sleep(10);
                        e.Entity.Close();
                        return;
                    }

                    // 设置读取模式，读取密码
                    base.Command = command;
                    e.Entity.SetDataMode(len);
                    break;
                default:
                    Console.WriteLine($"> 未知命令类型:{command} 完整语句:{e.Content}");
                    string errMsg = "Unknow Command";
                    e.Entity.Send($"-{errMsg.Length}\r\n{errMsg}");
                    break;
            }
        }

    }
}
