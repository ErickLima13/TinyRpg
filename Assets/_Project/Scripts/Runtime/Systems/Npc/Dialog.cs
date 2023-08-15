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

[System.Serializable]

public class NpcBase
{
    public Dialog _dialog;

    public int _indexDialog;

    public List<Dialog> _dialogs;
    public Dialog _endDialog;

    public bool _allDialog;
    public bool _continue;

    public string _nameNpcPath;
    public string _nameXml;
}
