using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
  public float range;
  public float time = 0;

  private void OnDrawGizmosSelected() {
    Gizmos.DrawWireSphere(transform.position, range);
  }

  private void OnEnable() => SoundManager.AddSound(this);
  private void OnDisable() => SoundManager.RemoveSound(this);

  private void Update() {
    time += Time.deltaTime * 2;
  }
}
