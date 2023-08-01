using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    public string name;
    public Sprite iconNpc;
    [TextArea]
    public List<string> history;
}
