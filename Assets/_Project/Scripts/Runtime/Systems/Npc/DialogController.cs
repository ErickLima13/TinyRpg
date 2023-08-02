using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    public event Action OnEndDialog;

    private NpcDialog _npcDialog;

    [SerializeField] private GameObject painelDialog;
    [SerializeField] private GameObject _buttonOpen;

    [SerializeField] private TextMeshProUGUI nameNpc;
    [SerializeField] private TextMeshProUGUI converseNpc;

    [SerializeField] private Image imageNpc;

    [SerializeField] private Queue<string> converse = new();

    private void Start()
    {
        painelDialog.SetActive(false);
        _buttonOpen.SetActive(false);
    }

    public void StartTalk(NpcDialog currentNpc)
    {
        _npcDialog = currentNpc;
        painelDialog.SetActive(true);
        converse.Clear();
        nameNpc.text = _npcDialog._dialog.name;
        imageNpc.sprite = _npcDialog._dialog.iconNpc;

        foreach (string talk in _npcDialog._dialog.history)
        {
            converse.Enqueue(FormatText(talk));
        }

        NextTalk();
    }

    public void NextTalk()
    {
        _buttonOpen.SetActive(true);
        StopAllCoroutines();

        if (converse.Count == 0)
        {
            EndTalk();
            return;
        }

        string t = converse.Dequeue();
        StartCoroutine(TypewriterMethod(t));
    }

    public void EndTalk()
    {
        _npcDialog.NextDialog();

        if (_npcDialog.GetBoolContinue() && !_npcDialog.GetAllDialog())
        {
            StartTalk(_npcDialog);
        }
        else
        {
            painelDialog.SetActive(false);
            _buttonOpen.SetActive(false);
            OnEndDialog?.Invoke();
        }
    }

    public bool CanTalk()
    {
        return painelDialog.activeSelf;
    }

    private IEnumerator TypewriterMethod(string text)
    {
        converseNpc.text = " ";

        foreach (char t in text.ToCharArray())
        {
            converseNpc.text += t.ToString();
            yield return new WaitForSeconds(0.1f);
        }
    }

    private string FormatText(string text)
    {
        string temp = text;

        temp = temp.Replace("npc_name", _npcDialog._dialog.name);

        string color = "<color=#00FF00>";
        string color2 = "</color>";

        temp = temp.Replace("cor_nova", color);
        temp = temp.Replace("fim_cor", color2);

        return temp;
    }
}
