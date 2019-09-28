using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightedButtonScript : MonoBehaviour
{
    [SerializeField]
    float weightLimit = 1;

    float impulseTrigger;
    bool activated = false;
    bool canChange = true;
    public bool triggered = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (activated && canChange)
        {
            StartCoroutine("Activate");
        }
        else if (canChange)
        {
            StartCoroutine("Deactivate");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("PickUp"))
        {
            activated = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("PickUp"))
        {
            activated = false;
        }
    }

    IEnumerator Activate()
    {
        canChange = false;
        while (transform.localPosition.y > -0.01)
        {
            transform.localPosition = (new Vector3(0, -1 * Time.deltaTime, 0));

            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(0.1f);
        Debug.Log("trigger");
        Debug.Log(impulseTrigger);

        canChange = true;
        triggered = true;
    }

    IEnumerator Deactivate()
    {
        activated = false;
        canChange = false;
        while (transform.localPosition.y < 0)
        {
            transform.localPosition = (new Vector3(0, 1 * Time.deltaTime, 0));

            yield return new WaitForFixedUpdate();
        }
        Debug.Log("untrigger");
        Debug.Log(impulseTrigger);
        canChange = true;
        triggered = false;
    }

}
