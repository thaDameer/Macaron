using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            
            transform.position = Vector3.Lerp(fromPos, startPos, (timer/duration));
            timer+=Time.deltaTime;
            yield return null;
        }
        GameManager.instance.sGamePlaying.OnEnterState();
    }

    [SerializeField]
    float frequency = 25;
    float seed; 
    [SerializeField]
    Vector3 maxTranslationShake = Vector3.one * 0.5f;
    [SerializeField]
    Vector3 maxAngularShake = Vector3.one * 2;
    [SerializeField]
    float recoverySpeed = 1.5f;
    float trauma = 1;
    public IEnumerator ShootCameraShake()
    {
        seed = Random.value;
        shakeCounter = shakeTime;
        isFollowing = true;
        while(shakeCounter > 0)
        {
            shakeCounter -= Time.deltaTime;
            //mainCamera.transform.localPosition = new Vector2(Random.Range(-1,1),Random.Range(-1,1)) * 0.3f;
            mainCamera.transform.localPosition = new Vector3(
                maxTranslationShake.x * (Mathf.PerlinNoise(seed,Time.time * frequency) * 2-1),
                maxTranslationShake.y * (Mathf.PerlinNoise(seed +1, Time.time * frequency) * 2-1),
                maxTranslationShake.z * (Mathf.PerlinNoise(seed + 2, Time.time * frequency) * 2-1) *trauma);
            
            // mainCamera.transform.localRotation = Quaternion.Euler(new Vector3(
            //     60,
            //     maxAngularShake.y * (Mathf.PerlinNoise(seed+4, Time.time * frequency) * 2-1),
            //     maxAngularShake.z * (Mathf.PerlinNoise(seed+5, Time.time * frequency) * 2-1)) * trauma);

                trauma = Mathf.Clamp01(trauma - recoverySpeed * Time.deltaTime);

            //     Debug.Log(trauma);

            yield return new WaitForEndOfFrame();
        }
        //mainCamera.transform.position = Vector3.zero;
    }
    
}
