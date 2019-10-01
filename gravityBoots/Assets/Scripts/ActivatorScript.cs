using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ActivatorScript : MonoBehaviour
{
    [SerializeField]
    Transform[] objects;
    [SerializeField]
    Transform button;

    bool activated = true;

    // Update is called once per frame
    void Update()
    {
        if (button.GetComponent<WeightedButtonScript>().triggered && !activated)
        {
            StartCoroutine("Activate");
        }
        else if (!button.GetComponent<WeightedButtonScript>().triggered && activated)
        {
            StartCoroutine("DeActivate");
        }
    }

    IEnumerator Activate()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i] != null)
            {
                if (!objects[i].gameObject.activeSelf)
                {
                    objects[i].gameObject.SetActive(true);
                }

            }
            yield return new WaitForEndOfFrame();
        }
        activated = true;
    }

    IEnumerator DeActivate()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i] != null)
            {
                if (objects[i].gameObject.activeSelf)
                {
                    objects[i].gameObject.SetActive(false);
                }

            }
            yield return new WaitForEndOfFrame();
        }
        activated = false;
    }
}
