using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace HelloStickyNotes.Misc
{
    public class MyStorage
    {

        private readonly string _path;
        private readonly string _rootFolderName = "startfyWorks"; //startfyWorks
        private readonly string _folderName = "HelloStickyNotes";

        private string relativePath;

        public MyStorage(string relativePath)
        {
            _path = Path.Combine(
               Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
               _rootFolderName, _folderName);
            this.relativePath = relativePath;
        }

        public string GetAppPath()
        {
            return _path;
        }

        public string GetPath()
        {
            return _path + "\\" + relativePath;
        }

        public string GetParentPath()
        {
            string path = GetPath();
            return path.Substring(0, path.LastIndexOf("\\"));
        }

        public void Save(string saveContent)
        {
            Directory.CreateDirectory(GetParentPath());
            File.WriteAllText(GetPath(), saveContent);
        }

        public void Save<T>(T saveContent)
        {
            Save(JsonSerializer.Serialize<T>(saveContent));
        }

        public void Save<T>(IEnumerable<T> items)
        {
            Save(JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true }));
            //Directory.CreateDirectory(Path.GetDirectoryName(_path)!);
            //File.WriteAllText(_path, JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true }));
        }

        public T Read<T>()
        {
            if (!File.Exists(GetPath())) return default;
            return JsonSerializer.Deserialize<T>(File.ReadAllText(GetPath()));
            //Directory.CreateDirectory(Path.GetDirectoryName(_path)!);
            //File.WriteAllText(_path, JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true }));
        }

    }
}
