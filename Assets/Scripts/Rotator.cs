using System;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    float originalY;
    public float speed = 1f;
    public float floatStrength = 1f;
    public Vector3 rotationValue = new Vector3(15, 30, 45);
    void Start()
    {
        this.originalY = this.transform.position.y;
    }

    void Update()
    {
        //Rotates the object 
        transform.Rotate(rotationValue * Time.deltaTime * speed); //The new vector determines the direction it is spinning. 

        transform.position = new Vector3(transform.position.x,originalY + ((float)Math.Sin(Time.time) * floatStrength ),transform.position.z);
 
    }
}
 