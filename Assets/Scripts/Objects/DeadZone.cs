using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum ObjectRotation 
    {
        RIGHT,
        LEFT, 
        UP
    }
public class DeadZone : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public void SetRotation(ObjectRotation rot)
    {
        switch(rot)
        {
            case ObjectRotation.RIGHT:
            transform.rotation = Quaternion.Euler(0,90,0);
            break;

            case ObjectRotation.LEFT:
            transform.rotation = Quaternion.Euler(0,-90,0);
            break;

            case ObjectRotation.UP:
            transform.rotation = Quaternion.Euler(Vector3.zero);
            break;
        }
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetRotation(ObjectRotation.UP);
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetRotation(ObjectRotation.LEFT);
        }
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetRotation(ObjectRotation.RIGHT);
        }
    }
    
    public void HideMesh()
    {
        meshRenderer.enabled = false;
    }
}
