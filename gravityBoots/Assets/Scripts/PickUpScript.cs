using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    [SerializeField]
    Transform cam;
    [SerializeField]
    Rigidbody hands;
    [SerializeField]
    float range = 5;
    [SerializeField]
    Transform otherHandsA;
    [SerializeField]
    Transform otherHandsB;

    bool holding = false;
    Transform objectHeld;
    Transform portalObject;
    // Update is called once per frame
    void Update()
    {
        Ray ray;
        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.E)){
            ray = new Ray(cam.position, cam.forward);

            if (Physics.Raycast(ray, out hit, range) && hit.transform.CompareTag("PickUp") && !holding)
            { // interactable object?
                objectHeld = hit.transform;
                objectHeld.position = hands.position;
                //objectHeld.GetComponent<Rigidbody>().useGravity = false;
                objectHeld.gameObject.AddComponent<FixedJoint>();
                objectHeld.gameObject.GetComponent<FixedJoint>().connectedBody = hands.GetComponent<Rigidbody>();
                holding = true;
                portalObject = Instantiate(objectHeld);
                portalObject.gameObject.AddComponent<FixedJoint>();
            }
            else if (holding)
            {
                //objectHeld.GetComponent<Rigidbody>().useGravity = true;
                Destroy(objectHeld.gameObject.GetComponent<FixedJoint>());
                holding = false;

                Destroy(portalObject.gameObject);
            }
        }
        if (holding)
        {
            portalObject.rotation = objectHeld.rotation;
            if (cam.position.magnitude - otherHandsA.position.magnitude < cam.position.magnitude - otherHandsB.position.magnitude)
            {
                if (portalObject.gameObject.GetComponent<FixedJoint>().connectedBody == otherHandsB.GetComponent<Rigidbody>())
                {
                    portalObject.gameObject.GetComponent<FixedJoint>().connectedBody = null;
                }
                portalObject.position = otherHandsA.position;

                portalObject.gameObject.GetComponent<FixedJoint>().connectedBody = otherHandsA.GetComponent<Rigidbody>();
            }
            else
            {
                if (portalObject.gameObject.GetComponent<FixedJoint>().connectedBody == otherHandsA.GetComponent<Rigidbody>())
                {
                    portalObject.gameObject.GetComponent<FixedJoint>().connectedBody = null;
                }
                portalObject.position = otherHandsB.position;
                portalObject.gameObject.GetComponent<FixedJoint>().connectedBody = otherHandsB.GetComponent<Rigidbody>();
            }
        }
    }
}
