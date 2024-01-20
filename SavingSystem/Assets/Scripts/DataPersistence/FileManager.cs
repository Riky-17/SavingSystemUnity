using System.IO;
using Newtonsoft.Json;

public class FileManager
{
    string dataPath;
    string file;

    public FileManager(string dataPath, string file)
    {
        this.dataPath = dataPath;
        this.file = file;
    }

    public void SaveData(GameData data)
    {
        string fullPath = Path.Combine(dataPath, file);;

        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
        JsonSerializer serializer = new JsonSerializer();

        using FileStream stream = new(fullPath, FileMode.Create);
        using StreamWriter sw = new(stream);
        using JsonWriter writer = new JsonTextWriter(sw);
        
        serializer.Serialize(writer, data);

    }

    public GameData LoadData()
    {
        GameData dataToLoad = null;
        string fullPath = Path.Combine(dataPath, file);

        if (File.Exists(fullPath))
        {
            JsonSerializer serializer = new JsonSerializer();
            using FileStream stream = new(fullPath, FileMode.Open);
            using StreamReader sr = new StreamReader(stream);
            using JsonReader reader = new JsonTextReader(sr);
    
            dataToLoad = serializer.Deserialize<GameData>(reader);
        }

        return dataToLoad;
    }
}
