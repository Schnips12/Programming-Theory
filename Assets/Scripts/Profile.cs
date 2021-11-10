using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Profile : MonoBehaviour
{
    public static Profile Instance { get; private set; }
    public ProfileData data;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        } else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }

    // Recover saved data from local file. Return false if the file is missing.
    public bool Load(string profileName)
    {
        bool hasBeenLoaded = false;
        if (File.Exists(profileName)) 
        {
            string json = File.ReadAllText(profileName);
            data = JsonUtility.FromJson<ProfileData>(json);
            hasBeenLoaded = true;
        } else
        {
            Debug.Log("Missing profile file.");
        }
        return hasBeenLoaded;
    }

    public class ProfileData
    {
        public string name;
        public bool[] unlocks;
        public bool hasOngoingGame;
        public long bestScore;
        public long maxLevel;
    }
}
