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
    [SerializeField] private string _path;
    [SerializeField] private string _nameNpcPath;
    [SerializeField] private string _nameXml;


    [Header("Question System")]
    public Sprite spriteQuestion;
    public string _nameQuestion;
    public string _question;
    public List<string> _answersList;
    [SerializeField] private List<string> _targetXml;


    public bool GetBoolContinue()
    {
        return _continue;
    }

    public bool GetAllDialog()
    {
        return _allDialog;
    }


    public void SetAllDialog(bool value)
    {
        _allDialog = value;
    }

    private void Start()
    {
        LoadXMLData(_nameXml);
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

    private void LoadXMLData(string xml)
    {
        _indexDialog = 0;
        _dialogs.Clear();
        _nameQuestion = null;
        _question = null;
        _answersList.Clear();
        _targetXml.Clear();

        _endDialog.name = null;
        _endDialog.history.Clear();
        _endDialog = new();

        TextAsset xmlData = (TextAsset)Resources.Load(_path + _nameNpcPath + xml);
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
            else if (node.Name == "question")
            {
                _nameQuestion = node.Attributes["name"].Value;
                _question = node["text"].FirstChild.InnerText;
                _answersList = new();
                _targetXml = new();
                foreach (XmlNode n in node["answers"].ChildNodes)
                {
                    _answersList.Add(n.InnerText);
                    _targetXml.Add(n.Attributes["name"].Value);
                }

                foreach (XmlNode n in node["image"].ChildNodes)
                {
                    string imageName = "Sprites/" + n.InnerText;

                    spriteQuestion = Resources.Load<Sprite>(imageName);
                }
            }
        }

        SetupDialog();

    }

    public void ChooseXml(int valueXml)
    {
        LoadXMLData(_targetXml[valueXml]);
    }

    public bool HasQuestion()
    {
        return _question != null;
    }
}
