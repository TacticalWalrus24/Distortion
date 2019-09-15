using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCameraScript : MonoBehaviour
{
    [SerializeField]
    Transform playerCam;
    [SerializeField]
    Transform portal;
    [SerializeField]
    Transform otherPortal;

    // Update is called once per frame
    void Update()
    {
        Vector3 playerOffset = playerCam.position - otherPortal.position;
        transform.position = portal.position + playerOffset;

        float angleDiff = Quaternion.Angle(portal.rotation, otherPortal.rotation) - 180;

        Quaternion portalRotDiff = Quaternion.AngleAxis(angleDiff, otherPortal.up);
        Vector3 newCamDir = portalRotDiff * playerCam.forward;
        transform.rotation = Quaternion.LookRotation(newCamDir, playerCam.up);
    }
}
