using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class NpcDialog : MonoBehaviour
{
    public Dialog _dialog;

    private int _indexDialog;

    [SerializeField] private List<Dialog> _dialogs;
    [SerializeField] private Dialog _endDialog;

    [SerializeField] private bool _allDialog;
    [SerializeField] private bool _continue;
    [SerializeField] private string _nameXml;

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
        LoadXMLData();
    }


    public void NextDialog()
    {
        if (!_allDialog)
        {
            _indexDialog++;

            if (_indexDialog >= _dialogs.Count)
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
        _dialog.iconNpc = _dialogs[_indexDialog].iconNpc;
    }

    private void LoadXMLData()
    {
        TextAsset xmlData = (TextAsset)Resources.Load(_nameXml);
        XmlDocument xmlDocument = new();

        xmlDocument.LoadXml(xmlData.text);

        int i = 0;

        foreach (XmlNode node in xmlDocument["npc"].ChildNodes)
        {
            if (node.Name == "dialog")
            {
                _dialogs.Add(new Dialog());
                string nameChar = node.Attributes["name"].Value;
                _dialogs[i].name = nameChar;
                _dialogs[i].history = new();

                foreach (XmlNode n in node["text"].ChildNodes)
                {
                    _dialogs[i].history.Add(n.InnerText);
                }

                foreach (XmlNode n in node["image"].ChildNodes)
                {
                    string imageName = "Sprites/" + n.InnerText;

                    _dialogs[i].iconNpc = Resources.Load<Sprite>(imageName);
                }

                i++;
            }
            else if (node.Name == "enddialog")
            {
                string nameChar = node.Attributes["name"].Value;
                _endDialog.name = nameChar;
                _endDialog.history = new();

                foreach (XmlNode n in node["text"].ChildNodes)
                {
                    _endDialog.history.Add(n.InnerText);
                }

                foreach (XmlNode n in node["image"].ChildNodes)
                {
                    string imageName = "Sprites/" + n.InnerText;
                    _endDialog.iconNpc = Resources.Load<Sprite>(imageName);
                }
            }
        }

        SetupDialog();

    }
}
