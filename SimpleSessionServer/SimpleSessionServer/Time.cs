using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleSessionServer {

    /// <summary>
    /// 相关时间处理
    /// </summary>
    public class Time {

        /// <summary>
        /// 获取当前时间戳
        /// </summary>
        /// <returns></returns>
        public static int GetTimestamp() {
            DateTime dtUtc = DateTime.UtcNow;
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0);

            TimeSpan ts = dtUtc - dt;
            return Convert.ToInt32(ts.TotalSeconds);
        }

        /// <summary>
        /// 获取当前完整时间
        /// </summary>
        /// <returns></returns>
        public static string GetString() {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 获取当前时间
        /// </summary>
        /// <returns></returns>
        public static string GetTimeString() {
            return DateTime.Now.ToString("HH:mm:ss");
        }

    }
}
