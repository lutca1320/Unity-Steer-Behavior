using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class Seek : MonoBehaviour
{
    public Vector3 targetPos;
    public float speed = 10;
    public float slowRadius = 10;
    public float maxVel = 10;
    public float maxSteer = 3;
    
    private Vector3 vel;

    Vector3 Truncate(Vector3 vector, float maxSteer)
    {
        var mag = vector.magnitude;
        if (mag > maxSteer)
        {
            vector *= maxSteer / mag;
        }

        return vector;
    }

    void Update()
    {
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        targetPos = new Vector3(worldPosition.x, worldPosition.y, 0);
        
        var dir = targetPos - gameObject.transform.position;
        var ramp = Mathf.Min(dir.magnitude / slowRadius, 1.0f);

        var steering = dir * (speed * ramp) - vel;
        steering = Truncate(steering, maxSteer);

        vel = Truncate(vel + steering, maxVel);

        gameObject.transform.position += vel;
    }
}
