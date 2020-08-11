using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum CameraMode
{
    NONE,
    START_PAN,
    IGNORING_X_MOVEMENT,
    FOLLOWING_ALL_AXIS,
    RETURNING
}

public class CameraManager : MonoBehaviour
{
    public CameraMode camMode;
    public float cameraSmoothSpeed = 5;
    Vector3 followPos;
    public Camera mainCamera;
    bool isFollowing = false;
    public Vector3 offset = new Vector3(0,0,10);
    protected Vector3 startPos;
    public Transform currentFollowObject;
    public float shakeTime;
    float shakeCounter;
    
    private void Start() 
    {
        startPos = transform.position;
    }
    void FixedUpdate()
    {
        if(currentFollowObject == null) return; 
        if(!isFollowing)return;
        
        switch(camMode)
        {
            case CameraMode.NONE:
            //use when player is dead!
            break;
            case CameraMode.START_PAN:
            //use when starting 
            break;
            case CameraMode.IGNORING_X_MOVEMENT:
            CameraFollowingIgnoringXMovement();
            break;
            case CameraMode.FOLLOWING_ALL_AXIS:
            CameraFollowingAllAxis();
            //following on xyz
            break;
            case CameraMode.RETURNING:
            break;
        }

    }
    void CameraFollowingAllAxis()
    {
        followPos = followPos = new Vector3(currentFollowObject.position.x, currentFollowObject.position.y + offset.y,currentFollowObject.position.z + offset.z);
        transform.position = Vector3.Lerp(transform.position, followPos, cameraSmoothSpeed*Time.fixedDeltaTime);
    }

    private void CameraFollowingIgnoringXMovement()
    {
        followPos = new Vector3(0, currentFollowObject.position.y + offset.y,currentFollowObject.position.z + offset.z);
        transform.position = Vector3.Lerp(transform.position, followPos,cameraSmoothSpeed*Time.fixedDeltaTime);   
    }

    public void UpdateObjectToFollowAndMode(Transform newObject, CameraMode mode)
    {

        camMode = mode;
        currentFollowObject = newObject;
    }
    float timer;
    public float duration = 1f;
    public IEnumerator ReturnToStartPosition()
    {
        camMode = CameraMode.RETURNING;
        Vector3 fromPos = transform.position;
        while(timer<duration)
        {
            startPos.y = offset.y;
            transform.position = Vector3.Lerp(fromPos, offset, (timer/duration));
            timer+=Time.deltaTime;
            yield return null;
        }
        GameManager.instance.sGamePlaying.OnEnterState();
    }
    public Timer shakeTimer;
    public float shakeDuration = 1.2f;
    public float shakeStrength = 1.5f;
    public int vibrato = 1;
    public float randomness = 10;
    public bool fadeOut = true; 
    public void CameraShake(ShakeType shakeType, bool startFollow = false)
    {
        shakeTimer = new Timer(shakeDuration);
        shakeTimer.StartTimer();
        switch(shakeType)
        {
            case ShakeType.Shake:
            mainCamera.DOShakePosition(shakeTimer.elapsedPercent,shakeStrength,vibrato,randomness, fadeOut);
            break;

            case ShakeType.ShakeRot:
            mainCamera.DOShakeRotation(shakeTimer.elapsedPercent,shakeStrength,vibrato,randomness,fadeOut);
            break;
        }
        isFollowing = true;
    }

}
public enum ShakeType
{
    Shake,
    ShakeRot
}
