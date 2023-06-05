using Newtonsoft.Json;
using Rocket.Core.Logging;
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
        private string _file;
        private TData _initialData;
        

        public TData Instance { get; private set; }
        
        public JsonStorage(string file, TData initialData)
        {
            _file = file;
            _initialData = initialData;
        }

        public void Save()
        {
            try
            {
                if(Instance != null)
                {
                    var json = JsonConvert.SerializeObject(Instance);

                    File.WriteAllText(_file, json);
                }
            }
            catch(Exception ex) { Logger.LogException(ex, $"Failed to save data for path: {_file}"); }
        }

        public void Load()
        {
            try
            {
                if (!File.Exists(_file))
                {
                    var initialJson = JsonConvert.SerializeObject(_initialData);
                    File.WriteAllText(_file, initialJson);

                    Instance = _initialData;
                    return;
                }

                var json = File.ReadAllText(_file);

                Instance = JsonConvert.DeserializeObject<TData>(json);
            }
            catch(Exception ex) { Logger.LogException(ex, $"Failed to load data for path: {_file}"); }
        }
    }
}
