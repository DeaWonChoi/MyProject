using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RedTheSettlers.GameSystem
{
    public abstract class AbstractCamera
    {
        protected Camera camera;
        //protected GameObject cameraObject;
        protected CameraMoving cameraMoving;
        protected CameraZoomInOut cameraZoomInOut;
        protected CameraAngle cameraAngle;


        public abstract CameraMoving CameraMoving(Vector3 vector3, CameraStateType cameraState);
        public abstract CameraZoomInOut CameraZoomInOut(float value);
        public abstract CameraAngle CameraAngle(Vector3 target);

        public void TurnOn()
        {
            camera.enabled = true;
        }
        public void TurnOff()
        {
            camera.enabled = false;
        }
    } 
}