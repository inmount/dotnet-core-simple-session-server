﻿using System;
using System.Collections.Generic;
using System.Text;
using ssr;

namespace SimpleSessionServer {

    /// <summary>
    /// 业务宿主引导宿主
    /// 每一个客户端连接，都将实例化一个新的引导宿主
    /// </summary>
    public class SsrHost : ssr.IHost {

        // 实际业务相关宿主
        private ssr.IHost _host = null;

        /// <summary>
        /// 获取或设置交互信息存储实体
        /// </summary>
        public Storages.Entity StorageEntity { get; set; }

        /// <summary>
        /// 设置为空业务
        /// </summary>
        public void SetHostNone() {
            _host = null;
        }

        /// <summary>
        /// 检测存储对象
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool CheckStorageEntity(HostRecieveEventArgs e) {
            // 当交互存储不存在时，触发致命错误
            if (this.StorageEntity == null) {
                Console.WriteLine($"> 进行{e.Content}业务时发生致命错误：交互存储不存在");
                var args = (ServerHostRecieveEventArgs)e;
                this.SendFail(args, "None Sid");
                System.Threading.Thread.Sleep(10);
                args.Entity.Close();
                return false;
            }
            return true;
        }

        public void OnRecieve(HostRecieveEventArgs e) {

            // 日志式输出
            Console.WriteLine($">> {e.Content}");

            // 判断业务宿主是否为空
            if (_host == null) {
                switch (e.Content) {
                    // 操作交互标识业务
                    case "SID":
                        _host = new Hosts.Sid(this);
                        break;
                    // 设置业务
                    case "SET":
                        // 检测存储对象
                        if (!CheckStorageEntity(e)) return;
                        _host = new Hosts.Set(this);
                        break;
                    // 获取业务
                    case "GET":
                        // 检测存储对象
                        if (!CheckStorageEntity(e)) return;
                        _host = new Hosts.Get(this);
                        break;
                    default:
                        Console.WriteLine($"> 不支持的业务类型:{e.Content}");
                        break;
                }
            } else {
                // 存在业务宿主则直接将事件传递给业务宿主
                _host.OnRecieve(e);
            }


        }

        /// <summary>
        /// 发送成功信息
        /// </summary>
        /// <param name="e"></param>
        /// <param name="data"></param>
        public void SendSuccess(ServerHostRecieveEventArgs e, string data = null) {
            if (data == null) {
                e.Entity.Send($"+0\r\n");
            } else {
                byte[] bs = System.Text.Encoding.UTF8.GetBytes(data);
                e.Entity.Sendln($"+{bs.Length}");
                e.Entity.Send(bs);
            }
        }

        /// <summary>
        /// 发送失败信息
        /// </summary>
        /// <param name="e"></param>
        /// <param name="data"></param>
        public void SendFail(ServerHostRecieveEventArgs e, string data = null) {
            if (data == null) {
                e.Entity.Send($"-0\r\n");
            } else {
                byte[] bs = System.Text.Encoding.UTF8.GetBytes(data);
                e.Entity.Sendln($"-{bs.Length}");
                e.Entity.Send(bs);
            }
        }

    }
}
