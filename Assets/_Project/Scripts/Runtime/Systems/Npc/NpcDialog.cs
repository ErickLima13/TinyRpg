using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class NpcDialog : MonoBehaviour
{
    public NpcBase npcBase;

    [Header("Question System")]
    public Sprite spriteQuestion;
    public string _nameQuestion;
    public string _question;
    public List<string> _answersList;
    [SerializeField] private List<string> _targetXml;


    public bool GetBoolContinue()
    {
        return npcBase._continue;
    }

    public bool GetAllDialog()
    {
        return npcBase._allDialog;
    }


    public void SetAllDialog(bool value)
    {
        npcBase._allDialog = value;
    }

    private void Start()
    {
        LoadXMLData(npcBase._nameXml);
    }


    public void NextDialog()
    {
        if (!npcBase._allDialog)
        {
            npcBase._indexDialog++;

            if (npcBase._indexDialog >= npcBase._dialogs.Count)
            {
                npcBase._allDialog = true;
                npcBase._dialog = npcBase._endDialog;
                return;
            }

            SetupDialog();
        }
        else
        {
            npcBase._dialog = npcBase._endDialog;
        }
    }

    private void SetupDialog()
    {
        npcBase._dialog.name = npcBase._dialogs[npcBase._indexDialog].name;
        npcBase._dialog.history = npcBase._dialogs[npcBase._indexDialog].history;
        npcBase._dialog.iconNpc = npcBase._dialogs[npcBase._indexDialog].iconNpc;
    }

    private void LoadXMLData(string xml)
    {
        npcBase._indexDialog = 0;
        npcBase._dialogs.Clear();
        _nameQuestion = null;
        _question = null;
        _answersList.Clear();
        _targetXml.Clear();

        npcBase._endDialog.name = null;
        npcBase._endDialog.history.Clear();
        npcBase._endDialog = new();

        TextAsset xmlData = (TextAsset)Resources.Load("Npc/" + npcBase._nameNpcPath + xml);
        XmlDocument xmlDocument = new();

        xmlDocument.LoadXml(xmlData.text);

        int i = 0;

        foreach (XmlNode node in xmlDocument["npc"].ChildNodes)
        {
            if (node.Name == "dialog")
            {
                npcBase._dialogs.Add(new Dialog());
                string nameChar = node.Attributes["name"].Value;
                npcBase._dialogs[i].name = nameChar;
                npcBase._dialogs[i].history = new();

                foreach (XmlNode n in node["text"].ChildNodes)
                {
                    npcBase._dialogs[i].history.Add(n.InnerText);
                }

                foreach (XmlNode n in node["image"].ChildNodes)
                {
                    string imageName = "Sprites/" + n.InnerText;

                    npcBase._dialogs[i].iconNpc = Resources.Load<Sprite>(imageName);
                }

                i++;
            }
            else if (node.Name == "enddialog")
            {
                string nameChar = node.Attributes["name"].Value;
                npcBase._endDialog.name = nameChar;
                npcBase._endDialog.history = new();

                foreach (XmlNode n in node["text"].ChildNodes)
                {
                    npcBase._endDialog.history.Add(n.InnerText);
                }

                foreach (XmlNode n in node["image"].ChildNodes)
                {
                    string imageName = "Sprites/" + n.InnerText;
                    npcBase._endDialog.iconNpc = Resources.Load<Sprite>(imageName);
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
