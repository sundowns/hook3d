using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Left"))
        {
            Debug.Log("left");
        }
        else if (Input.GetButtonDown("Right"))
        {
            Debug.Log("right");
        }

        if (Input.GetButtonDown("Up"))
        {
            Debug.Log("up");
        }
        else if (Input.GetButtonDown("Down"))
        {
            Debug.Log("down");
        }

    }
}
