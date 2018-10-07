using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    Transform lookAt;
    Vector3 startoffset;
    Vector3 moveVector;
	// Use this for initialization
	void Start () {
        lookAt=GameObject.FindGameObjectWithTag("Player").transform;
        startoffset = transform.position - lookAt.position;
    }
	
	// Update is called once per frame
	void Update () {
        moveVector = lookAt.position + startoffset;
        //moveVector.x = 0;
		moveVector.y = Mathf.Clamp(moveVector.y,lookAt.position.y,lookAt.position.y+5.5f);
        transform.position = moveVector;
	}
}
