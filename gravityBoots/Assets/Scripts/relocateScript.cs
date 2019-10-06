using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class relocateScript : MonoBehaviour
{
    [SerializeField]
    private Transform endPos;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.GetComponent<multibutton>().triggerEvent && transform.position != endPos.position)
        {
            transform.position = endPos.position;

        }
        else if (!transform.GetComponent<multibutton>().triggerEvent && transform.position == endPos.position)
        {
            transform.position = startPos;
        }
    }
}
