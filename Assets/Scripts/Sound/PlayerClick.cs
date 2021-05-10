using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClick : MonoBehaviour
{

  private float range = 5f;
  private List<Object> objectsOnRange;

  void Start()
  {
    objectsOnRange = ObjectManager.Instance.GetObjectsOnRange(transform.position, range);
    foreach (Object items in objectsOnRange)
    {
      items.MakeVisible(transform.position);
    }

    StartCoroutine(AutoDestroy());
  }

  private IEnumerator AutoDestroy() {
    yield return new WaitForSeconds(5f);
    Destroy(gameObject);
  }
}
