// 引用URL: https://dkrevel.com/makegame-beginner/make-2d-action-scene-change/ (2020.10.02)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionDirector : MonoBehaviour
{
    public bool firstFadeInComp;

    private Image img;
    private float timer;
    private int frameCount;
    private bool fadeIn, fadeOut;
    private bool compFadeIn, compFadeOut;

    // Start is called before the first frame update
    void Start()
    {
        img = this.gameObject.GetComponent<Image>();

        compFadeOut = true;
        if (firstFadeInComp)
        {
            FadeInComplete();
        }
        else
        {
            StartFadeIn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (frameCount > 2) // シーン移行時の処理の重さでTime.deltaTimeが大きくなってしまうから2フレーム待つ
        {
            if (fadeIn)
            {
                FadeInUpdate();
            }
            else if (fadeOut)
            {
                FadeOutUpdate();
            }
        }
        ++frameCount;
    }

    /// <summary>
    /// フェードインを開始します
    /// </summary>
    public void StartFadeIn()
    {
        if (fadeIn)
        {
            return;
        }
        fadeIn = true;
        compFadeIn = false;
        timer = 0.0f;
        img.color = Color.white;
        img.fillAmount = 1;
        img.raycastTarget = true;
    }

    /// <summary>
    /// フェードインを進行させます
    /// </summary>
    private void FadeInUpdate()
    {
        if (timer < 1f)
        {
            img.color = new Color(1, 1, 1, 1 - timer);
            img.fillAmount = 1 - timer;
        }
        else
        {
            FadeInComplete();
        }
        timer += Time.deltaTime;
    }

    /// <summary>
    /// フェードインを完了させます
    /// </summary>
    private void FadeInComplete()
    {
        img.color = Color.clear;
        img.fillAmount = 0;
        img.raycastTarget = false;
        timer = 0.0f;
        fadeIn = false;
        compFadeIn = true;
    }

    /// <summary>
    /// フェードインが完了したかどうかを返します
    /// </summary>
    /// <returns>フェードインが完了したかどうか</returns>
    public bool IsFadeInComplete()
    {
        return compFadeIn;
    }

    /// <summary>
    /// フェードアウトを開始します
    /// </summary>
    public void StartFadeOut()
    {
        if (fadeIn || fadeOut)
        {
            return;
        }
        fadeOut = true;
        compFadeOut = false;
        timer = 0.0f;
        img.color = Color.clear;
        img.fillAmount = 0;
        img.raycastTarget = true;
    }

    /// <summary>
    /// フェードアウトを進行させます
    /// </summary>
    private void FadeOutUpdate()
    {
        if (timer < 1f)
        {
            img.color = new Color(1, 1, 1, timer);
            img.fillAmount = timer;
        }
        else
        {
            FadeOutComplete();
        }
        timer += Time.deltaTime;
    }

    /// <summary>
    /// フェードアウトを完了させます
    /// </summary>
    private void FadeOutComplete()
    {
        img.color = Color.white;
        img.fillAmount = 1;
        img.raycastTarget = false;
        timer = 0.0f;
        fadeOut = false;
        compFadeOut = true;
    }

    /// <summary>
    /// フェードアウトが完了したかどうかを返します
    /// </summary>
    /// <returns>フェードアウトが完了したかどうか</returns>
    public bool IsFadeOutComplete()
    {
        return compFadeOut;
    }
}