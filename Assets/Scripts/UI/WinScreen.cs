using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WinScreen : UIBase
{
    public Animator animator;

    public override void Show()
    {
        base.Show();
        animator.SetTrigger(AnimID.scaleUp);
    }
}
