using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.ServiceProcess;
using System.Text;

namespace Service {
    partial class Service1 : ServiceBase {

        private SimpleSessionServer.Server _server;

        public Service1() {
            InitializeComponent();
            _server = new SimpleSessionServer.Server(IPAddress.Any, "000000");
        }

        protected override void OnStart(string[] args) {
            // TODO: 在此处添加代码以启动服务。
            _server.Start();
        }

        protected override void OnStop() {
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
            _server.Stop();
        }
    }
}
