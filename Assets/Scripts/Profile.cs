using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Profile : MonoBehaviour
{
    // ENCAPSULATION
    public static Profile Instance { get; private set; }

    public ProfileData data;
    
    // ENCAPSULATION
    public List<string> savedProfiles { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        // SINGLETON
        if (Instance != null)
        {
            Destroy(gameObject);
        } else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }

        SearchSavedProfiles();
    }

    // Recover saved data from local file. Create a new profile if the file is missing.
    public void Load(string profileName)
    {
        string json;
        string path = Application.persistentDataPath + "/" + profileName + ".json";
        if (File.Exists(path))
        {
            json = File.ReadAllText(path);
            data = JsonUtility.FromJson<ProfileData>(json);
        } else
        {
            data = new ProfileData(profileName);
            json = JsonUtility.ToJson(data);
            File.WriteAllText(path, json);
            savedProfiles.Add(profileName);
        }
    }

    /// <summary>Check the local directory for save files.
    /// The filenames are then added to the public list of savedProfiles.</summary>
    public void SearchSavedProfiles()
    {
        string directory = Application.persistentDataPath;
        string[] filePathsArray = Directory.GetFiles(directory);
        savedProfiles = new List<string>();

        // Consider each file name is a profile name, and save it in a public array.
        // TODO check for file format to confirm it's a valid profile data file.
        foreach (string filePath in filePathsArray)
        {
            savedProfiles.Add(Path.GetFileNameWithoutExtension(filePath));
        }
    }
    /// <summary>Return the index of the current profile in the list of found profiles.</summary>
    public int GetProfileIndex()
    {
        if(data != null & savedProfiles != null)
        {
            return savedProfiles.FindIndex(x => x == data.name);
        }
        return -1;
    }

    public class ProfileData
    {
        public string name;
        public bool[] unlocks;
        public bool hasOngoingGame;
        public long bestScore;
        public long maxLevel;

        public ProfileData(string newName)
        {
            name = newName;
            unlocks = new bool[5];
            hasOngoingGame = false;
            bestScore = 0;
            maxLevel = 0;
        }        
    }
}
