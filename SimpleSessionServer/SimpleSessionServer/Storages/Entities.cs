using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SimpleSessionServer.Storages {

    /// <summary>
    /// 存储集合
    /// </summary>
    public class Entities : Dictionary<string, Entity> {

        // 超时时间
        public int Timeout { get; private set; }

        public Entities(int timeout) {
            this.Timeout = timeout;
        }

        /// <summary>
        /// 清理存储信息
        /// </summary>
        public void CleanUp() {
            int ts = Time.GetTimestamp();

            // 建立清理清单
            List<string> keys = new List<string>();

            foreach (string key in this.Keys) {
                // 判断时间戳，如超时则加入清理清单
                if (this[key].ValidTime < ts) {
                    //this[key].Dispose();
                    //this.Remove(key);
                    keys.Add(key);
                }
            }

            // 清理存储
            foreach (string key in keys) {
                this[key].Dispose();
                this[key] = null;
                this.Remove(key);
            }

            // 释放清理清单
            keys.Clear();
        }

        /// <summary>
        /// 获取一个新的存储实例
        /// </summary>
        /// <returns></returns>
        public Entity GetNew() {

            // 交互标识
            string sid;

            // 如交互标识存在则重新生成
            do {

                // 生成一个新的GUID作为新的交互标识
                sid = Guid.NewGuid().ToString().Replace("-", "");

            } while (this.ContainsKey(sid));

            // 新建一个存储实例
            Entity pairs = new Entity(sid, this);
            // 更新有效时间
            pairs.UpdateValidTime();
            // 添加到集合
            this.Add(sid, pairs);

            // 调试输出
            Debug.WriteLine("-> Entities" + this.Count);

            return pairs;

        }

    }
}
