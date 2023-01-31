using LawinServer.Net;
using System;
using static LawinServer.Win32;

namespace LawinServer
{
    internal class Program
    {
        private SetConsoleCtrlEventHandler setConsoleCtrlEventHandler;
        private Proxy proxy;
        private static void Main(string[] args)
        {
            ushort? port = null;
            string url = null;
            if (args.Length == 2)
            {
                if (ushort.TryParse(args[0], out ushort portValue))
                {
                    port = portValue;
                }
                url = args[1];
            }
            Console.WriteLine("Thanks for using LawinServer made by Lawin#0001 :D\n" +
                "This launcher was made for LawinServer by PsychoPast.\n" +
                "Running... PLEASE, don't close the window else the connection will be aborted.");
            new Program().Run(port, url);
            Console.ReadLine();
        }

        private void Run(ushort? port, string url)
        {
            setConsoleCtrlEventHandler = CleanUp;
            SetConsoleCtrlHandler(setConsoleCtrlEventHandler, true);
            proxy = port switch
            {
                null => new Proxy(),
                _ => new Proxy((ushort)port, url)
            };
            proxy.StartProxy();
        }

        private bool CleanUp(CtrlType ctrlType)
        {
            switch (ctrlType)
            {
                case CtrlType.CTRL_BREAK_EVENT:
                case CtrlType.CTRL_CLOSE_EVENT:
                case CtrlType.CTRL_C_EVENT:
                case CtrlType.CTRL_LOGOFF_EVENT:
                case CtrlType.CTRL_SHUTDOWN_EVENT:
                    proxy.StopProxy();
                    return true;
                default:
                    return false;
            }
        }
    }
}