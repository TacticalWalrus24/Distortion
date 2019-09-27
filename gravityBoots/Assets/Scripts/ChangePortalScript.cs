using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePortalScript : MonoBehaviour
{
    [SerializeField]
    Transform portalA;
    [SerializeField]
    Transform portalB;

    private Transform player;
    private bool isOverlapping = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isOverlapping)
        {
            Vector3 triggerToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, -triggerToPlayer);
            if (dotProduct < 0)
            {
                portalA.gameObject.active = false;
                portalA.Rotate(0, 180, 0);
                portalB.Rotate(0, 180, 0);

                isOverlapping = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
            isOverlapping = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;
            isOverlapping = false;
        }
    }
}
