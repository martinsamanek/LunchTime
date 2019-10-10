using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace LunchTime.Services
{
    public abstract class JsonFileStorage<T>
    {
        protected JsonFileStorage(IHostingEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }
        
        protected IHostingEnvironment WebHostEnvironment { get; }

        private IList<T> _cache = new List<T>();
        
        protected virtual string FileName => Path.Combine(WebHostEnvironment.ContentRootPath, "data", $"{GetType().Name}.json");
        
        protected IList<T> Load()
        {
            if (_cache.Any())
            {
                return _cache;
            } 
            
            if (!File.Exists(FileName))
            {
                return new List<T>();
            }

            using(var jsonFileReader = File.OpenText(FileName))
            {
                _cache = JsonConvert.DeserializeObject<IList<T>>(jsonFileReader.ReadToEnd());
                return _cache;
            }
        }

        protected void Save(IList<T> values)
        {
            _cache = values;
            using (var outputStream = File.Open(FileName, FileMode.Create))
            using (var writer = new StreamWriter(outputStream))
            {
                writer.Write(JsonConvert.SerializeObject(values, Formatting.Indented));
            }
        }
    }
}