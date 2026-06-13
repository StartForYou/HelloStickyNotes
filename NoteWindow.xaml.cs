using HelloStickyNotes.Misc;
using HelloStickyNotes.Models;
using HelloStickyNotes.Utils;
using HelloStickyNotes.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace HelloStickyNotes
{
    /// <summary>
    /// NoteWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NoteWindow : Window
    {

        private long _activeTime = 0L;

        public NoteWindow()
        {
            InitializeComponent();
            this.KeyDown += NoteWindow_KeyDown;
            this.Activated += NoteWindow_Activated;
        }

        

        private void NoteWindow_Activated(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            _activeTime = DateTime.Now.Ticks;
        }

        private async void NoteWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    if (FocusManager.GetFocusedElement(this) != null/* && _active*/)
                    {
                        // 窗口获得了焦点
                        if ((DateTime.Now.Ticks - _activeTime)/ 10000 > 100)
                        {
                            Window.GetWindow(this)?.Close();
                        }
                    }
                    break;
                case Key.Tab:
                    try
                    {
                        string result = await NoteCommands.OnCommit(InputView.Text, this);
                        if (result != null)
                        {
                            InputView.Text = result;
                        }
                    }
                    catch (Exception)
                    {
                        InputView.Text = "出错了，请检查输入是否正确";//ex.ToString();
                        /*if (Application.Current.MainWindow is MainWindow mainView && mainView is MainWindow mainWindow)
                        {
                            if (mainWindow.DataContext is MainViewModel viewModel)
                            {
                                viewModel.NoteList.Add(new NoteItemViewModel(new NoteItem("err", ex.ToString())));
                            }
                        }*/
                    }
                    break;
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

    }

}
