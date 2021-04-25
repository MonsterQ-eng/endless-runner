using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraFollow : MonoBehaviour
{
    private bool isGameRun = true;
    private Func<Vector3> GetCameraFollowPositionFunc;

   public void Setup(Func<Vector3> GetCameraFollowPositionFunc)
    {

        this.GetCameraFollowPositionFunc = GetCameraFollowPositionFunc;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameRun)
        {
            Vector3 cameraFollowPosition = GetCameraFollowPositionFunc();
            cameraFollowPosition.z = transform.position.z;
            transform.position = cameraFollowPosition;
        } 
        
    }

 
    public void IsGameRun()
    {
        isGameRun = false;
    }


}
