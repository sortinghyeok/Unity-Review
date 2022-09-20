using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterRotator : MonoBehaviour
{
    private enum RotateState
    {
        Idle, Vertical, Horizontal, Ready
            //idle -> hori -> verti -> ready -> idle
    }
    private RotateState state = RotateState.Idle;
    private float verticalRotateSpeed = 360f;
    private float horizontalRotateSpeed = 360f;

    private void Update()
    {
        if(state == RotateState.Idle)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                state = RotateState.Horizontal;
                Debug.Log("State Horizontal");
            }
        }
        else if(state == RotateState.Horizontal)
        {
            if(Input.GetButton("Fire1"))
            {
                transform.Rotate(new Vector3(0, horizontalRotateSpeed*Time.deltaTime, 0));
            }
            else if(Input.GetButtonUp("Fire1"))
            {
                state = RotateState.Vertical;
                Debug.Log("State Vertical");
            }
        }
        else if(state == RotateState.Vertical)
        {
            if(Input.GetButton("Fire1"))
            {
                transform.Rotate(new Vector3(-verticalRotateSpeed * Time.deltaTime, 0, 0));
            }
            else if(Input.GetButtonUp("Fire1"))
            {
                state = RotateState.Ready;
                Debug.Log("Ready");
            }
        }
    }
}
