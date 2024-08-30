
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace OnlineShopWebApp.Helpers
{
    public static class Helper
    {
        public static void Save(object data, string fileName)
        {

            string path = $"{Environment.CurrentDirectory}\\Data\\{fileName}";

            var option = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,

                WriteIndented = true,
            };

            string serializeData = JsonSerializer.Serialize(data, option);

            if (!File.Exists(path))

                File.Create(path).Close();

            File.WriteAllText(path, serializeData);

        }
        public static List<T> Load<T>(string fileName)
        {
            string path = $"{Environment.CurrentDirectory}\\Data\\{fileName}";

            if (!File.Exists(path)) return new List<T>();

            string jsonString = File.ReadAllText(path);

            List<T> deserializeData = JsonSerializer.Deserialize<List<T>>(jsonString);

            return deserializeData;
        }
    }
}
