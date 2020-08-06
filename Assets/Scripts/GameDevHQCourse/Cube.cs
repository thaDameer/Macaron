using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    

    private void Start() 
    {
        Main.onTeleport += SetTeleportPos;    
    }
    public void SetTeleportPos(Vector3 position)
    {
        transform.position = position;
    }
    private void OnDisable() {
        Main.onTeleport -= SetTeleportPos;
    }
}
