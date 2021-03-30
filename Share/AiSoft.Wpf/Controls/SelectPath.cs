using System;
using System.Windows;
using System.Windows.Forms;
using AiSoft.Wpf.Enums;
using Button = System.Windows.Controls.Button;
using Control = System.Windows.Controls.Control;

namespace AiSoft.Wpf.Controls
{
    [TemplatePart(Name = "SelectBtn", Type = typeof(Button))]
    public class SelectPath : Control
    {
        public static readonly DependencyProperty PathProperty = DependencyProperty.Register("Path", typeof(string), typeof(SelectPath), new PropertyMetadata(string.Empty));
        
        /// <summary>
        /// 选择的路径
        /// </summary>
        public string Path
        {
            get => (string)GetValue(PathProperty);
            set => SetValue(PathProperty, value);
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

        static SelectPath()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SelectPath), new FrameworkPropertyMetadata(typeof(SelectPath)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var btn = GetTemplateChild("SelectBtn") as Button;
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
            var dlg = new SaveFileDialog { Filter = Filter, FileName = Path };
            var res = dlg.ShowDialog() == DialogResult.OK;
            if (!res)
            {
                return;
            }
            Path = dlg.FileName;
        }

        /// <summary>
        /// 选择文件
        /// </summary>
        private void OpenSelectFileDialog()
        {
            var dlg = new OpenFileDialog { Filter = Filter, FileName = Path };
            var res = dlg.ShowDialog() == DialogResult.OK;
            if (!res)
            {
                return;
            }
            Path = dlg.FileName;
        }

        /// <summary>
        /// 选择文件夹
        /// </summary>
        private void OpenSelectFolderDialog()
        {
            var dlg = new FolderBrowserDialog { SelectedPath = Path };
            var res = dlg.ShowDialog() == DialogResult.OK;
            if (!res)
            {
                return;
            }
            Path = dlg.SelectedPath;
        }
    }
}