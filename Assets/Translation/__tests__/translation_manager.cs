using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

namespace Tests
{
  public class translation_manager
  {
    private GameObject translationManagerGameObject;

    [SetUp]
    public void SetUp()
    {
      PlayerPrefs.DeleteKey("Language");
      translationManagerGameObject = new GameObject();
      TranslationManager translation = translationManagerGameObject.AddComponent<TranslationManager>();

      translation.translationCSV = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Translation/__tests__/resources/translationTestFile.CSV");
    }

    [TearDown]
    public void TearDown()
    {
      GameObject.DestroyImmediate(translationManagerGameObject);
    }

    [UnityTest]
    public IEnumerator translation_managerShouldBeASingleton()
    {
      yield return null;
      
      Assert.AreNotEqual(TranslationManager.Instance, null);
    }

    [UnityTest]
    public IEnumerator translation_managerShoulBeAbleToSetTranslationCSV()
    {
      yield return null;
      
      Assert.AreNotEqual(TranslationManager.Instance, null);
    }

    [UnityTest]
    public IEnumerator translation_managerShoulBeAbleToGetAllAvailableLanguages()
    {
      yield return null;
      
      Assert.AreEqual(new List<string>{
        "en","pt-br"
      }, TranslationManager.Instance.GetLanguages());
    }

    [UnityTest]
    public IEnumerator translation_managerDefaultLanguageShouldBeEn()
    {
      yield return null;
      
      Assert.AreEqual("en", TranslationManager.Instance.currentLanguage);
    }

    [UnityTest]
    public IEnumerator translation_managerChangeDefaultLanguageForAValidLanguage()
    {
      yield return null;
      TranslationManager.Instance.SetLanguage("pt-br");

      Assert.AreEqual("pt-br", TranslationManager.Instance.currentLanguage);
    }

    [UnityTest]
    public IEnumerator translation_managerGetTranslationTextForNewGameInEn()
    {
      yield return null;

      Assert.AreEqual("Play Game", TranslationManager.Instance.GetTranslation("PLAY_GAME"));
    }

    [UnityTest]
    public IEnumerator translation_managerGetTranslationTextForNewGameInPtBr()
    {
      yield return null;

      TranslationManager.Instance.SetLanguage("pt-br");
      Assert.AreEqual("Jogar", TranslationManager.Instance.GetTranslation("PLAY_GAME"));
    }

    [UnityTest]
    public IEnumerator translation_managerDefaultPlayerPrefLanguageShouldBeEn()
    {
      yield return null;

      Assert.AreEqual("en", PlayerPrefs.GetString("Language"));
    }

    [UnityTest]
    public IEnumerator translation_managerShouldBeChangePlayerPrefLanguageWhenChangeLanguage()
    {
      yield return null;

      TranslationManager.Instance.SetLanguage("pt-br");
      Assert.AreEqual("pt-br", PlayerPrefs.GetString("Language"));
    }

    [UnityTest]
    public IEnumerator translation_managerShouldNotBeChangeLanguageForWrongLanguage()
    {
      yield return null;

      TranslationManager.Instance.SetLanguage("cs");
      Assert.AreNotEqual("cs", PlayerPrefs.GetString("Language"));
    }

    [UnityTest]
    public IEnumerator translation_managerShouldReturnNameWhenTextNotFound()
    {
      yield return null;

      Assert.AreEqual("NOT_FOUND", TranslationManager.Instance.GetTranslation("NOT_FOUND"));
    }
  }
}
