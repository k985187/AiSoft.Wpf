using System;
using System.Windows;

namespace AiSoft.Wpf.Helpers
{
    public class DispatcherHelper
    {
        /// <summary>
        /// 线程同步给主线程
        /// </summary>
        /// <param name="action"></param>
        public static void Invoke(Action action)
        {
            try
            {
                Application.Current?.Dispatcher?.Invoke(action);
            }
            catch
            {
            }
        }

        /// <summary>
        /// 线程异步给主线程
        /// </summary>
        /// <param name="action"></param>
        public static void BeginInvoke(Action action)
        {
            try
            {
                Application.Current?.Dispatcher?.BeginInvoke(action);
            }
            catch
            {
            }
        }
    }
}