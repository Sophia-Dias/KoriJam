using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherController : MonoBehaviour
{
  public DialogObject dialog;
  private void OnTriggerEnter(Collider other)
  {
    DialogManager.Instance.OnFinish.AddListener(ShowCredits);
    DialogManager.Instance.StartDialog(dialog);
  }

  private void ShowCredits()
  {
    GameManager.Instance.ShowCredits();
  }
}
