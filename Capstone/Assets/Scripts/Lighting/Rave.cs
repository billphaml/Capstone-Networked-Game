/******************************************************************************
 * Script to control the global illumination and alternate between RGB colors
 * creating a rave effect. Mainly for testing.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/

using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Rave : MonoBehaviour
{
    [SerializeField] private new Light2D light;

    [SerializeField] private float speed;

    private float hue;

    private float sat;

    private float bri;

    void Start()
    {
        if (light == null) light = gameObject.GetComponent<Light2D>();

        hue = Random.Range(0f, 1f);
        sat = 1;
        bri = 1;
        light.color = Color.HSVToRGB(hue, sat, bri);
    }

    private void Update()
    {
        Color.RGBToHSV(light.color, out hue, out sat, out bri);

        hue += speed / 10000;
        if (hue >= 1) hue = 0;
        light.color = Color.HSVToRGB(hue, sat, bri);
    }
}
