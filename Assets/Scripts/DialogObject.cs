using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialog", menuName = "KoriJam/Dialog", order = 0)]
public class DialogObject : ScriptableObject
{
  [TextArea(5, 20)]
  [SerializeField] private List<string> texts;

  public string GetTranslatedText (int index) {
    return TranslationManager.Instance.GetTranslation(texts[index]);
  }

  public int Count () {
    return texts.Count;
  }
}