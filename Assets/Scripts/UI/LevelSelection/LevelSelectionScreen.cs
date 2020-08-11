using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionScreen : UIBase
{
    public Transform selectionPanel;
    //level count for each level, might be put into level manager later
    int levelId = 1;
   public override void Awake()
   {
       base.Awake();
       Show();
       GetChildIcons(selectionPanel);
       SetIconInfo();
   }
   public List<LevelIcon> levelIcons = new List<LevelIcon>();
   public void GetChildIcons(Transform parent)
   {
       foreach (Transform item in parent)
       {
           var icon = item.GetComponent<LevelIcon>();
           if(icon)
           {
           
            levelIcons.Add(icon);
           }
       }
   }
   public void SetIconInfo()
   {
       for (int i = 0; i < levelIcons.Count; i++)
       {
           levelIcons[i].SetLevelName(levelId.ToString());
           levelId++;
       }
   }
}
