using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
  public static ObjectManager Instance;

  public bool isDebugging;
  public List<Object> allObjects = new List<Object>();

  private void Awake()
  {
    if (Instance == null) Instance = this;
    if (Instance != this) Destroy(this);
  }

  [ContextMenu("Ativar Debug")]
  public void ActiveDebug()
  {
    isDebugging = true;

    foreach (Object gameObject in allObjects)
    {
      gameObject.ActiveDebug();
    }
  }

  [ContextMenu("Desativar Debug")]
  public void DeactiveDebug()
  {
    isDebugging = true;

    foreach (Object gameObject in allObjects)
    {
      gameObject.DeactiveDebug();
    }
  }

  public void AddOutLine(Vector3 clapSource)
  {
    foreach (Object gameObject in allObjects)
    {
      gameObject.MakeVisible(clapSource);
    }
  }
}
