using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RedTheSettlers.GameSystem
{
    public class GameCamera : MonoBehaviour
    {

        AbstractCamera abstractCamera;
        string cameraTagName;
        private void Awake()
        {
            cameraTagName = gameObject.tag;
            if (cameraTagName == "BoardCamera")
            {
                abstractCamera = new BoardCamera(gameObject);
            }
            else if(cameraTagName == "BattleCamera")
            {
                abstractCamera = new BattleCamera(gameObject);
            }
        }
        


        public void TrunOnCamera()
        {
            abstractCamera.TurnOn();
        }
        public void TrunOffCamera()
        {
            abstractCamera.TurnOff();
        }
        public void MovingCamera(Vector3 vector3, CameraStateType cameraState)
        {
            abstractCamera.CameraMoving(vector3, cameraState);
        }
        public void ZoomInOutCamera(float value)
        {
            abstractCamera.CameraZoomInOut(value);
        }
        public void Looking(Vector3 target)
        {
            abstractCamera.CameraAngle(target);
        }



    } 
}