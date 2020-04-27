using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            // transform.LookAt(target.transform.position);
            // TODO: do something cleverer than this :p
        }
        else
        {
            target = GameObject.Find("Player");
        }
    }
}
