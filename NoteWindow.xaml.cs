using HelloStickyNotes.Misc;
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
        public NoteWindow()
        {
            InitializeComponent();
            this.KeyDown += NoteWindow_KeyDown;
        }

        private async void NoteWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    Window.GetWindow(this)?.Close();
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
                    catch (Exception ex)
                    {
                        InputView.Text = "出错了，请检查输入是否正确";//ex.ToString();
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
