using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    #region Fields
    readonly static string kSettingsFilePath = "settings.cfg";
    public static Dictionary<string, string> Settings { get; set; }
    #endregion

    #region Methods
    // Awake is called before all Start() calls
    private void Awake()
    {
        InitSettings();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Retrieve settings from config
    /// </summary>
    public static void InitSettings()
    {
        Settings = new Dictionary<string, string>();

        string settingsFilePath = Path.Combine(Application.dataPath, kSettingsFilePath);

        using (StreamReader reader = new StreamReader(settingsFilePath))
        {
            string line = reader.ReadLine();
            while (string.IsNullOrEmpty(line) == false)
            {
                if (line.StartsWith("//") == false)
                {
                    string[] id_value = line.Split('=');
                    if (id_value.Length == 2)
                    {
                        Settings.Add(id_value[0], id_value[1]);
                    }
                }
                
                line = reader.ReadLine();
            }
        }
    }

    public static bool TryGet<T>(string key, ref T value)
    {
        bool success = false;

        if (Settings.TryGetValue(key, out string str))
        {
            T val = (T)Convert.ChangeType(str, typeof(T));
            if (val != null)
            {
                value = val;
                success = true;
            }
        }
        else
        {
            Debug.Log($"Tried to get nonexistent key {key} from settings.");
        }

        return success;
    }
    #endregion
}
