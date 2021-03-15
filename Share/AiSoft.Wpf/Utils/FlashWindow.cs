using System;
using System.Runtime.InteropServices;
using System.Windows;
#if (NETCOREAPP || NET)
    using System.Windows.Interop;
#endif
#if NETFRAMEWORK
    using System.Windows.Forms;
#endif
using AiSoft.Tools.Api;

namespace AiSoft.Wpf.Utils
{
    /// <summary>
    /// 任务栏闪烁
    /// </summary>
    public class FlashWindow
    {
#if (NETCOREAPP || NET)

        /// <summary>
        /// 开始闪烁
        /// </summary>
        /// <param name="window"></param>
        public static void Start(Window window)
        {
            var handle = new WindowInteropHelper(window).Handle;
            if (window.WindowState == WindowState.Minimized || handle != WinApi.GetForegroundWindow())

#endif

#if NETFRAMEWORK

        /// <summary>
        /// 开始闪烁
        /// </summary>
        /// <param name="winForm"></param>
        public static void Start(Form winForm)
        {
            var handle = winForm.Handle;
            if (winForm.WindowState == FormWindowState.Minimized || handle != WinApi.GetForegroundWindow())
#endif

            {
                var fInfo = new WinApi.FLASHWINFO();
                fInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(fInfo));
                fInfo.hwnd = handle;
                fInfo.dwFlags = WinApi.FLASHW_TRAY | WinApi.FLASHW_TIMERNOFG;
                fInfo.uCount = UInt32.MaxValue;
                fInfo.dwTimeout = 0;
                WinApi.FlashWindowEx(ref fInfo);
            }
        }
    }
}