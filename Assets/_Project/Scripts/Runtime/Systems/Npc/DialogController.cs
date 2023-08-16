using System;
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
        GameStateController.ChangeState(GameState.Dialog);

        _npcDialog = currentNpc;
        _npcDialog.SetAllDialog(false);
        painelDialog.SetActive(true);
        converse.Clear();
        nameNpc.text = _npcDialog.npcBase._dialog.name;
        imageNpc.sprite = _npcDialog.npcBase._dialog.iconNpc;

        currentNpc.npcWithQuest.CheckQuests(currentNpc.npcBase);

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
        StartCoroutine(FormatTextController.TypewriterMethod(t, converseNpc));
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

            if (_npcDialog.HasQuestion())
            {
                nameNpcQuestion.text = _npcDialog.npcWithQuest._nameQuestion;
                _questionText.text = _npcDialog.npcWithQuest._question;
                imageNpcQuestion.sprite = _npcDialog.npcWithQuest.spriteQuestion;

                int i = 0;
                foreach (string a in _npcDialog.npcWithQuest._answersList)
                {
                    _answerButtons[i].gameObject.SetActive(true);
                    _answersText[i].text = a;
                    i++;
                }

                _painelQuestion.SetActive(true);
            }
            else if (_npcDialog.npcWithQuest._returnQuest)
            {
                LoadXmlController.LoadXMLData(_npcDialog.npcBase._nameXml, _npcDialog.npcBase, _npcDialog.npcWithQuest, _npcDialog);
            }
            else
            {
                _buttonOpen.SetActive(false);
                OnEndDialog?.Invoke();
            }

            if (!painelDialog.activeSelf && !_painelQuestion.activeSelf)
            {
                GameStateController.ChangeState(GameState.Gameplay);
            }
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
