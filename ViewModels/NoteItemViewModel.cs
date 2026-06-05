using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HelloStickyNotes.Models;
using System;
using System.Windows.Input;

namespace HelloStickyNotes.ViewModels
{
    public partial class NoteItemViewModel: ObservableObject
    {
        public NoteItem Model { get; }
        private long lastDownTime = 0;

        private bool isChanged = false;
        private int waitSeconds = 0;
        private string noticeTitle = null;

        public string Id { get => Model.Id; }
        public string Title { get => Model.Title;
            set 
            {
                if (Model.Title != value)
                {
                    Model.Title = value;
                    NoteChanged();
                    OnPropertyChanged();
                }
            }
        }
        public string Content
        {
            get => Model.Content;
            set
            {
                if (Model.Content != value)
                {
                    Model.Content = value;
                    NoteChanged();
                    OnPropertyChanged();
                }
            }
        }

        public bool Topmost
        {
            get => Model.Topmost;
            set
            {
                if (Model.Topmost != value)
                {
                    Model.Topmost = value;
                    NoteChanged();
                    OnPropertyChanged();
                }
            }
        }

        public int WaitSeconds
        {
            get => waitSeconds;
            set
            {
                if (waitSeconds != value)
                {
                    waitSeconds = value;
                    OnPropertyChanged();
                }
            }
        }

        public string NoticeTitle
        {
            get => noticeTitle;
            set => noticeTitle = value;
        }

        // 主窗口中item按下时
        public void OnNoteItemMouseDown()
        {
            lastDownTime = DateTime.Now.Ticks;
        }

        public bool CanNoteItemClick()
        {
            //Content += " " + ((DateTime.Now.Ticks - lastDownTime)/ 10000);
            return (DateTime.Now.Ticks - lastDownTime)/ 10000 < 1000;
        }

        public bool IsContentChanged()
        {
            if (isChanged)
            {
                isChanged = false;
                return true;
            }
            return false;
        }

        [RelayCommand]
        public void OnNoteItemClick()
        {
            //Content += " (Click!)";
        }

        public ICommand ItemClickCommand { get; }

        public NoteItemViewModel(NoteItem model)
        {
            Model = model;
            ItemClickCommand = new RelayCommand(OnNoteItemClick);
        }

        public void NoteChanged()
        {
            //Content += " c";
            isChanged = true;
        }

        public void Tick()
        {
            if (WaitSeconds> 0)
            {
                WaitSeconds--;
                if (WaitSeconds <= 0)
                {
                    NoticeTitle = "倒计时结束";
                }
                Content = "剩余时间: " + (WaitSeconds / 60) + ":";
                int second = WaitSeconds % 60;
                if (second< 10)
                {
                    Content += "0"+ second;
                }
                else
                {
                    Content += second.ToString();
                }
            }
            //OnPropertyChanged(Content);
        }



    }
}
