using System.Collections.Generic;

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
