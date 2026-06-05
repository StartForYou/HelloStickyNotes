using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HelloStickyNotes.Misc;
using HelloStickyNotes.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HelloStickyNotes.ViewModels
{
    public partial class MainViewModel: ObservableObject
    {

        [ObservableProperty]
        private bool _isAlwaysOnTop = false;

        [ObservableProperty]
        private string _debugText = "";
        private int tick = 0;

        private bool isContentChanged = false;
        private NoteItemViewModel lastRightClickNote = null;
        private List<NoticeBean> noticeItems = new List<NoticeBean>();

        public ObservableCollection<NoteItemViewModel> NoteList { get; set; } = new ObservableCollection<NoteItemViewModel>();


        public string DebugText
        {
            get => _debugText;
            set{ SetProperty(ref _debugText, value); }
        }

        public bool IsAlwaysOnTop
        {
            get => _isAlwaysOnTop;
            set { SetProperty(ref _isAlwaysOnTop, value); }
        }

        /*public List<NoteItemViewModel> NoteList 
        {   get => _noteList;
            set { SetProperty(ref _noteList, value); }
        }*/


        [RelayCommand]
        public void ToggleAlwaysOnTop()
        {
            IsAlwaysOnTop = !IsAlwaysOnTop;
            DebugText = IsAlwaysOnTop ? "已置顶当前窗口" : "已取消当前窗口置顶";
            //SaveSettings();
        }
        
        [RelayCommand]
        public void NewNote()
        {
            int count = 1;
            foreach (var item in NoteList) { count++; }

            DebugText = "已尝试添加新的便签!";
            NoteItem model = new NoteItem("新便签" + count, "");
            NoteItemViewModel viewModel = new NoteItemViewModel(model);
            NoteList.Add(viewModel);
            //OnPropertyChanged();
            NoteWindow noteWindow = new NoteWindow();
            noteWindow.DataContext = viewModel;
            noteWindow.Show();
            
            DebugText = "已尝试添加新的便签! 现在已有"+ count+ "个";
            isContentChanged = true;
        }

        public void RemoveNote(NoteItemViewModel noteViewModel)
        {

            NoteItem noteItem = noteViewModel.Model;
            NoteList.Remove(noteViewModel);
            foreach (Window window in Application.Current.Windows)
            {
                if (window is NoteWindow noteWindow && noteWindow.DataContext is NoteItemViewModel model)
                {
                    if (model.Id == noteViewModel.Id)
                    {
                        noteWindow.Close();
                    }
                }
            }
            //DebugText = "已尝试添加新的便签! 现在已有" + count + "个";
            DebugText = "已尝试删除便签: " + (noteItem.Title != null ? noteItem.Title : "无标题");
            isContentChanged = true;
        }

        public void SetLastRightClickNote(NoteItemViewModel noteViewModel)
        {
            lastRightClickNote = noteViewModel;
        }

        public NoteItemViewModel GetLastRightClickNote()
        {
            return lastRightClickNote;
        }

        public bool NeedNotice()
        {
            return noticeItems.Count() > 0;
        }

        public List<NoticeBean> GetNoticeItems()
        {
            return noticeItems;
        }

        [RelayCommand]
        public void CloseAllWindows()
        {
            DebugText = "已尝试关闭所有便签";
            /*App.Current.Shutdown();
            Window[] childArray = Application.Current.Windows.Cast<Window>().ToArray();
            for (int i = childArray.Length; i-- > 0;)//关闭所有子窗体
            {
                Window item = childArray[i];
                item.Close();
                //if (item.Title == "") continue; // 忽略无标题窗口
                //if (item.Title != this.Title) item.Close();
            }*/
            //Window.GetWindow()
            foreach (Window window in Application.Current.Windows)
            {
                if (!(window is MainWindow))
                {
                    window.Close();
                }
            }
        }

        public void Tick()
        {
            tick++;
            foreach (var noteModel in NoteList) 
            {
                noteModel.Tick();
                if (noteModel.IsContentChanged())
                {
                    isContentChanged = true;
                }
                if (noteModel.NoticeTitle!= null)
                {
                    noticeItems.Add(new NoticeBean(noteModel.NoticeTitle, noteModel.Model));
                    noteModel.NoticeTitle = null;
                }
            }
            if (tick % 5== 0)
            {
                if (isContentChanged)
                {
                    SaveNotes();
                    DebugText = "已自动保存便签数据";
                    isContentChanged = false;
                }
                if (!DebugText.EndsWith(")"))
                {
                    DebugText += " (" + DateTime.Now.ToString() + ")";
                }
            }
        }

        public void SaveNotes()
        {
            List<NoteItem> items = new List<NoteItem>();
            foreach (var noteView in NoteList)
            {
                items.Add(noteView.Model);
            }
            new MyStorage("notes.json").Save(items);
        }

        public IEnumerable<NoteItem> LoadNotes()
        {
            MyStorage storage = new MyStorage("notes.json");
            if (!File.Exists(storage.GetPath())) return new List<NoteItem>();
            try
            {
                return JsonSerializer.Deserialize<IEnumerable<NoteItem>>(File.ReadAllText(storage.GetPath())) ?? new List<NoteItem>();
            }
            catch
            {
                return new List<NoteItem>();
            }
        }


        public ICommand ToggleAlwaysOnTopCommand { get; }
        public ICommand NewNoteCommand { get; }
        public ICommand CloseAllWindowsCommand { get; }

        public MainViewModel()
        {
            DebugText = "初始化完成";
            ToggleAlwaysOnTopCommand = new RelayCommand(ToggleAlwaysOnTop);
            NewNoteCommand = new RelayCommand(NewNote);
            CloseAllWindowsCommand = new RelayCommand(CloseAllWindows);
            // 可用
            //MyStorage storage = new MyStorage("notes.json");
            //String json = File.ReadAllText(storage.GetPath());
            //NoteList.Add(new NoteItemViewModel(new NoteItem("信息", json)));

            foreach (NoteItem item in LoadNotes())
            {
                NoteList.Add(new NoteItemViewModel(item));
            }
            DebugText = "已从本地加载" + NoteList.Count + "项数据";
        }


    }
}
