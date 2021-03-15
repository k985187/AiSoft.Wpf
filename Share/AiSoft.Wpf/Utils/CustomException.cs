using System;
using System.Threading.Tasks;
using System.Windows.Threading;
using AiSoft.Tools.Helpers;

namespace AiSoft.Wpf.Utils
{
    /// <summary>
    /// 自定义错误类
    /// </summary>
    public class CustomException
    {
        /// <summary>
        /// 处理UI线程中某个异常未被捕获时出现的异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Current_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            LogHelper.WriteLog(e.Exception);
            e.Handled = true;
        }
    }
}