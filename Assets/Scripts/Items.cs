using System;
using UnityEngine;

#nullable disable
[Serializable]
public class Items
{
  public int ID;
  [TextArea]
  public string name;
  public int amount;
  public float weight;
}
