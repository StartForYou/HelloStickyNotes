using Hardcodet.Wpf.TaskbarNotification;
using HelloStickyNotes.ViewModels;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace HelloStickyNotes
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        private MainViewModel mViewModel;
        private readonly DispatcherTimer mTimer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(5) };

        private bool isExiting = false;
        private bool tipMininuzedToTray = false;

        //private const string MutexName = "Global\\MyAppMutex"; // 确保这个名称是唯一的就行
        //private Mutex mutex;

        public MainWindow()
        {
            InitializeComponent();
            this.mViewModel = new MainViewModel();
            this.DataContext = mViewModel;
            this.mTimer.Tick += MTimer_Tick;
            this.mTimer.Start();

            this.Closing += OnClosing;

        }

        private void MTimer_Tick(object sender, EventArgs e)
        {
            this.mViewModel.Tick();
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            /*bool isOwned;
            if (mutex == null)
            {
                mutex = new Mutex(true, MutexName, out isOwned);
            }*/
            if (Application.Current is App)
            {
                if ((Application.Current as App).ExistOthers())
                {
                    // 已存在其他实例
                    TrayIcon.Visibility = Visibility.Hidden;
                    ShowMainWindow();
                    return;
                }
            }
            if (isExiting)
            {
                return;
            }
            e.Cancel = true; // 阻止窗口关闭
            this.WindowState = WindowState.Minimized; // 最小化窗口到任务栏（可选）
            this.Hide();
            TrayIcon.Visibility = Visibility.Visible;
            if (!tipMininuzedToTray)
            {
                tipMininuzedToTray = true;
                TrayIcon.ShowBalloonTip("提示", "应用程序已最小化到托盘", BalloonIcon.None);
            }
        }

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                if (Mouse.LeftButton == MouseButtonState.Pressed)
                {
                    var windowMode = this.ResizeMode;
                    if (this.ResizeMode != ResizeMode.NoResize)
                    {
                        this.ResizeMode = ResizeMode.NoResize;
                    }

                    this.UpdateLayout();
                    /* 当点击拖拽区域的时候，让窗口跟着移动
                    (When clicking the drag area, make the window follow) */
                    DragMove();

                    if (this.ResizeMode != windowMode)
                    {
                        this.ResizeMode = windowMode;
                    }
                    this.UpdateLayout();
                }

            }
        }

        private void ShowMainWindow()
        {
            Show();
            WindowState = WindowState.Normal;
            Activate();
            //TrayIcon.Visibility = Visibility.Collapsed;
        }

        private void TrayIcon_TrayLeftMouseDown(object sender, RoutedEventArgs e)
        {
            ShowMainWindow();
        }

        private void TrayItemShow_Click(object sender, RoutedEventArgs e)
        {
            ShowMainWindow();
        }

        private void TrayItemClose_Click(object sender, RoutedEventArgs e)
        {
            isExiting = true;
            //TrayIcon.Visibility = Visibility.Hidden;
            foreach (var window in Application.Current.Windows)
            {
                (window as Window).Close();
            }
            Application.Current.Shutdown();
        }
    }
}
