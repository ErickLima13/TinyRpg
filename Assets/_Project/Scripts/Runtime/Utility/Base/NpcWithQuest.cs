using Codice.CM.Client.Differences.Merge;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NpcWithQuest
{
    public Dialog _questComplete;

    public Sprite spriteQuestion;

    public string _nameQuestion;
    public string _question;

    public List<string> _answersList;
    public List<string> _targetXml;

    public bool[,] _quests;

    public ItemData[] _itemQuest;

    public int[] _itemQuantityQuest;
    
    public bool _endQuest;

    public string[] _xmlQuestComplete;

    public int _currentQuest;

    public Inventory inventory;

    public bool IsQuestComplete(NpcBase npcBase,NpcDialog npcDialog)
    {
        if (_endQuest)
        {
            LoadXmlController.LoadXMLData(_xmlQuestComplete[_currentQuest], npcBase, this,npcDialog);       
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
                Debug.Log("peguei a quest");

                _currentQuest = i;

                if (_itemQuest[_currentQuest].onlySlot)
                {
                    if (inventory.HasQuantityItems(_itemQuest[_currentQuest], _itemQuantityQuest[_currentQuest]))
                    {
                        npcBase._dialog = _questComplete;

                        _quests[i, 1] = true;

                        inventory.RemoveQuantityItems(_itemQuantityQuest[_currentQuest], _itemQuest[_currentQuest]);

                        _endQuest = true;

                        Debug.Log("tenho o item");
                    }
                }
                else
                {
                    if (inventory.HasItem(_itemQuest[_currentQuest]))
                    {
                        npcBase._dialog = _questComplete;

                        _quests[i, 1] = true;

                        inventory.RemoveItem(_itemQuest[_currentQuest]);

                        _endQuest = true;

                        Debug.Log("tenho o item");
                    }
                }
              

                break;
            }
        }
    }
}
