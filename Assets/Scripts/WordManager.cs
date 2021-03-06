using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    #region Fields
    readonly static string kWordFilePath = "words{0}.txt";

    private static WordManager _instance;
    public static WordManager Instance
    {
        get
        {
            if (_instance is null)
            {
                Debug.Log("SettingsManager is null");
            }
            return _instance;
        }
    }

    public static List<List<string>> Words { get; set; }
    #endregion

    #region Methods
    // Awake is called before all Start() calls
    private void Awake()
    {
        _instance = this;
        InitWords();
    }

    private void InitWords()
    {
        Words = new List<List<string>>() { null, null, null }; // 0, 1, 2 unused

        int i = 3;
        string fileName = Path.Combine(Application.dataPath, string.Format(kWordFilePath, i));
        while (File.Exists(fileName))
        {
            Words.Add(new List<string>());
            using (StreamReader reader = new StreamReader(fileName))
            {
                string line = reader.ReadLine();
                string[] words = line.Split(' ');
                foreach (string word in words)
                {
                    if (word.Length == i)
                    {
                        Words[i].Add(word);
                    }
                }
            }

            i++;
            fileName = Path.Combine(Application.dataPath, string.Format(kWordFilePath, i));
        }
    }

    public static string GetRandomWord(int wordLength)
    {
        string word = string.Empty;

        if (Words.Count > wordLength)
        {
            word = Words[wordLength][Random.Range(0, Words[wordLength].Count)];
        }

        return word;
    }
    #endregion
}
