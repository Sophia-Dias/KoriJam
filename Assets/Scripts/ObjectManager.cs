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

  public List<Object> GetObjectsOnRange(Vector3 soundPosition, float range)
  {
    List<Object> onRange = new List<Object>();

    foreach (Object objectToTest in allObjects)
    {
      Vector3 objectPosition = objectToTest.transform.position;
      if (Vector3.Distance(objectPosition, soundPosition) > range) continue;

      onRange.Add(objectToTest);
    }

    return onRange;
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

  internal void MakeClick(Vector3 position)
  {
    GameObject click = new GameObject();
    click.transform.position = position;
    click.AddComponent<PlayerClick>();
  }

  public void AddOutLine(Vector3 clapSource)
  {
    foreach (Object gameObject in allObjects)
    {
      gameObject.MakeVisible(clapSource);
    }
  }
}
