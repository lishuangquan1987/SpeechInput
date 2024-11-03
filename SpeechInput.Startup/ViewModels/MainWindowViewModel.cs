using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Interop;
using SpeechInput.Common.Helpers;
using System.Drawing;

namespace SpeechInput.Startup.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        const int WS_EX_TOOLWINDOW = 0x00000080;
        const int WS_EX_NOACTIVATE = 0x08000000;
        const int GWL_EXSTYLE = -20;

        private DateTime _lastPressedTime;
        private bool _isPressed = false;
        private Point _lastPoint;
        public MainWindowViewModel()
        {
            StartOrStopRecordCmd = new RelayCommand(StartOrStopRecord, () => this.IsConnected);
            this.LoadedCmd = new RelayCommand(Loaded);
            this.MouseLeftButtonDownCmd = new RelayCommand<MouseButtonEventArgs>(MouseLeftButtonDown);
            this.MouseLeftButtonUpCmd = new RelayCommand<MouseButtonEventArgs>(MouseLeftButtonUp);
            this.MouseMoveCmd = new RelayCommand(MouseMove);
        }

        private void MouseMove()
        {
            if (_isPressed)
            {
                User32Helper.GetCursorPos(out var point);

                App.Current.MainWindow.Left = App.Current.MainWindow.Left + point.X - _lastPoint.X;
                App.Current.MainWindow.Top = App.Current.MainWindow.Top + point.Y - _lastPoint.Y;

                //赋值
                _lastPoint = point;
            }
        }

        private void MouseLeftButtonUp(MouseButtonEventArgs? e)
        {
            _isPressed = false;
            //长按的话，不响应Button点击事件
            if ((DateTime.Now - _lastPressedTime).TotalSeconds > 0.5)
            {
                e.Handled = true;
            }
        }

        private void MouseLeftButtonDown(MouseButtonEventArgs? e)
        {
            _isPressed = true;
            _lastPressedTime = DateTime.Now;

            User32Helper.GetCursorPos(out _lastPoint);
        }

        private void Loaded()
        {
            var helper = new WindowInteropHelper(App.Current.MainWindow);

            int GWL_EXSTYLE = -20;

            var style = User32Helper.GetWindowLong(helper.Handle, GWL_EXSTYLE);

            style |= (WS_EX_NOACTIVATE | WS_EX_TOOLWINDOW);

            User32Helper.SetWindowLong(helper.Handle, GWL_EXSTYLE, new IntPtr(style));

            App.Current.MainWindow.Left = System.Windows.SystemParameters.PrimaryScreenWidth - App.Current.MainWindow.Width;
            App.Current.MainWindow.Top = System.Windows.SystemParameters.PrimaryScreenHeight / 2;
        }

        private async void StartOrStopRecord()
        {
            if (!IsConnected)
            {
                System.Windows.MessageBox.Show("未连接到服务器", "错误", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return;
            }
            if (IsRecording)
            {
                await speechInput.Stop();
                IsRecording = false;
            }
            else
            {
                await speechInput.Start();
                IsRecording = true;
            }
        }

        ~MainWindowViewModel()
        {
            speechInput.Unload();
        }
        /// <summary>
        /// 是否正在录音
        /// </summary>
        [ObservableProperty] private bool _isRecording;
        /// <summary>
        /// 是否连接上了服务端
        /// </summary>
        [NotifyCanExecuteChangedFor(nameof(StartOrStopRecordCmd))]
        [ObservableProperty] private bool _isConnected;

        public RelayCommand StartOrStopRecordCmd { get; }
        public RelayCommand<MouseButtonEventArgs> MouseLeftButtonDownCmd { get; }
        public RelayCommand<MouseButtonEventArgs> MouseLeftButtonUpCmd { get; }
        public RelayCommand MouseMoveCmd { get; }
        public RelayCommand LoadedCmd { get; }
    }

}
