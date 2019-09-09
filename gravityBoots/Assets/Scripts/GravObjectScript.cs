using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravObjectScript : MonoBehaviour
{
    [SerializeField]
    Vector3 gravityDirection = new Vector3(0, -1, 0);

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(Physics.gravity.y * gravityDirection.x, Physics.gravity.y * gravityDirection.y, Physics.gravity.y * gravityDirection.z);
    }
}
