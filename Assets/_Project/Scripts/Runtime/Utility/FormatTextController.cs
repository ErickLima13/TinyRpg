using System.Collections;
using TMPro;
using UnityEngine;

public static class FormatTextController
{
    public static IEnumerator TypewriterMethod(string text, TextMeshProUGUI uiText)
    {
        uiText.text = " ";

        foreach (char t in text.ToCharArray())
        {
            uiText.text += t.ToString();
            yield return new WaitForSeconds(0.1f);
        }
    }

    public static string FormatText(string text, string nameNpc)
    {
        string temp = text;

        temp = temp.Replace("npc_name", nameNpc);

        string color = "<color=#00FF00>";
        string color2 = "</color>";

        temp = temp.Replace("cor_nova", color);
        temp = temp.Replace("fim_cor", color2);

        return temp;
    }
}
