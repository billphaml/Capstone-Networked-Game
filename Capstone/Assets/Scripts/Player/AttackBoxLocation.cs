/******************************************************************************
 * 
 *****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;


public class AttackBoxLocation : NetworkBehaviour
{
    public Camera mainCamera;
    private float m_depth;
    private float test;
    private Vector3 mousePosition;
    private Vector3 centerPosition;
    [SerializeField] private float radius;
    [SerializeField] private Rigidbody2D player;
    void Start()
    {
        m_depth = transform.position.z - mainCamera.transform.position.z;
    }

    void Update()
    {
        if (IsLocalPlayer) 
        {
            //moves the attackbox torwards the location of the mouse based on the camera veiw
            transform.position = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, m_depth));
        

            //All the code below keeps the attack box within a radius
            centerPosition = player.position;

            test = Vector3.Distance(transform.position, centerPosition);
            if (test > radius)
            {
                Vector3 fromOriginToObject = transform.position - centerPosition;
                fromOriginToObject *= radius / test;
                transform.position = centerPosition + fromOriginToObject;
            }
        }

        //mousePosition = Input.mousePosition;
        // mousePosition Vector3 = Camera.ScreenToWorldPoint(Input.mousePosition);
        //transform.position = new Vector3(mousePosition.x, mousePosition.y, m_depth);

        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //RaycastHit hit;

        //if (Input.GetMouseButtonDown(0)) ;

    }
}
