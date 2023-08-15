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
}
