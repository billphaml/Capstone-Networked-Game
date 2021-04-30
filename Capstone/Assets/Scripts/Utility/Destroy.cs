using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField] private float destroyDelayTime = 1f;

    private float nextDestroyTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        nextDestroyTime = Time.time + destroyDelayTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (nextDestroyTime <= Time.time)
        {
            Destroy(gameObject);
        }
    }
}
