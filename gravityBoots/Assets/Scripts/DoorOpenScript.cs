using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenScript : MonoBehaviour
{
    [SerializeField]
    float openDist = 2;
    [SerializeField]
    float speed = 10;
    [SerializeField]
    Transform button;

    float movedDist = 0;

    [SerializeField]
    bool open = false;

    // Update is called once per frame
    void Update()
    {
        if (button.GetComponent<WeightedButtonScript>().triggered && !open)
        {
            StartCoroutine("Open");
        }
        else if (!button.GetComponent<WeightedButtonScript>().triggered && open)
        {
            StartCoroutine("Close");
        }
    }

    IEnumerator Open()
    {
        while (openDist > movedDist)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
            movedDist += speed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        open = true;
    }

    IEnumerator Close()
    {
        while (movedDist > 0)
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
            movedDist -= speed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        open = false;
    }
}
