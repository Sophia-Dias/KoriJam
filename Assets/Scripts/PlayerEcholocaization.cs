using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEcholocaization : MonoBehaviour
{
  public static PlayerEcholocaization Instance;

  public PlayerEcholocaization me;

  public float cooldown;
  public float recoverAt;

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
  }

  private void Update()
  {
    recoverAt -= Time.deltaTime;
    if (!GameManager.Instance.CanMove()) return;
    if (recoverAt > 0) return;

    if (Input.GetButtonUp("Fire1"))
    {
      ObjectManager.Instance.AddOutLine(transform.position);
      recoverAt = cooldown;
    }
  }


}
