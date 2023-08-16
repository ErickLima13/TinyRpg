using System.Xml;
using UnityEngine;

public static class LoadXmlController
{
    public static void LoadXMLData(string xml, NpcBase npcBase, NpcWithQuest npcWithQuest, NpcDialog npcDialog)
    {
        npcDialog.SetAllDialog(false);

        npcBase.ResetBase();
        npcWithQuest.ResetQuest();

        TextAsset xmlData = (TextAsset)Resources.Load("Npc/" + npcBase._nameNpcPath + xml);
        XmlDocument xmlDocument = new();

        xmlDocument.LoadXml(xmlData.text);

        int i = 0;

        if (xmlDocument["npc"].Attributes["quest"] != null)
        {
            int idQuest = int.Parse(xmlDocument["npc"].Attributes["quest"].Value);
            npcWithQuest._quests[idQuest, 0] = true;
        }

        if (xmlDocument["npc"].Attributes["return"] != null)
        {
            npcWithQuest._returnQuest = true;
            npcBase._nameXml = xmlDocument["npc"].Attributes["return"].Value;
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
                string a = node.Attributes["name"].Value;
                string b = node["text"].FirstChild.InnerText;

                npcWithQuest.AddQuestValues(a, b);

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

        npcBase.SetupDialog();
    }
}
