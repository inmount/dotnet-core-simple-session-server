using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleSessionServer.Storages {

    /// <summary>
    /// 存储实例
    /// </summary>
    public class Entity : Dictionary<string, string>, IDisposable {

        private Entities _parent;

        /// <summary>
        /// 获取交互标识
        /// </summary>
        public string Sid { get; private set; }

        public Entity(string sid, Entities parent) {
            _parent = parent;
            this.Sid = sid;
        }

        /// <summary>
        /// 获取有效期
        /// </summary>
        public int ValidTime { get; private set; }

        /// <summary>
        /// 更新有效时间
        /// </summary>
        /// <returns></returns>
        public void UpdateValidTime() {
            int ts = Time.GetTimestamp();
            this.ValidTime = ts + _parent.Timeout;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose() {
            this.Sid = null;
            this.ValidTime = 0;
            // 清理所有的子项目
            foreach (string key in this.Keys) {
                this[key] = null;
            }
            this.Clear();
        }
    }
}
