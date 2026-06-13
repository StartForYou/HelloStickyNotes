using HelloStickyNotes.Models;
using HelloStickyNotes.ViewModels;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HelloStickyNotes.Controls
{
    /// <summary>
    /// NoteListControl.xaml 的交互逻辑
    /// </summary>
    public partial class NoteListControl : UserControl
    {

        private object itemMouseDownBorder;

        public NoteListControl()
        {
            InitializeComponent();
            //ScrollView.Set
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Border self = (sender as Border);
            if (self.DataContext != null && self.DataContext is NoteItemViewModel selfModel)
            {
                selfModel.OnNoteItemMouseDown();
            }
            this.itemMouseDownBorder = sender;
            //(this.DataContext as NoteItemViewModel).OnNoteItemClick();
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.itemMouseDownBorder == sender)
            {
                if (e.ChangedButton == MouseButton.Left)
                {
                    // 左键点击
                    //Border self = (sender as Border);
                    NoteItemViewModel selfModel = GetSelfModel(sender as FrameworkElement);
                    if (selfModel != null)
                    {
                        if (selfModel.CanNoteItemClick())
                        {
                            OnItemLeftClick(sender, selfModel);
                        }
                    }
                }
                else if (e.ChangedButton == MouseButton.Right)
                {
                    // 右键点击
                    Border self = (sender as Border);
                    if (self.DataContext != null && self.DataContext is NoteItemViewModel selfModel)
                    {
                        if (selfModel.CanNoteItemClick())
                        {
                            OnItemRightClick(sender, selfModel);
                            //selfModel.RemoveSelf();
                            //OnItemClick(sender, selfModel);
                        }
                    }
                }

            }
            this.itemMouseDownBorder = null;
        }

        private void OnItemLeftClick(object sender, NoteItemViewModel item)
        {
            try
            {
                bool existNote = false;
                // 查找item对应的noteWindow是否存在
                NoteWindow itemWindow = null;
                foreach (Window window in Application.Current.Windows)
                {
                    if (window is NoteWindow noteWindow && noteWindow.DataContext is NoteItemViewModel model)
                    {
                        if (model.Id == item.Id)
                        {
                            existNote = true;
                            itemWindow = noteWindow;
                        }
                    }
                }
                if (existNote)
                {
                    itemWindow.Focus();
                }
                else
                {
                    NoteWindow noteWindow = new NoteWindow { DataContext = item };
                    noteWindow.Show();
                    //item.Content += existNote ? "\n(existNote!)" : "\n(nofoundNote)";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        private void OnItemRightClick(object sender, NoteItemViewModel item)
        {
            try
            {
                //MessageBoxButton boxButton = MessageBoxButton.YesNo;
                //MessageBoxResult messageResult = MessageBox.Show("Hello world!", "caption", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                /*MainViewModel mainViewModel = GetMainViewModel();
                mainViewModel?.RemoveNote(item);*/
                MainViewModel mainViewModel = GetMainViewModel();
                mainViewModel?.SetLastRightClickNote(item);
                ClearActionButton.Visibility = item.ActionVisible ? Visibility.Visible : Visibility.Collapsed;
                myPopup.IsOpen = true;
                //myPopup.find
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        private MainViewModel GetMainViewModel()
        {
            if (Application.Current.MainWindow is MainWindow mainView && mainView is MainWindow mainWindow)
            {
                if (mainWindow.DataContext is MainViewModel viewModel)
                {
                    return viewModel;
                }
            }
            return null;
        }

        private NoteItemViewModel GetSelfModel(FrameworkElement sender)
        {
            if (sender.DataContext != null && sender.DataContext is NoteItemViewModel selfModel)
            {
                return selfModel;
            }
            return null;
        }

        private void PopupClearAction_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel mainViewModel = GetMainViewModel();
            if (mainViewModel != null)
            {
                try
                {
                    myPopup.IsOpen = false;
                    NoteItemViewModel selfModel = mainViewModel.GetLastRightClickNote();//GetSelfModel(sender as FrameworkElement);
                    if (selfModel != null)
                    {
                        selfModel.Model.Action = NoteAction.NULL;
                    }
                    string tip = "已尝试清除" + selfModel.Title + "的行为";
                    mainViewModel.DebugText = tip;
                    mainViewModel.SetRequireToSave();
                }
                catch (Exception ex)
                {
                    MainViewModel mainViewModel1 = GetMainViewModel();
                    NoteItem model = new NoteItem("报错日志", ex.ToString());
                    NoteItemViewModel viewModel = new NoteItemViewModel(model);
                    mainViewModel1.NoteList.Add(viewModel);
                    //mainViewModel1.NewNote();//DebugText = ex.ToString();
                }
            }
        }

        private void PopupDelete_Click(object sender, RoutedEventArgs e)
        {
            myPopup.IsOpen = false;
            MainViewModel mainViewModel = GetMainViewModel();
            mainViewModel?.RemoveNote(mainViewModel.GetLastRightClickNote());
        }

        private void PopupCancel_Click(object sender, RoutedEventArgs e)
        {
            myPopup.IsOpen = false;
        }
        
    }
}
