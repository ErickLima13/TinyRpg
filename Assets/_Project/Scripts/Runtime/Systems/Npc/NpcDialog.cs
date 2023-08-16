using UnityEngine;

public class NpcDialog : MonoBehaviour
{
    public NpcBase npcBase;
    public NpcWithQuest npcWithQuest;

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
        LoadXmlController.LoadXMLData(npcBase._nameXml, npcBase, npcWithQuest,this);

        npcWithQuest._quests = new bool[2, 2];
        npcWithQuest._quests[0, 0] = false;
        npcWithQuest._quests[0, 1] = false;
        npcWithQuest._quests[1, 0] = false;
        npcWithQuest._quests[1, 1] = false;
    }

    public void NextDialog()
    {
        if (npcWithQuest.IsQuestComplete(npcBase, this))
        {
            return;
        }
        else
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

                npcBase.SetupDialog();
            }
            else
            {
                npcBase._dialog = npcBase._endDialog;
            }
        }
    }

    public void ChooseXml(int valueXml)
    {
        LoadXmlController.LoadXMLData(npcWithQuest._targetXml[valueXml], npcBase, npcWithQuest,this);
    }

    public bool HasQuestion()
    {
        return npcWithQuest._question != null;
    }
}
