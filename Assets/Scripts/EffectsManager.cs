using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public enum Party
{
    landingParty,
}
public class EffectsManager : MonoBehaviour
{
    Timer partyTimer =new Timer();
    public GameObject landingParty;
    public Animator transitionAnimator;

    public void FadeTransition(string animId)
    {
        transitionAnimator.SetTrigger(animId);
        //end value 80
        Vector3 endpos = new Vector3(0,0,0);
        
    }
    public void PlayParty(Vector3 pos, Quaternion rot, float lenght, Transform partyParent = null)
    {   
        if(partyParent)
        {
            landingParty.transform.parent = partyParent;
        }
        partyTimer = new Timer(lenght);
        StartCoroutine(MultiPartyRoutine(pos, rot,lenght));
    }
    //dirty fix, neeed to rework the  whole effects script
    public void FlashMaterial(Material material, Color color)
    {
        var materialColor = material.color;
        StartCoroutine(FlashMaterial_CO(material, Color.white, color));
    }
    IEnumerator FlashMaterial_CO(Material mat,Color startCol, Color flashCol)
    {
        int flashCount = 0;
        while(flashCount < 4)
        {
            mat.color = flashCol;
            yield return new WaitForSeconds(0.1f);
            mat.color = startCol;
            yield return new WaitForSeconds(0.1f);
            flashCount++;
        }
        mat.color = startCol;
    }
    IEnumerator MultiPartyRoutine(Vector3 position, Quaternion rotation, float time)
    {
        partyTimer.StartTimer();
        landingParty.transform.position = position;
        landingParty.transform.rotation = rotation;
        landingParty.gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        landingParty.gameObject.SetActive(false);
        landingParty.transform.parent = this.transform;
    }
}
