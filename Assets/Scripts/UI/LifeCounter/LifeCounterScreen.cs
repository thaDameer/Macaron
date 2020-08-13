using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCounterScreen : UIBase
{
    private UIHeart heartPrefab;
    public List<UIHeart> hearts = new List<UIHeart>();
    public Transform heartContainer;
    public Vector3 heartPosition;

    public override void Awake()
    {
        Show();
    }
    private void Start() 
    {
        if(GameManager.instance.sceneHandler)
        {
            Show();
        }else
        {
            Hide();
        }
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.O))
        {
            LifeLost();
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            LifeGained();
        }
    }

    ///<summary> Setup the hearts in the Life Counter UI at start of each level</summary>
    public void SetupHearts(int amountOfHearts)
    {
        StartCoroutine(SetupHearts_CO(amountOfHearts));
    }
    private IEnumerator SetupHearts_CO(int heartCount)
    {

        heartPrefab =  Resources.Load<UIHeart>("UIHeart");
        for (int i = 0; i < heartCount; i++)
        {
            yield return new WaitForSeconds(0.2f);
            var newHeart = Instantiate(heartPrefab,heartContainer);
            newHeart.SetupHeart(heartPosition);
            hearts.Add(newHeart);
            newHeart.animator.SetTrigger(AnimID.scaleUp);
            //offset position on the UI heart
            heartPosition.x += 70f;
        }
    }

    public void LifeLost()
    {
        for (int i = hearts.Count -1; i >= 0; i--)
        {
            if(hearts[i].isAlive)
            {
                hearts[i].LoseALife();
                break;
            }
        }
    }
    public void LifeGained()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if(!hearts[i].isAlive)
            {
                hearts[i].GainALife();
                break;
            }
        }
    }
}
