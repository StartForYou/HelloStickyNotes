using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HelloStickyNotes.Models;
using HelloStickyNotes.Utils;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Input;

namespace HelloStickyNotes.ViewModels
{
    public partial class NoteItemViewModel: ObservableObject
    {
        public NoteItem Model { get; }
        private long lastDownTime = 0;

        private bool isChanged = false;
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

        /*public int WaitSeconds
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
        }*/

        public string BackgroundColor
        {
            get
            {
                // Color To String: ColorTranslator.ToHtml(Color.Red)
                return ToColor(Model.Color, System.Drawing.Color.Bisque); //"#808080";
            }
            set
            {
                if (Model.Color != value)
                {
                    Model.Color = value;
                    NoteChanged();
                    OnPropertyChanged();
                }
            }
        }
        
        public string TextColor
        {
            get
            {
                return ToColor(Model.TextColor, Color.Black);
            }
            set
            {
                if (Model.TextColor != value)
                {
                    Model.TextColor = value;
                    NoteChanged();
                    OnPropertyChanged();
                }
            }
        }

        private string ToColor(string oldColor, Color defaultColor)
        {
            if (oldColor == "")
            {
                return ColorTranslator.ToHtml(defaultColor);
            }
            else if (oldColor.StartsWith("#"))
            {
                return oldColor;
            }
            else
            {
                try
                {
                    ColorTranslator.FromHtml(oldColor);
                    return oldColor;
                }
                catch (Exception)
                {
                    return ColorTranslator.ToHtml(defaultColor);
                }
            }
        }

        public string NoticeTitle
        {
            get => noticeTitle;
            set => noticeTitle = value;
        }

        public bool ActionVisible
        {
            get 
            {
                if (Model.Action == NoteAction.NULL)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
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
            if (Model.NeedUpdate)
            {
                isChanged = true;
                Model.NeedUpdate = false;
            }
            if (Model.Action != NoteAction.NULL)
            {
                switch (Model.Action)
                {
                    case NoteAction.CLOCK:
                        if (Model.StartTime > 0)
                        {
                            if (DateTime.Now.Ticks > Model.StartTime)
                            {
                                NoticeTitle = "闹钟";
                                Content = Title + " " + DateTime.Now.ToShortDateString() + " "+ DateTime.Now.ToShortTimeString() + " - 闹钟铃响！";
                                Model.StartTime = 0;
                                BackgroundColor = "#802010";
                                TextColor = "#FFFFFF";
                            }
                            else
                            {  
                                int remainingTime = (int)((Model.StartTime - DateTime.Now.Ticks) / 10000 / 1000);
                                string remainingText = DateTimeUtils.GetRemainingTime(remainingTime);
                                Content = Title + " - 闹钟将在" + remainingText + "后响铃" ;
                            }
                        }
                        break;
                    case NoteAction.TIME:
                        Content = "当前时间: " + DateTime.Now.ToShortTimeString();
                        break;
                    case NoteAction.COUNTDOWN:
                        if (Model.StartTime > 0)
                        {
                            int lessTime = Model.Seconds - (int) ((DateTime.Now.Ticks - Model.StartTime)/ 10000/ 1000);
                            if (lessTime <= 0)
                            {
                                NoticeTitle = "倒计时结束";
                                Content = Title + " - 倒计时结束";
                                Model.StartTime = 0;
                            }
                            else
                            {
                                string countdownText = DateTimeUtils.GetRemainingTime(lessTime);//minutes + ":" + secondsText
                                Content = Title + "倒计时: " + countdownText;
                            }
                        }
                        break;
                }
            }
            
            //OnPropertyChanged(Content);
        }



    }
}
