using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloStickyNotes.Models
{
    public class NoticeBean
    {

        public NoteItem NoteItem { get; set; }
        public string NoticeTitle { get; set; }

        public NoticeBean(string noticeTitle, NoteItem noteItem)
        {
            NoticeTitle = noticeTitle;
            NoteItem = noteItem;
        }

    }
}
