using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedArrowObject : MonoBehaviour
{
    public MeshRenderer mesh;
    private Material material;
    private Color startColor;
    public Color detectedColor;

    private void Start() 
    {
        material = mesh.material;
        startColor = material.color;
    }
    private void OnTriggerEnter(Collider other) 
    {
        Player player = other.GetComponentInParent<Player>();
        SwitchArrowColor(player);    
    }
    private void OnTriggerExit(Collider other) 
    {
        SwitchArrowColor(false);    
    }
    public void SwitchArrowColor(bool shouldChangeColor)
    {
        if(shouldChangeColor)
        {
            material.color = detectedColor;
        } 
        else
        {
            material.color = startColor;
        }
    }
}
