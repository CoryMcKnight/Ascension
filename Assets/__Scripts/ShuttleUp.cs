using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuttleUp : MonoBehaviour {

    public GameObject shuttle;
    public float Speed = 6.0f;
    public GameObject JetsFlying;

	// Use this for initialization
	void Update () {
        JetsFlying.SetActive(true);
        shuttle.transform.Translate(Vector3.up * (Time.deltaTime * Speed));


    }


}
