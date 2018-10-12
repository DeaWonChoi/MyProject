using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlTwinkle : MonoBehaviour
{
    public bool switched;
    public float frequency = 0.2f;
    public Texture2D origAlbedo;
    public Texture2D newalbedo;
    public Renderer renderer;
    
    void Start ()
    {
        renderer = GetComponent<Renderer>();
        InvokeRepeating("Switch", frequency, frequency);
	}
	
	void Update ()
    {
        if (switched)
        {
            renderer.material.SetTexture("_MainTex", newalbedo);
        }
        else
        {
            renderer.material.SetTexture("_MainTex", origAlbedo);
        }
	}
    
    void Switch()
    {
        switched = !switched;
    }
}
