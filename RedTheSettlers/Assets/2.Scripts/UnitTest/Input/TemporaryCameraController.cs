using RedTheSettlers.GameSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryCameraController : MonoBehaviour
{
    private Camera camera;

    private void Start()
    {
        camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    public void CameraMove(Vector3 direction)
    {
        camera.transform.Translate(new Vector3(direction.x, 0, direction.y), Space.World);
    }

    public void CameraZoom(float value)
    {
        camera.fieldOfView += value;
    }
}
