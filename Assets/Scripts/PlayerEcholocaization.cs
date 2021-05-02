using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEcholocaization : MonoBehaviour
{
  public static PlayerEcholocaization Instance;
  public List<Object> allObjects = new List<Object>();
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
      AddOutLine();
    }
  }

  private void AddOutLine()
  {
    foreach (Object gameObject in allObjects)
    {
      gameObject.MakeVisible(transform.position);
    }
  }
}
