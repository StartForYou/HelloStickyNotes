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

        private void NoteWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    Window.GetWindow(this)?.Close();
                    break;
                case Key.Tab:
                    string result = NoteCommands.OnCommit(InputView.Text, this);
                    if (result != null)
                    {
                        InputView.Text = result;
                    }
                    /*switch (InputView.Text)
                    {
                        case "pwd":
                            String pwd = RandomUtils.RandomLABCode4("2026-06-02");
                            String pwd1 = RandomUtils.RandomLABCode4(DateTime.Now);
                            String pwd2 = RandomUtils.RandomLABCode4("2026-06-03");
                            String pwd3 = RandomUtils.RandomLABCode4("2026-06-04");
                            String pwd4 = RandomUtils.RandomLABCode4("2026-06-05");
                            String pwd5 = RandomUtils.RandomLABCode4("2026-06-23");
                            String str = "" + pwd + "\n" + pwd2 + "\n" + pwd3 + "\n" + pwd4 + "\n" + pwd5 + "\nsame as pwdNow: "+ pwd1;
                            InputView.Text = str;
                            break;
                        case "dayOfYear":
                            int i = DateTime.Now.DayOfYear;
                            InputView.Text = "DayOfYear: "+ i;
                            break;
                        case "day":
                            int day = DateTime.Now.Day;
                            InputView.Text = "Day: " + day;
                            break;
                        case "dayOfWeek":
                            string dayOfWeek = DateTime.Now.DayOfWeek.ToString();
                            InputView.Text = "DayOfWeek: " + dayOfWeek;
                            break;
                        case "year":
                            int year = DateTime.Now.Year;
                            InputView.Text = "Years: " + year;
                            break;
                        case "close":
                            Window.GetWindow(this)?.Close();
                            break;
                        case "alwaysOnTop":
                        case "top":
                            this.Topmost = !this.Topmost;
                            break;
                        case "savePath":
                            InputView.Text = new MyStorage("notes.json").GetPath();
                            break;
                        case "save":
                            InputView.Text = "Save Failed";
                            if (Application.Current.MainWindow is MainWindow mainView && mainView is MainWindow mainWindow)
                            {
                                if (mainWindow.DataContext is MainViewModel viewModel)
                                {
                                    viewModel.SaveNotes();
                                    InputView.Text = "已尝试进行保存";
                                }
                            }
                            break;
                    }*/
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
