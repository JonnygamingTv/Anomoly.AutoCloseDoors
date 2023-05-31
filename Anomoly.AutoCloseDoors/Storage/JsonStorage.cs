using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anomoly.AutoCloseDoors.Storage
{
    public class JsonStorage<TData> where TData: class
    {
        private string _filePath;
        private TData _initial;
        public TData Data { get; set; }


        public JsonStorage(string filePath, TData initialData)
        {
            _filePath = filePath;
            _initial = initialData;
            Load();
        }

        public void Save()
        {
            File.WriteAllText(_filePath, JsonConvert.SerializeObject(Data));
        }

        public void Load()
        {
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, JsonConvert.SerializeObject(_initial));
                Data = _initial;
            }
            else
            {
                var json = File.ReadAllText(_filePath);
                Data = JsonConvert.DeserializeObject<TData>(json);
            }
        }
    }
}
