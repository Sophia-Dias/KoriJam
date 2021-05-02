using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEcholocaization : MonoBehaviour
{
  public static PlayerEcholocaization Instance;

  public PlayerEcholocaization me;

  private void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
      me = this;
    }

    if (Instance != this) Destroy(this);
  }

  private void Update()
  {
    if (Input.GetButtonUp("Fire1"))
    {
      ObjectManager.Instance.AddOutLine(transform.position);
    }
  }


}
