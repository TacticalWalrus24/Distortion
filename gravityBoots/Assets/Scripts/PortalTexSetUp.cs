using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTexSetUp : MonoBehaviour
{
    [Header("Link 1")]
    [SerializeField]
    Camera camA;
    [SerializeField]
    Material camA_Mat;
    [SerializeField]
    Camera camB;
    [SerializeField]
    Material camB_Mat;

    [Header("Link 2")]
    [SerializeField]
    Camera camC;
    [SerializeField]
    Material camC_Mat;
    [SerializeField]
    Camera camD;
    [SerializeField]
    Material camD_Mat;
    // Start is called before the first frame update
    void Start()
    {
        if (camA != null)
        {
            if (camA.targetTexture != null)
            {
                camA.targetTexture.Release();
            }

            camA.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
            camA_Mat.mainTexture = camA.targetTexture;
        }

        if (camB != null)
        {
            if (camB.targetTexture != null)
            {
                camB.targetTexture.Release();
            }

            camB.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
            camB_Mat.mainTexture = camB.targetTexture;
        }

        if (camC != null)
        {
            if (camC.targetTexture != null)
            {
                camC.targetTexture.Release();
            }

            camC.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
            camC_Mat.mainTexture = camC.targetTexture;
        }

        if (camD != null)
        {
            if (camD.targetTexture != null)
            {
                camD.targetTexture.Release();
            }

            camD.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
            camD_Mat.mainTexture = camD.targetTexture;
        }
    }
}
