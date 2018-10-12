using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textAnim : MonoBehaviour
{
    Vector2 uvOffset = Vector2.zero;
    public Vector2 uvAnimSpeed = new Vector2(0f, 0.1f);
    Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    private void LateUpdate()
    {
        uvOffset += (uvAnimSpeed * Time.deltaTime);
        renderer.material.SetTextureOffset("_MainTex", uvOffset);
    }

}
