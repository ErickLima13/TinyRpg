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
