using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScripts : MonoBehaviour
{
    private float speed = 1f;
    private float acceleration = 0.2f;
    private float maxSpeed = 3.2f;

    [HideInInspector]
    public bool moveCamera;
    // Start is called before the first frame update
    void Start()
    {
        moveCamera = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveCamera)
            MoveCamera();
    }

    void MoveCamera()
    {
        Vector3 tmp = transform.position;

        float oldY = tmp.y;
        float newY = tmp.y - (speed * Time.deltaTime);
        tmp.y = Mathf.Clamp(tmp.y, oldY, newY);
        transform.position = tmp;

        speed += acceleration * Time.deltaTime;

        if (speed > maxSpeed)
            speed = maxSpeed;
    }
}
