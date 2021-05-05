using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class DialogManager : MonoBehaviour
{
  public static DialogManager Instance;
  public DialogObject currentDialog;
  public int currentDialogIndex;
  public TextMeshProUGUI textField;
  public GameObject textCanvas;
  public float textSpeed;

  public DialogObject initial;
  private bool canSwitch;
  public bool isTalking;
  public UnityEvent OnFinish;

  private void Awake()
  {
    if (Instance == null) Instance = this;
    if (Instance != this) Destroy(this);
  }

  private void Start()
  {
    if (OnFinish == null) OnFinish = new UnityEvent();
    StartDialog(initial);
  }

  private void Update()
  {
    if (!isTalking) return;

    if (Input.GetButtonUp("Fire1"))
    {
      NextText();
    }
  }

  public void StartDialog(DialogObject dialog)
  {
    currentDialog = dialog;
    currentDialogIndex = 0;
    canSwitch = true;
    isTalking = true;
    textCanvas.SetActive(true);

    NextText();
  }

  private void NextText()
  {
    if (!canSwitch) return;

    if (currentDialogIndex >= currentDialog.texts.Count)
    {
      CloseDialog();
      return;
    }

    string textToShow = currentDialog.texts[currentDialogIndex];
    textField.text = "";
    StartCoroutine(WriteText(textToShow));
    currentDialogIndex++;

  }

  private IEnumerator WriteText(string text)
  {
    canSwitch = false;
    while (textField.text.Length < text.Length)
    {
      textField.text = textField.text + text[textField.text.Length];
      yield return new WaitForSeconds(textSpeed);
    }

    canSwitch = true;
  }

  private void CloseDialog()
  {
    Debug.Log("Fim");
    isTalking = false;
    textCanvas.SetActive(false);
    OnFinish.Invoke();
  }
}
