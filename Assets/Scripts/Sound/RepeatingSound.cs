using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingSound : SoundController
{
  public float interval;

  protected override void Update() {
    base.Update();

    if(time >= interval) {
      PlaySound();
    }
  }
}
