using System;
using System.ComponentModel;
using System.Windows;

namespace AiSoft.Wpf.Helpers
{
    public class WpfHelper
    {
        /// <summary>
        /// 是否设计模式
        /// </summary>
        public static bool IsInDesignMode => (bool)DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue;
    }
}