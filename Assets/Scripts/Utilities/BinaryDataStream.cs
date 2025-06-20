using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace GameBlockAdv.Utility
{
    public class BinaryDataStream
    {
        public static void Save<T>(T serializedObject, string fileName)
        {
            string path = Path.Combine(Application.persistentDataPath, "saves");
            Directory.CreateDirectory(path);
            string fullPath = Path.Combine(path, fileName + ".dat");

            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    formatter.Serialize(fileStream, serializedObject);
                }
            }
            catch (SerializationException e)
            {
                Debug.LogError("Save Failed: " + e.Message);
            }
        }

        public static bool Exist(string fileName)
        {
            string path = Path.Combine(Application.persistentDataPath, "saves");
            string fullPath = Path.Combine(path, fileName + ".dat");
            return File.Exists(fullPath);
        }

        public static T Read<T>(string fileName)
        {
            string path = Path.Combine(Application.persistentDataPath, "saves");
            string fullPath = Path.Combine(path, fileName + ".dat");

            if (!File.Exists(fullPath))
            {
                Debug.LogWarning("File not found: " + fullPath);
                return default(T);
            }

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            T returnType = default(T);

            try
            {
                using (FileStream fileStream = new FileStream(fullPath, FileMode.Open))
                {
                    returnType = (T)binaryFormatter.Deserialize(fileStream);
                }
            }
            catch (SerializationException e)
            {
                Debug.LogError("Read failed: " + e.Message);
            }

            return returnType;
        }

    }

}
