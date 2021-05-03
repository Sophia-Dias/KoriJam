using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialog", menuName = "KoriJam/Dialog", order = 0)]
public class DialogObject : ScriptableObject
{
  [TextArea(5, 20)]
  public List<string> texts;
}