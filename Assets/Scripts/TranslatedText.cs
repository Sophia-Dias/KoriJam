using UnityEngine;
using TMPro;

public class TranslatedText : MonoBehaviour
{
  public string textString;
  private TextMeshProUGUI textField;

  void Start()
  {
    textField = GetComponent<TextMeshProUGUI>();
    ChangeLanguage();
    TranslationManager.Instance.OnChangeLanguage.AddListener(ChangeLanguage);
  }

  private void ChangeLanguage()
  {
    textField.text = TranslationManager.Instance.GetTranslation(textString);
  }
}
