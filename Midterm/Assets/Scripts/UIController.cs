using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textBestScore;
    public TextMeshProUGUI textYourScore;
    public TextMeshProUGUI textNotify;
    public TextMeshProUGUI textNew;
    public GameObject panelGameOver;

    private void Start()
    {
        textNotify.text = "";
        isShowTextNew(false);
        setScoreTxt(0);
        isShowPanelGV(false);
    }

    public void setScoreTxt(int scoretxt)
    {
        if (textScore)
        {
            textScore.text = "Score : " + scoretxt;
        }
    }
    public void setBestScoreTxt(int bestScore)
    {
        if (textBestScore)
        {
            textBestScore.text = "Best Score : " + bestScore;
        }
    }
    public void setYourScoreTxt(int yourScore)
    {
        if (textYourScore)
        {
            textYourScore.text = "Your Score : " + yourScore;
        }
    }
    public void isShowPanelGV(bool isShow)
    {
        panelGameOver.SetActive(isShow);
    }
    public void isShowTextNew(bool isShow)
    {
        textNew.enabled = isShow;
    }
    public void setNotify(string txt,Color color)
    {
        textNotify.text = txt;
        //Color aa = new Color(255f, 220f, 0f, 1);
        textNotify.color = color;
    }
    public void FadeOut()
    {
        StartCoroutine(FadeOutCR());
    }
    IEnumerator FadeOutCR()
    {
        float duration = 2f; //2s
        float currentTime = 0f;
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, currentTime / duration);
            textNotify.color = new Color(textNotify.color.r, textNotify.color.g, textNotify.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
        yield break;
    }
    
}
