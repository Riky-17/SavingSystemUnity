using System.Collections.Generic;
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

    public void SaveData(GameData data, string slotID)
    {
        string fullPath = Path.Combine(dataPath, slotID, file);;

        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
        JsonSerializer serializer = new JsonSerializer();

        using FileStream stream = new(fullPath, FileMode.Create);
        using StreamWriter sw = new(stream);
        using JsonWriter writer = new JsonTextWriter(sw);
        
        serializer.Serialize(writer, data);

    }

    public GameData LoadData(string slotID)
    {
        GameData dataToLoad = null;
        string fullPath = Path.Combine(dataPath, slotID, file);

        if (File.Exists(fullPath))
        {
            JsonSerializer serializer = new();
            using FileStream stream = new(fullPath, FileMode.Open);
            using StreamReader sr = new StreamReader(stream);
            using JsonReader reader = new JsonTextReader(sr);
    
            dataToLoad = serializer.Deserialize<GameData>(reader);
        }

        return dataToLoad;
    }

    public Dictionary<string, GameData> GetAllData()
    {
        Dictionary<string, GameData> allData = new();
        IEnumerable<DirectoryInfo> directoryInfos = new DirectoryInfo(dataPath).EnumerateDirectories();
        GameData slotData;

        foreach (DirectoryInfo directory in directoryInfos)
        {
            string slotID = directory.Name;
            string fullPath = Path.Combine(dataPath, slotID, file);

            if(!File.Exists(fullPath))
            {
                continue;
            }

            slotData = LoadData(slotID);
            allData.Add(slotID, slotData);
        }

        return allData;
    }
}
