using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDelete : MonoBehaviour
{
    [SerializeField]
    Transform[] objects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine("DeActivate");
        }
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
        gameObject.SetActive(false);
    }
}
