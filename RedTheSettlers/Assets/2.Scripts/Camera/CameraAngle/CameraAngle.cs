using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RedTheSettlers.GameSystem
{
    public abstract class CameraAngle
    {
        public GameObject cameraObject;

        public abstract void Looking(Vector3 target);
    }

    public class LookAtManual : CameraAngle
    {
        private bool isWark = false;
        private Transform originalTransform;
        private Transform tileCenter;
        private Vector3 cameraOffset;

        private float smooth = 0.2f;
        private float hDist = 2f;
        private float vDist = 2f;

        public LookAtManual(GameObject gameObject)
        {
            cameraObject = gameObject;
        }

        private void Initialize()
        {
            originalTransform = cameraObject.transform;
            if (TileManager.Instance.BoardTileGrid[GlobalVariables.BoardTileGridSize / 2, GlobalVariables.BoardTileGridSize / 2].transform != null)
            {
                tileCenter = TileManager.Instance.BoardTileGrid[GlobalVariables.BoardTileGridSize / 2, GlobalVariables.BoardTileGridSize / 2].transform;
                cameraOffset = cameraObject.transform.position - tileCenter.position;
            }
            else
            {
                cameraOffset = new Vector3(0.3f, vDist, hDist);
            }
        }

        public override void Looking(Vector3 target)
        {
            if (originalTransform == null)
            {
                Initialize();
            }
            if (isWark == false && target != new Vector3())
            {
                Vector3 newPos = target + cameraOffset;
                cameraObject.transform.position = Vector3.Slerp(cameraObject.transform.position, newPos, smooth);
                isWark = true;
            }
            else
            {
                cameraObject.transform.position = Vector3.Slerp(cameraObject.transform.position, originalTransform.position, smooth);
                isWark = false;
            }
        }
    }

    public class LookAtTarget : CameraAngle
    {
        public LookAtTarget(GameObject gameObject)
        {
            cameraObject = gameObject;
        }

        public override void Looking(Vector3 target)
        {
            if (target == null)
            {
                return;
            }
            if (target != null)
            {
                cameraObject.transform.LookAt(target);
            }
        }
    }
}