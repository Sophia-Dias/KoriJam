using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEcholocaization : MonoBehaviour
{
  public static PlayerEcholocaization Instance;

  public PlayerEcholocaization me;

  public float cooldown;
  public float recoverAt;
  public Slider clapSlider;

  private AudioSource audioSource;

  private void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
      me = this;
    }

    if (Instance != this) Destroy(this);
  }

  private void Start()
  {
    recoverAt = 0;
    audioSource = GetComponent<AudioSource>();
    clapSlider.maxValue = cooldown;
  }

  private void Update()
  {
    if (recoverAt > 0) recoverAt -= Time.deltaTime;
    clapSlider.value = cooldown - recoverAt;
    if (!GameManager.Instance.CanMove()) return;
    if (recoverAt > 0) return;

    if (Input.GetButtonUp("Fire1"))
    {
      audioSource.Play();
      ObjectManager.Instance.MakeClick(transform.position);
      recoverAt = cooldown;
    }
  }


}
