using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Animator;

public static class AnimID 
{
    public enum AnimIDEnum
    {

    }

    public static readonly int
        
        scaleUp = StringToHash("scaleUp"),
        scaleDown = StringToHash("scaleDown"),
        bump = StringToHash("bump"),
        landing = StringToHash("landing"),
        idle = StringToHash("idle"),
        stretch = StringToHash("stretch");

   
}
