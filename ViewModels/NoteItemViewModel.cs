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
            //OnPropertyChanged(Content);
        }



    }
}
