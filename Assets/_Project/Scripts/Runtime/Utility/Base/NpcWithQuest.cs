using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NpcWithQuest
{
    public Inventory inventory;

    public Dialog _questComplete;

    public Sprite spriteQuestion;

    public ItemData[] _itemQuest;

    public string _nameQuestion;
    public string _question;
    public List<string> _answersList;
    public List<string> _targetXml;
    public string[] _xmlQuestComplete;

    public bool[,] _quests;
    public bool _endQuest;
    public bool _returnQuest;

    public int[] _itemQuantityQuest;
    public int _currentQuest;

    public void ResetQuest()
    {
        _nameQuestion = null;
        _question = null;
        _answersList.Clear();
        _targetXml.Clear();
        _endQuest = false;
        _returnQuest = false;
    }

    public void AddQuestValues(string nameValue, string questionValue)
    {
        _nameQuestion = nameValue;
        _question = questionValue;
        _answersList = new();
        _targetXml = new();
    }

    public bool IsQuestComplete(NpcBase npcBase, NpcDialog npcDialog)
    {
        if (_endQuest)
        {
            LoadXmlController.LoadXMLData(_xmlQuestComplete[_currentQuest], npcBase, this, npcDialog);
            return true;
        }

        return false;
    }

    public void CheckQuests(NpcBase npcBase)
    {
        for (int i = 0; i < _quests.Length / 2; i++)
        {
            if (_quests[i, 0] == true && _quests[i, 1] == false)
            {
                // Debug.Log("peguei a quest");
                _currentQuest = i;

                if (_itemQuest[_currentQuest].onlySlot)
                {
                    if (inventory.HasQuantityItems(_itemQuest[_currentQuest], _itemQuantityQuest[_currentQuest]))
                    {
                        QuestComplete(npcBase);
                    }
                }
                else
                {
                    if (inventory.HasItem(_itemQuest[_currentQuest]))
                    {
                        QuestComplete(npcBase);
                    }
                }

                break;
            }
        }
    }

    private void QuestComplete(NpcBase npc)
    {
        npc._dialog = _questComplete;
        _quests[_currentQuest, 1] = true;
        _endQuest = true;
        inventory.RemoveQuantityItems(_itemQuantityQuest[_currentQuest], _itemQuest[_currentQuest]);
    }
}
