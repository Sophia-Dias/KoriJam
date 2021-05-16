using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (AudioSource))]
public class SoundController : MonoBehaviour
{
  [Range(0.01f, 10f)]
  public float range;
  public float time = 0;
  public AudioSource audioSource;

  private void OnDrawGizmosSelected() {
    Gizmos.DrawWireSphere(transform.position, range);
  }

  private void OnEnable() => SoundManager.AddSound(this);
  private void OnDisable() => SoundManager.RemoveSound(this);

  private void Start() {
    audioSource = GetComponent<AudioSource>();
  }

  protected virtual void Update() {
    if (!GameManager.Instance.CanMove()) return;
    time += Time.deltaTime * 2;
  }

  protected void PlaySound()
  {
    time = 0;
    audioSource.Play();
  }
}
