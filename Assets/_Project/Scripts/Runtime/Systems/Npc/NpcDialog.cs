using UnityEngine;

public class NpcDialog : MonoBehaviour
{
    public NpcBase npcBase;
    public NpcWithQuest npcWithQuest;

    private DialogController dialogController;

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
        dialogController = FindObjectOfType<DialogController>();
        LoadXmlController.LoadXMLData(npcBase._nameXml, npcBase, npcWithQuest);

        npcWithQuest._quests = new bool[2, 2];
        npcWithQuest._quests[0, 0] = false;
        npcWithQuest._quests[0, 1] = false;
        npcWithQuest._quests[1, 0] = false;
        npcWithQuest._quests[1, 1] = false;
    }

    public void NextDialog()
    {
        print("Quest : " + npcWithQuest._quests[0, 0] + "-" + npcWithQuest._quests[0, 1]);

        if (!npcBase._allDialog)
        {
            npcBase._indexDialog++;

            if (npcBase._indexDialog >= npcBase._dialogs.Count)
            {
                npcBase._allDialog = true;
                npcBase._dialog = npcBase._endDialog;
                return;
            }

            LoadXmlController.SetupDialog(npcBase);
        }
        else
        {
            npcBase._dialog = npcBase._endDialog;
        }
    }

    public void ChooseXml(int valueXml)
    {
        LoadXmlController.LoadXMLData(npcWithQuest._targetXml[valueXml], npcBase, npcWithQuest);
    }

    public bool HasQuestion()
    {
        return npcWithQuest._question != null;
    }
}
