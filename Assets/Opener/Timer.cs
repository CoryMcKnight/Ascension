using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameObject Lights1;
    public GameObject Lights2;
    public GameObject Lights3;
    public GameObject Jets;
    public GameObject Title;
    public GameObject Shuttle;



    void Start()
    {

        StartCoroutine(LateCall());
    }

    IEnumerator LateCall()
    {
        yield return new WaitForSeconds(3);
        Lights1.SetActive(true);
        yield return new WaitForSeconds(3);
        Lights2.SetActive(true);
        yield return new WaitForSeconds(2);
        Lights3.SetActive(true);
        yield return new WaitForSeconds(5);
        Jets.SetActive(true);
        Title.SetActive(true);
        Shuttle.GetComponent<ShuttleUp>().enabled = true;
        yield return new WaitForSeconds(3);
        Jets.SetActive(false);

    }
}
