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


    public void ResetBase()
    {
        _indexDialog = 0;
        _dialogs.Clear();
        _endDialog.name = null;
        _endDialog.history.Clear();
        _endDialog = new();
    }

    public  void SetupDialog()
    {
        _dialog.name = _dialogs[_indexDialog].name;
        _dialog.history = _dialogs[_indexDialog].history;
        _dialog.iconNpc = _dialogs[_indexDialog].iconNpc;
    }
}
