using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Interop;

namespace AiSoft.Wpf.Helpers
{
    public class WpfHelper
    {
        /// <summary>
        /// 是否设计模式
        /// </summary>
        public static bool IsInDesignMode => (bool)DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue;

        /// <summary>
        /// 获取控件句柄
        /// </summary>
        /// <param name="dependencyObject"></param>
        /// <returns></returns>
        public static IntPtr GetHandleByDependencyObject(DependencyObject dependencyObject)
        {
            var hwndSource = (HwndSource)PresentationSource.FromDependencyObject(dependencyObject);
            var handle = hwndSource.Handle;
            return handle;
        }
    }
}