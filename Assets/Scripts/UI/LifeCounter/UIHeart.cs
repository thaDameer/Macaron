using UnityEngine;
using UnityEngine.UI;
public class UIHeart : MonoBehaviour
{
    public Image heartImage;
    public RectTransform heartRtransform;
    public Sprite drainedHeart;
    private Sprite fullHeart;
    public bool isAlive;
    public Animator animator;

    private void Awake() 
    {
        fullHeart = heartImage.sprite;    
    }

    public void SetupHeart(Vector3 positionOffset)
    {
        //set heart sprite and rect transform position
        heartImage.sprite = fullHeart;
        heartRtransform.localPosition = positionOffset;
        isAlive = true;
    }
    public void LoseALife()
    {
        heartImage.sprite = drainedHeart;
        animator.SetTrigger(AnimID.pulse);
        isAlive = false;
    }
    public void GainALife()
    {
        heartImage.sprite = fullHeart;
        animator.SetTrigger(AnimID.scaleUp);
        isAlive = true;
    }
}
