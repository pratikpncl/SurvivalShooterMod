using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform target;            
    public float smoothing = 5f;        

    Vector3 offset;                     

    void Start()
    {
        //offset for camera
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        //get position of player and follow it
        Vector3 targetCamPos = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}