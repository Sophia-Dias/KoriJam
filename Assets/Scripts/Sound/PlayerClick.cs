using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClick : SoundController
{
  void Start()
  {
    range = 5f;
    StartCoroutine(AutoDestroy());
  }

  private IEnumerator AutoDestroy() {
    yield return new WaitForSeconds(SoundManager.Instance.shaderDuration);
    Destroy(gameObject);
  }
}
