using HelloStickyNotes.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloStickyNotes.Models
{
    public class AppSettings
    {
        public static readonly string FILE_NAME = "app_settings.json";

        public int Width { get; set; }
        public int Height { get; set; }

        public AppSettings()
        {

        }

        public static AppSettings Get() 
        {
            MyStorage storage = new MyStorage("app_settings.json");
            AppSettings settings = storage.Read<AppSettings>();
            if (settings == default)
            {
                return new AppSettings();
            }
            else
            {
                return settings;
            }
            //storage.
            //return new AppSettings();
        }

    }
}
