﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Animator;

public static class AnimID 
{
    public enum AnimIDEnum
    {
        fadeOut 
    }

    public static readonly int
        
        scaleUp = StringToHash("scaleUp"),
        scaleDown = StringToHash("scaleDown"),
        bump = StringToHash("bump"),
        pulse = StringToHash("pulse"),
        landing = StringToHash("landing"),
        idle = StringToHash("idle"),
        stretch = StringToHash("stretch"),
        fadeIn = StringToHash("fadeIn"),
        fadeOut = StringToHash("fadeOut"),
        closed = StringToHash("closed");

   
}
