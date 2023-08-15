using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    public event Action OnEndDialog;

    [SerializeField] private NpcDialog _npcDialog;

    [Header("Dialog")]
    [SerializeField] private GameObject painelDialog;
    [SerializeField] private GameObject _buttonOpen;
    [SerializeField] private TextMeshProUGUI nameNpc;
    [SerializeField] private TextMeshProUGUI converseNpc;
    [SerializeField] private Image imageNpc;
    [SerializeField] private Queue<string> converse = new();

    [Header("Question")]
    [SerializeField] private Image imageNpcQuestion;
    [SerializeField] private GameObject _painelQuestion;
    [SerializeField] private TextMeshProUGUI _questionText;
    [SerializeField] private TextMeshProUGUI nameNpcQuestion;
    [SerializeField] private Button[] _answerButtons;
    [SerializeField] private List<TextMeshProUGUI> _answersText = new();



    private void Start()
    {
        foreach (var answer in _answerButtons)
        {
            _answersText.Add(answer.GetComponent<TextMeshProUGUI>());
        }

        for (int i = 1; i > _answerButtons.Length; i--)
        {
            _answerButtons[i].onClick.RemoveAllListeners();
            _answerButtons[i].onClick.AddListener(() => TakeXml(i));
        }

        painelDialog.SetActive(false);
        _painelQuestion.SetActive(false);
        _buttonOpen.SetActive(false);
    }

    public void StartTalk(NpcDialog currentNpc)
    {
        _npcDialog = currentNpc;
        painelDialog.SetActive(true);
        converse.Clear();
        nameNpc.text = _npcDialog.npcBase._dialog.name;
        imageNpc.sprite = _npcDialog.npcBase._dialog.iconNpc;

        foreach (string talk in _npcDialog.npcBase._dialog.history)
        {
            converse.Enqueue(FormatTextController.FormatText(talk, _npcDialog.npcBase._dialog.name));
        }

        for (int i = 1; i > _answerButtons.Length; i--)
        {
            _answerButtons[i].onClick.RemoveAllListeners();
            _answerButtons[i].onClick.AddListener(() => TakeXml(i));
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
        StartCoroutine(FormatTextController.TypewriterMethod(t,converseNpc));
    }

    public void EndTalk()
    {
        _npcDialog.NextDialog();

        if (_npcDialog.GetBoolContinue() && !_npcDialog.GetAllDialog())
        {
            StartTalk(_npcDialog);
        }
        else if (_npcDialog.HasQuestion())
        {
            nameNpcQuestion.text = _npcDialog._nameQuestion;
            _questionText.text = _npcDialog._question;
            imageNpcQuestion.sprite = _npcDialog.spriteQuestion;

            int i = 0;
            foreach (string a in _npcDialog._answersList)
            {
                _answerButtons[i].gameObject.SetActive(true);
                _answersText[i].text = a;
                i++;
            }

            _painelQuestion.SetActive(true);

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

    public void TakeXml(int value)
    {
        _npcDialog.SetAllDialog(false);
        _painelQuestion.SetActive(false);
        _npcDialog.ChooseXml(value);
        StartTalk(_npcDialog);

    }
}
