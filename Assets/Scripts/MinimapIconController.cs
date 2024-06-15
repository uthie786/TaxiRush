using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapIconController : MonoBehaviour
{
    private MeshRenderer mrParent;

    private MeshRenderer mr;
    // Start is called before the first frame update
    void Start()
    {
        mrParent = transform.parent.GetComponent<MeshRenderer>();
        mr = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mr.enabled == false)
        {
            mr.enabled = false;
        }
        else if (mrParent.enabled == true)
        {
            mr.enabled = true;
        }
    }
}
