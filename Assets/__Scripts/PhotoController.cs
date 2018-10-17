using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoController : MonoBehaviour {

    public Transform leader;
    public float followSharpness;

    Vector3 _followOffset;

    // For rotation
    public float degreesPerSecond; // set to 30 in inspector

    // For floating up-and-down
    public float amplitude; // set to 0.1 in inspector
    public float frequency; // set to 1 in inspector

    // Related to throwing the photo
    public Rigidbody2D rb2d;
    public float power = 100;
    Vector3 gravity = Vector3.down * 0.01f;
    bool thrown = false;

    void Start() {
        // Cache the initial offset at time of load/spawn:
        _followOffset = transform.position - leader.position;

        // Color the material red
        //gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    void LateUpdate() {
        // Apply that offset to get a target position.
        Vector3 targetPosition = leader.position + _followOffset;

        // Keep our y position unchanged.
        targetPosition.y = transform.position.y;

        // Float up-and-down
        transform.position = new Vector3(transform.position.x,
                                         transform.position.y + Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude,
                                         transform.position.z);

        // Smooth follow.    
        transform.position += (targetPosition - transform.position) * followSharpness;

        // Spin object around Y-Axis
        transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);
    }

    void FixedUpdate() {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("Throw photo");

            if(thrown) {
                rb2d.AddForce(gravity);
            }
            else {
                rb2d.AddForce(leader.transform.forward * power);
                thrown = true;
            }
        }
    }
}
