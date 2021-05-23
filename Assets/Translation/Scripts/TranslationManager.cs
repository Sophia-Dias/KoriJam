using System;
using System.Collections.Generic;
using UnityEngine;

public class TranslationManager : MonoBehaviour
{
  public static TranslationManager Instance;

  public TextAsset translationCSV;

  public string currentLanguage { get; private set; }
  private List<string> languages;
  private Dictionary<string, Dictionary<string, string>> texts;


  private void Awake() {
    if (Instance == null) Instance = this;
    if (Instance != this) Destroy(this);

    DontDestroyOnLoad(this);
  }

  private void Start()
  {
    languages = GetLanguages();
    SetLanguage(languages[0]);
    List<string> textString = new List<string>(translationCSV.ToString().Split('\r'));
    textString.RemoveAt(0);
    LoadTranslationData(textString);
  }

  private void LoadTranslationData(List<string> textString)
  {
    texts = new Dictionary<string, Dictionary<string, string>>();

    foreach (string lang in languages)
    {
      texts.Add(lang, new Dictionary<string, string>());
    }

    foreach (string translation in textString)
    {
      List<string> translationList = new List<string>(translation.Split(';'));
      string value = translationList[0];
      translationList.RemoveAt(0);

      for (int translationIndex = 0; translationIndex < translationList.Count; translationIndex++)
      {
        string lang = languages[translationIndex];

        texts[lang].Add(value.Trim(), translationList[translationIndex].Trim());
      }
    }
  }

  public List<string> GetLanguages () {
    string translationHeader = translationCSV.ToString().Split('\r')[0].Trim();
    List<string> languages = new List<string>(translationHeader.Split(';'));

    languages.RemoveAt(0);

    return languages;
  }

  public string GetTranslation(string textName)
  {
    if(!texts[currentLanguage].ContainsKey(textName)) return textName;
    
    return texts[currentLanguage][textName];
  }

  public void SetLanguage(string newLanguage)
  {
    if (!languages.Contains(newLanguage)) return;
    PlayerPrefs.SetString("Language", newLanguage);
    currentLanguage = newLanguage;
  }
}