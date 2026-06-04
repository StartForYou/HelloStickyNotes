using HelloStickyNotes.ViewModels;
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
                    Border self = (sender as Border);
                    if (self.DataContext != null && self.DataContext is NoteItemViewModel selfModel)
                    {
                        if (selfModel.CanNoteItemClick())
                        {
                            OnItemClick(sender, selfModel);
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
                            MainViewModel mainViewModel = GetMainViewModel();
                            mainViewModel?.RemoveNote(selfModel);
                            //selfModel.RemoveSelf();
                            //OnItemClick(sender, selfModel);
                        }
                    }
                }

            }
            this.itemMouseDownBorder = null;
        }

        private void OnItemClick(object sender, NoteItemViewModel item)
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
    }
}
