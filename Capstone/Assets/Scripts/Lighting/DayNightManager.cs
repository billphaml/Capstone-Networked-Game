/******************************************************************************
 * Script to control the global illumination and rotate between a day-night
 * cycle. Can be referenced by other scripts to determine if it is currently
 * day or night.
 * 
 * Authors: Bill, Hamza, Max, Ryan
 *****************************************************************************/

using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using MLAPI;
using MLAPI.NetworkVariable;

public class DayNightManager : NetworkBehaviour
{
    [SerializeField] private Light2D globalLight;

    [Header("Time")]
    [Range(0f, 1f)]
    [SerializeField] private float time;

    /// <summary>
    /// Variable to sync server's time with every client's time to have a 
    /// synced day/night cycle.
    /// </summary>
    private NetworkVariable<float> networkTime = new NetworkVariable<float>(0.4f);

    [SerializeField] private float fullDayLength;

    /// <summary>
    /// Time of day the game will be set to on start.
    /// </summary>
    [Range(0f, 1f)]
    [SerializeField] private float startTime;

    /// <summary>
    /// Amount of time that should pass every update.
    /// </summary>
    private float timeChangeRate;

    [Header("Color")]
    [Range(0f, 1f)]
    [SerializeField] private float hue = 0f;

    [Range(0f, 1f)]
    [SerializeField] private float sat = 0f;

    [Range(0f, 1f)]
    [SerializeField] private float bri = 1f;

    public AnimationCurve sunlightLevel;

    private void Start()
    {
        timeChangeRate = 1f / fullDayLength;
    }

    void Update()
    {
        if (IsServer || IsHost)
        {
            time += timeChangeRate * Time.deltaTime;

            if (time >= 1f) time = 0f;

            networkTime.Value = time;
        }

        if (IsClient)
        {
            time = networkTime.Value;
        }

        bri = sunlightLevel.Evaluate(time);

        globalLight.color = Color.HSVToRGB(hue, sat, bri);
    }

    /// <summary>
    /// Returns true if brightness is greater than 0.6f indicating daytime.
    /// False otherwise.
    /// </summary>
    /// <returns></returns>
    public bool IsDay()
    {
        return (bri >= 0.6f);
    }
}
