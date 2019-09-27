using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenScript : MonoBehaviour
{
    [SerializeField]
    float openDist = 2;
    [SerializeField]
    float speed = 10;

    float movedDist = 0;
    bool open = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor()
    {
        if (!open)
        {

        }
    }

    public void CloseDoor()
    {

    }

    IEnumerator Open()
    {
        while (openDist > movedDist)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
            movedDist += speed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
