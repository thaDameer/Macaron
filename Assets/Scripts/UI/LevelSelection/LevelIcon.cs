using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using SceneLoading;

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
    public int levelID;
    private void Start() {
        
        startColor = levelImage.color;
        bannerIcon.gameObject.SetActive(false);
        bannerIcon.sizeDelta = Vector3.zero;
    }
    
    public void PressButton()
    {
        levelImage.DOColor(pressedColor, 0.2f).OnComplete(ResetButton);
        LoadManager.OnLoadNewScene(levelID.ToString(),LoadMode.Delay);
    }

    public void SetLevelName(int level)
    {
        levelID = level;
        levelText.text = levelID.ToString();
    }

    private void ResetButton()
    {
        levelImage.color = startColor;
        levelImage.sprite = buttonSpriteS;
    }
    float tweenTime = 0.3f;
    Tweener bannerTween;
    Tweener selectionTween;
    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("select");
        bannerIcon.gameObject.SetActive(true);
        float scaleSize = 0.2f;
        
        Vector2 endScale = new Vector2(rectTransform.localScale.x + scaleSize, rectTransform.localScale.y + scaleSize);
        selectionTween = levelImage.rectTransform.DOScale(endScale,tweenTime);
        bannerTween = bannerIcon.DOScale(Vector3.one,tweenTime).SetEase(Ease.InCubic);
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
