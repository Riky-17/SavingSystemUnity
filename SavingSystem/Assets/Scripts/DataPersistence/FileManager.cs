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

    string FullPath(string slotID) => Path.Combine(dataPath, slotID, file);

    public void SaveData(GameData data, string slotID)
    {
        string fullPath = FullPath(slotID);

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
        string fullPath = FullPath(slotID);

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

    public void DeleteData(string slotID)
    {
        string fullPath = FullPath(slotID);

        if(File.Exists(fullPath))
            File.Delete(fullPath);
    }

    public void CopyData(string slotIDCopyFrom, string slotIDCopyTo)
    {
        string fullPathCopyFrom = FullPath(slotIDCopyFrom);

        if(File.Exists(fullPathCopyFrom))
        {
            GameData data = LoadData(slotIDCopyFrom);
            SaveData(data, slotIDCopyTo);
        }
    }

    public Dictionary<string, GameData> GetAllData()
    {
        Dictionary<string, GameData> allData = new();
        IEnumerable<DirectoryInfo> directoryInfos = new DirectoryInfo(dataPath).EnumerateDirectories();
        GameData slotData;

        foreach (DirectoryInfo directory in directoryInfos)
        {
            string slotID = directory.Name;
            string fullPath = FullPath(slotID);

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
