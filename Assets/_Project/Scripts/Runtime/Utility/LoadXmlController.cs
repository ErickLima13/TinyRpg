using System.Xml;
using UnityEngine;

public static class LoadXmlController 
{
    public static void LoadXMLData(string xml, NpcBase npcBase, NpcWithQuest npcWithQuest)
    {
        npcBase._indexDialog = 0;
        npcBase._dialogs.Clear();

        npcWithQuest._nameQuestion = null;
        npcWithQuest._question = null;
        npcWithQuest._answersList.Clear();
        npcWithQuest._targetXml.Clear();

        npcBase._endDialog.name = null;
        npcBase._endDialog.history.Clear();
        npcBase._endDialog = new();

        TextAsset xmlData = (TextAsset)Resources.Load("Npc/" + npcBase._nameNpcPath + xml);
        XmlDocument xmlDocument = new();

        xmlDocument.LoadXml(xmlData.text);

        int i = 0;

        if (xmlDocument["npc"].Attributes["quest"] != null)
        {
            int idQuest = int.Parse( xmlDocument["npc"].Attributes["quest"].Value);
            npcWithQuest._quests[idQuest, 0] = true;
        }

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
            else if (node.Name == "complete")
            {
                string nameChar = node.Attributes["name"].Value;
                npcWithQuest._questComplete.name = nameChar;
                npcWithQuest._questComplete.history = new();

                foreach (XmlNode n in node["text"].ChildNodes)
                {
                    npcWithQuest._questComplete.history.Add(n.InnerText);
                }

                foreach (XmlNode n in node["image"].ChildNodes)
                {
                    string imageName = "Sprites/" + n.InnerText;
                    npcWithQuest._questComplete.iconNpc = Resources.Load<Sprite>(imageName);
                }
            }
            else if (node.Name == "question")
            {
                npcWithQuest._nameQuestion = node.Attributes["name"].Value;
                npcWithQuest._question = node["text"].FirstChild.InnerText;
                npcWithQuest._answersList = new();
                npcWithQuest._targetXml = new();
                foreach (XmlNode n in node["answers"].ChildNodes)
                {
                    npcWithQuest._answersList.Add(n.InnerText);
                    npcWithQuest._targetXml.Add(n.Attributes["name"].Value);
                }

                foreach (XmlNode n in node["image"].ChildNodes)
                {
                    string imageName = "Sprites/" + n.InnerText;

                    npcWithQuest.spriteQuestion = Resources.Load<Sprite>(imageName);
                }
            }
        }

        SetupDialog(npcBase);

    }

    public static void SetupDialog(NpcBase npcBase)
    {
        npcBase._dialog.name = npcBase._dialogs[npcBase._indexDialog].name;
        npcBase._dialog.history = npcBase._dialogs[npcBase._indexDialog].history;
        npcBase._dialog.iconNpc = npcBase._dialogs[npcBase._indexDialog].iconNpc;
    }
}
