using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string dataDirpath = "";
    private string dataFileName = "";

    private bool useEncryption = false;
    private string useEncryptionCodeWord = "carem";                                                                                                                                                                                                                                                                                                                                                                                                                                                                            

    public FileDataHandler(string dataDirpath, string dataFileName, bool useEncryption)
    {
        this.dataDirpath = dataDirpath;
        this.dataFileName = dataFileName;
        this.useEncryption = useEncryption;
    }

    public GameData Load()
    {
        var fullPath = Path.Combine(dataDirpath, dataFileName);
        GameData loadedData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                if (useEncryption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                // Desieialize data from Json to c# object
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error loading save file: " + fullPath + "\n" + e.Message);
            }
        }

        return loadedData;
    }

    public void Save(GameData data)
    {
        var fullPath = Path.Combine(dataDirpath, dataFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            
            // Serialize data to a Json
            string dataToStore = JsonUtility.ToJson(data, true);

            if (useEncryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error saving to a file: " + fullPath + "\n" + e.Message);
        }

    }

    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ useEncryptionCodeWord[i % useEncryptionCodeWord.Length]);
        }
        return modifiedData;
    }
}
