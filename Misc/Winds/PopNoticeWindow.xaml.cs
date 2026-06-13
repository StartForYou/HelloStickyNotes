using HelloStickyNotes.Models;
using System;
using System.Windows;

namespace HelloStickyNotes.Misc.Winds
{
    /// <summary>
    /// PopNoticeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PopNoticeWindow : Window
    {
        private NoteItem mItem;

        public PopNoticeWindow()
        {
            InitializeComponent();
            InitViews();
        }

        public void SetNoteItem(NoteItem noteItem)
        {
            this.mItem = noteItem;
            this.mTitle.Text = noteItem.Title;
            this.mMessage.Text = noteItem.Content;
        }

        private void InitViews()
        {
            /*if (DataContext is NoteItem noteItem)
            {

            }*/
            this.Topmost = true;
            this.Left = 0.0;
            this.Top = 0.0;
            this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            if (!this.Focus())
            {
                this.Dispatcher.BeginInvoke(new Action(() => this.Focus()));
            }
        }

        private void mBackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
