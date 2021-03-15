using System;
using AiSoft.Tools.Extensions;
using Microsoft.Win32;

namespace AiSoft.Wpf.Helpers
{
    public class WinHelper
    {
        /// <summary> 
        /// 开机启动项
        /// </summary> 
        /// <param name="name">启动值的名称</param> 
        /// <param name="executablePath">启动程序的路径</param> 
        /// <param name="isDelete">是否删除</param>
        public static void RunWhenStart(string name, string executablePath, bool isDelete = false)
        {
            //var regName = @"SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Run";
            var regName1 = @"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
            //var regKey = Registry.LocalMachine.OpenSubKey(regName, true) ?? Registry.LocalMachine.OpenSubKey(regName1, true);
            var regKey = Registry.CurrentUser.OpenSubKey(regName1, true) ?? Registry.CurrentUser.CreateSubKey(regName1);
            if (isDelete)
            {
                regKey.DeleteValue(name, false);
            }
            else
            {
                regKey.SetValue(name, executablePath);
            }
            regKey.Close();
        }

        /// <summary>
        /// 开启远程桌面
        /// </summary>
        /// <param name="isRun"></param>
        public static void RunTerminalServer(bool isRun = true)
        {
            var regKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\\CurrentControlSet\\Control\\Terminal Server", true);
            regKey.SetValue("fDenyTSConnections", isRun ? 0 : 1, RegistryValueKind.DWord);
            regKey.Close();
        }

        /// <summary>
        /// 设置远程桌面端口
        /// </summary>
        /// <param name="port"></param>
        public static void SetTerminalServerPort(int port)
        {
            var regKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\\CurrentControlSet\\Control\\Terminal Server\\WinStations\\RDP-Tcp", true);
            regKey.SetValue("PortNumber", port, RegistryValueKind.DWord);
            regKey.Close();

            regKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\\CurrentControlSet\\Control\\Terminal Server\\Wds\\rdpwd\\Tds\\tcp", true);
            regKey.SetValue("PortNumber", port, RegistryValueKind.DWord);
            regKey.Close();
        }

        /// <summary>
        /// 返回远程桌面端口
        /// </summary>
        /// <returns></returns>
        public static int GetTerminalServerPort()
        {
            var regKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\\CurrentControlSet\\Control\\Terminal Server\\WinStations\\RDP-Tcp", true);
            var port = regKey.GetValue("PortNumber", RegistryValueKind.DWord).JsonSerialize();
            regKey.Close();
            return port.ToInt32();
        }
    }
}