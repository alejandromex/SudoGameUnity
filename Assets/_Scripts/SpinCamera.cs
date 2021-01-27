using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinCamera : MonoBehaviour
{
    public float rotationSpeed = 5f;
    private float horizontalInput;

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        this.transform.Rotate(Vector3.up * Time.deltaTime * horizontalInput * rotationSpeed);
    }
}
