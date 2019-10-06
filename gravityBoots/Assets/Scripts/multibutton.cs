using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class multibutton : MonoBehaviour
{
    [SerializeField]
    Transform[] buttons;

    public bool triggerEvent = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (!buttons[i].GetComponent<WeightedButtonScript>().triggered)
            {
                triggerEvent = false;
                return;
            }
        }
        triggerEvent = true;
    }
}
