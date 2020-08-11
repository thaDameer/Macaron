using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class LevelIcon : MonoBehaviour, ISelectHandler,IDeselectHandler, IPointerEnterHandler 
{
    
    public Timer iconTimer = new Timer(0.04f);
    public Sprite buttonSpriteS;
    public RectTransform rectTransform;
    public Image levelImage;
    private Color startColor;
    public Color pressedColor;
    public RectTransform bannerIcon;
    public Text levelText;
    private void Start() {
        
        startColor = levelImage.color;
        bannerIcon.gameObject.SetActive(false);
        bannerIcon.sizeDelta = Vector3.zero;
    }
    
    public void PressButton()
    {
        iconTimer.StartTimer();
        levelImage.DOColor(pressedColor, iconTimer.elapsedPercent).OnComplete(ResetButton);
        
    }

    public void SetLevelName(string levelname)
    {
        levelText.text = levelname;
    }

    private void ResetButton()
    {
        levelImage.color = startColor;
        levelImage.sprite = buttonSpriteS;
    }
    private Timer scaleTimer = new Timer(0.1f);
    Tweener bannerTween;
    Tweener selectionTween;
    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("select");
        bannerIcon.gameObject.SetActive(true);
        scaleTimer.StartTimer();
        float scaleSize = 0.2f;
        Debug.Log(scaleTimer.elapsedPercent);
        Vector2 endScale = new Vector2(rectTransform.localScale.x + scaleSize, rectTransform.localScale.y + scaleSize);
        selectionTween = levelImage.rectTransform.DOScale(endScale,scaleTimer.elapsedPercent).OnComplete(DebugTimer);
        bannerTween = bannerIcon.DOScale(Vector3.one,scaleTimer.elapsedPercent).SetEase(Ease.InCubic);
    }
    void DebugTimer()
    {
        Debug.Log(scaleTimer.elapsedPercent);
    }
    void ReturnToStartScale()
    {
         //
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("pointing");
    }

    public void OnDeselect(BaseEventData eventData)
    {
         selectionTween.Kill();
         levelImage.rectTransform.localScale = Vector3.one;
         bannerIcon.gameObject.SetActive(false);
         bannerIcon.sizeDelta = Vector3.zero;
    }
}
