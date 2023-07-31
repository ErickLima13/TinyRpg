using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    [SerializeField] private GameObject painelDialog;

    [SerializeField] private TextMeshProUGUI nameNpc;
    [SerializeField] private TextMeshProUGUI converseNpc;

    [SerializeField] private Queue<string> converse = new();


    private void Start()
    {
        painelDialog.SetActive(false);
    }

    public void StartTalk(Dialog dialog)
    {
        painelDialog.SetActive(true);
        converse.Clear();
        nameNpc.text = dialog.name;

        foreach(string talk in dialog.history)
        {
            converse.Enqueue(talk);
        }

        NextTalk();
    }

    public void NextTalk()
    {
        if(converse.Count <= 0)
        {
            EndTalk();
            return;
        }


        string t = converse.Dequeue();
        converseNpc.text = t;   
    }

    public void EndTalk()
    {
        painelDialog.SetActive(false);
    }

}
