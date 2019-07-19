using System;
using System.ServiceProcess;

namespace Service {
    class Program {
        static void Main(string[] args) {
            ServiceBase[] services = new ServiceBase[] { new Service1() };
            ServiceBase.Run(services);
        }
    }
}
