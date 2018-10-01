using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RedTheSettlers.GameSystem
{
    public abstract class CameraZoomInOut
    {
        public Camera camera;
        //public float zoomSpeed = 5.0f;
        //public float cameraFOV;
        
        public abstract void ZoomInOut(float value);
    }

    public class DragZoom : CameraZoomInOut
    {
        public DragZoom(GameObject cameraObject)
        {
            camera = cameraObject.GetComponentInChildren<Camera>();
        }
        public override void ZoomInOut(float value)
        {
            camera.fieldOfView += value;
            //if (Input.touchCount == 2)
            //{
            //    Debug.Log("드레그줌인아웃 if");
            //    Touch touchZero = Input.GetTouch(0);
            //    Touch touchOne = Input.GetTouch(1);
            //    Vector2 touchZeroPos = touchZero.position - touchZero.deltaPosition;
            //    Vector2 touchOnePos = touchOne.position - touchOne.deltaPosition;
            //    float prevTouchDeltaMagitude = (touchZeroPos - touchOnePos).magnitude;
            //    float touchDeltaMagitud = (touchZero.position - touchOne.position).magnitude;
            //    float deltaMagnitudediff = prevTouchDeltaMagitude - touchDeltaMagitud;
            //    camera.fieldOfView += deltaMagnitudediff * zoomSpeed;
            //    camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 40f, 160f);
            //}
        }

        
    }
    public class ManualZoom : CameraZoomInOut
    {
        bool isZoom;
        float zoomIn = 30f;

        public ManualZoom(GameObject cameraObject)
        {
            camera = cameraObject.GetComponent<Camera>();
        }
        public override void ZoomInOut(float value)
        {
            Debug.Log(isZoom);
            if (!isZoom)
            {
                Debug.Log("줌인");
                isZoom = true;
            }
            else
            {
                Debug.Log("줌아웃");
                isZoom = false;
            }

        }
        private void ZoomIn(float zoomIn)
        {

        }
    }


}