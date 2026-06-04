using HelloStickyNotes.Misc;
using HelloStickyNotes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HelloStickyNotes.Utils
{
    internal class NoteCommands
    {

        public static string OnCommit(string input, NoteWindow noteWindow) 
        {
            string result = null;
            switch (input)
            {
                case "pwd":
                    String pwd = RandomUtils.RandomLABCode4("2026-06-02");
                    String pwd1 = RandomUtils.RandomLABCode4(DateTime.Now);
                    String pwd2 = RandomUtils.RandomLABCode4("2026-06-03");
                    String pwd3 = RandomUtils.RandomLABCode4("2026-06-04");
                    String pwd4 = RandomUtils.RandomLABCode4("2026-06-05");
                    String pwd5 = RandomUtils.RandomLABCode4("2026-06-23");
                    String str = "" + pwd + "\n" + pwd2 + "\n" + pwd3 + "\n" + pwd4 + "\n" + pwd5 + "\nsame as pwdNow: " + pwd1;
                    result = str;
                    break;
                case "dayOfYear":
                    int i = DateTime.Now.DayOfYear;
                    result = "DayOfYear: " + i;
                    break;
                case "day":
                    int day = DateTime.Now.Day;
                    result = "Day: " + day;
                    break;
                case "dayOfWeek":
                    string dayOfWeek = DateTime.Now.DayOfWeek.ToString();
                    result = "DayOfWeek: " + dayOfWeek;
                    break;
                case "year":
                    int year = DateTime.Now.Year;
                    result = "Years: " + year;
                    break;
                case "close":
                    Window.GetWindow(noteWindow)?.Close();
                    break;
                case "alwaysOnTop":
                case "top":
                    noteWindow.Topmost = !noteWindow.Topmost;
                    break;
                case "savePath":
                    result = new MyStorage("notes.json").GetPath();
                    break;
                case "save":
                    result = "Save Failed";
                    if (Application.Current.MainWindow is MainWindow mainView && mainView is MainWindow mainWindow)
                    {
                        if (mainWindow.DataContext is MainViewModel viewModel)
                        {
                            viewModel.SaveNotes();
                            result = "已尝试进行保存";
                        }
                    }
                    break;
                case "checkUpdate":
                    string api = "https://api.github.com/repos/StartForYou/HelloStickyNotes/releases/latest";
                    result = api;
                    break;
            }
            return result;
        }

    }
}
