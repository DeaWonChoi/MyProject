using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedTheSettlers.GameSystem
{
    class BoardCamera : AbstractCamera
    {
        //GameObject cameraObject;
        public BoardCamera(GameObject cameraObject)
        {
            //this.cameraObject = cameraObject;
            camera = cameraObject.GetComponentInChildren<Camera>();
            cameraMoving = new DragMoving(cameraObject);
            cameraZoomInOut = new DragZoom(cameraObject);
            cameraAngle = new LookAtManual(cameraObject);
        }

        public override CameraAngle CameraAngle(Vector3 target)
        {
            //return new LookAtManual(cameraObject);
            cameraAngle.Looking(target);
            return cameraAngle;
        }

        public override CameraMoving CameraMoving(Vector3 vector3, CameraStateType cameraState)
        {
            cameraMoving.Moving(vector3, cameraState);
            return cameraMoving;
        }

        public override CameraZoomInOut CameraZoomInOut(float value)
        {
            cameraZoomInOut.ZoomInOut(value);
            return cameraZoomInOut;
        }
    } 
}