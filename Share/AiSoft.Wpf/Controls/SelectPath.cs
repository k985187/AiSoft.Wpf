using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using AiSoft.Wpf.Enums;
using AiSoft.Wpf.Helpers;
using Application = System.Windows.Application;
using Button = System.Windows.Controls.Button;
using Control = System.Windows.Controls.Control;

namespace AiSoft.Wpf.Controls
{
    public class SelectPath : Control
    {
        public static readonly DependencyProperty OpenPathProperty = DependencyProperty.Register("OpenPath", typeof(string), typeof(SelectPath), new PropertyMetadata(string.Empty));
        
        /// <summary>
        /// 选择的路径
        /// </summary>
        public string OpenPath
        {
            get => (string)GetValue(OpenPathProperty);
            set => SetValue(OpenPathProperty, value);
        }

        public static readonly DependencyProperty FilterProperty = DependencyProperty.Register("Filter", typeof(string), typeof(SelectPath), new PropertyMetadata("All|*.*"));
        
        /// <summary>
        /// 文件格式过滤器
        /// </summary>
        public string Filter
        {
            get => (string)GetValue(FilterProperty);
            set => SetValue(FilterProperty, value);
        }

        public static readonly DependencyProperty SelectPathModeProperty = DependencyProperty.Register("SelectPathMode", typeof(SelectPathModeEnum), typeof(SelectPath), new PropertyMetadata(SelectPathModeEnum.SelectFile));
        
        /// <summary>
        /// 选择格式
        /// </summary>
        public SelectPathModeEnum SelectPathMode
        {
            get => (SelectPathModeEnum)GetValue(SelectPathModeProperty);
            set => SetValue(SelectPathModeProperty, value);
        }

        public static readonly DependencyProperty ButtonStyleProperty = DependencyProperty.Register("ButtonStyle", typeof(Style), typeof(SelectPath), new PropertyMetadata(null));

        /// <summary>
        /// 按钮样式
        /// </summary>
        public Style ButtonStyle
        {
            get => (Style)GetValue(ButtonStyleProperty);
            set => SetValue(ButtonStyleProperty, value);
        }

        public static readonly DependencyProperty IsOwnerProperty = DependencyProperty.Register("IsOwner", typeof(bool), typeof(SelectPath), new PropertyMetadata(true));

        /// <summary>
        /// 是否设置所有者
        /// </summary>
        public bool IsOwner
        {
            get => (bool)GetValue(IsOwnerProperty);
            set => SetValue(IsOwnerProperty, value);
        }

        static SelectPath()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SelectPath), new FrameworkPropertyMetadata(typeof(SelectPath)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var btn = FrameworkHelper.GetChildObject<Button>(this, "SelectBtn");
            if (btn != null)
            {
                btn.Click += (s, e) =>
                {
                    switch (SelectPathMode)
                    {
                        case SelectPathModeEnum.SelectFile:
                            OpenSelectFileDialog();
                            break;
                        case SelectPathModeEnum.SelectFolder:
                            OpenSelectFolderDialog();
                            break;
                        case SelectPathModeEnum.SaveFile:
                            OpenSaveFileDialog();
                            break;
                    }
                };
            }
        }

        /// <summary>
        /// 设置保存的文件名称
        /// </summary>
        private void OpenSaveFileDialog()
        {
            var dlg = new SaveFileDialog { Filter = Filter, FileName = string.IsNullOrWhiteSpace(OpenPath) ? "" : Path.GetFileName(OpenPath), InitialDirectory = string.IsNullOrWhiteSpace(OpenPath) ? AppDomain.CurrentDomain.BaseDirectory : Path.GetDirectoryName(OpenPath) };
            DialogResult result;
            if (IsOwner)
            {
                var winPtr = WpfHelper.GetHandleByDependencyObject(Application.Current.MainWindow);
                var win = new WinFormWindow(winPtr);
                result = dlg.ShowDialog(win);
            }
            else
            {
                result = dlg.ShowDialog();
            }
            if (result != DialogResult.OK)
            {
                return;
            }
            OpenPath = dlg.FileName;
        }

        /// <summary>
        /// 选择文件
        /// </summary>
        private void OpenSelectFileDialog()
        {
            var dlg = new OpenFileDialog { Filter = Filter, FileName = string.IsNullOrWhiteSpace(OpenPath) ? "" : Path.GetFileName(OpenPath), InitialDirectory = string.IsNullOrWhiteSpace(OpenPath) ? AppDomain.CurrentDomain.BaseDirectory : Path.GetDirectoryName(OpenPath) };
            DialogResult result;
            if (IsOwner)
            {
                var winPtr = WpfHelper.GetHandleByDependencyObject(Application.Current.MainWindow);
                var win = new WinFormWindow(winPtr);
                result = dlg.ShowDialog(win);
            }
            else
            {
                result = dlg.ShowDialog();
            }
            if (result != DialogResult.OK)
            {
                return;
            }
            OpenPath = dlg.FileName;
        }

        /// <summary>
        /// 选择文件夹
        /// </summary>
        private void OpenSelectFolderDialog()
        {
            var dlg = new FolderBrowserDialog { SelectedPath = string.IsNullOrWhiteSpace(OpenPath) ? "" : Path.GetDirectoryName(OpenPath) };
            DialogResult result;
            if (IsOwner)
            {
                var winPtr = WpfHelper.GetHandleByDependencyObject(Application.Current.MainWindow);
                var win = new WinFormWindow(winPtr);
                result = dlg.ShowDialog(win);
            }
            else
            {
                result = dlg.ShowDialog();
            }
            if (result != DialogResult.OK)
            {
                return;
            }
            OpenPath = dlg.SelectedPath;
        }
    }

    internal class WinFormWindow : IWin32Window
    {
        IntPtr _handle;

        IntPtr IWin32Window.Handle => _handle;

        public WinFormWindow(IntPtr handle)
        {
            _handle = handle;
        }
    }
}