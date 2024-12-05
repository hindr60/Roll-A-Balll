using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum CameraStyle {Fixed, Free} 

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public CameraStyle cameraStyle;
    public Transform pivot;
    public float rotationSpeed = 1f;
    public bool isPaused { get; set; }

    private Vector3 offset;
    private Vector3 pivotOffset;

   

     void Start()
    {
        pivotOffset = pivot.position - player.transform.position;
        //Set the offset of the camera based on the player's position
        offset = transform.position - player.transform.position;
        //This calculates the distance between the player position and the camera.
    }

    
    void LateUpdate()
    {

        if (cameraStyle == CameraStyle.Fixed)
        {
            //Make the transform position of the camera follow the players transform position
            transform.position = player.transform.position + offset;
        }
        
        if (cameraStyle == CameraStyle.Free)
        {
            pivot.transform.position = player.transform.position + pivotOffset;
            Quaternion turnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);
            offset = turnAngle * offset;
            transform.position = pivot.transform.position + offset;
            transform.LookAt(pivot);
        }
    }

}
