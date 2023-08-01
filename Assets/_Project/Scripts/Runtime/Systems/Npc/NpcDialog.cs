using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDialog : MonoBehaviour
{
    public Dialog _dialog;

    private int _indexDialog;

    [SerializeField] private Dialog[] _dialogs;
    [SerializeField] private Dialog _endDialog;

    [SerializeField] private bool _allDialog;
    [SerializeField] private bool _continue;

    public bool GetBoolContinue()
    {
        return _continue;
    }

    public bool GetAllDialog()
    {
        return _allDialog;
    }

    private void Start()
    {
        SetupDialog();
    }


    public void NextDialog()
    {
        if (!_allDialog)
        {
            _indexDialog++;

            if (_indexDialog >= _dialogs.Length)
            {
                _allDialog = true;
                _dialog = _endDialog;
                return;
            }

            SetupDialog();
        }
        else
        {
            _dialog = _endDialog;
        }
      

    }

    private void SetupDialog()
    {
        _dialog.name = _dialogs[_indexDialog].name;
        _dialog.history = _dialogs[_indexDialog].history;
        _dialogs[_indexDialog].iconNpc = _dialog.iconNpc;
    }
}
