using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NpcWithQuest
{
    public Sprite spriteQuestion;
    public string _nameQuestion;
    public string _question;
    public List<string> _answersList;
    public List<string> _targetXml;
}
