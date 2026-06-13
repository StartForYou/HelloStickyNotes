using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HelloStickyNotes.Models
{
    public class NoteItem
    {

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public string Content { get; set; }
        public bool Topmost { get; set; }
        public NoteAction Action { get; set; }
        public string Color { get; set; }
        public string TextColor { get; set; }
        public int Seconds { get; set; }
        public long StartTime { get; set; }
        public bool ShowNoticeWindow { get; set; }

        [JsonIgnore]
        public bool NeedUpdate;


        public NoteItem(string title, string content)
        {
            Title = title;
            Content = content;
            Topmost = false;
            Action = NoteAction.NULL; // 0
            Color = "";
            TextColor = "";
        }

        // 设置倒计时
        public void SetCoountdown(int seconds)
        {
            Action = NoteAction.COUNTDOWN;
            StartTime = DateTime.Now.Ticks;
            Seconds = seconds;
        }

        

    }
}
