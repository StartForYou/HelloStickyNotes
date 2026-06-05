using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloStickyNotes.Models
{
    public class NoteItem
    {

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public string Content { get; set; }
        public bool Topmost { get; set; }

        public NoteItem(string title, string content)
        {
            Title = title;
            Content = content;
            Topmost = false;
        }

        

    }
}
