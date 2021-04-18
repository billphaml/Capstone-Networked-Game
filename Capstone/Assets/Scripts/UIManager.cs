using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] public GameObject RoomName;
    [SerializeField] public GameObject ipBoxClient;
    [SerializeField] public GameObject NickName;

    [Header("Android UI")]
    [SerializeField] private GameObject androidUI = default;

    // Start is called before the first frame update
    void Start()
    {
#if !UNITY_ANDROID
        Destroy(androidUI);
#endif
    }

    // Update is called once per frame
    void Update()
    {

    }
}
