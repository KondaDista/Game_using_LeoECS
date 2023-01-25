using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TragectoryRender : MonoBehaviour
{
    private LineRenderer lineRendererComponents;
    public LayerMask ColisionMask;

    void Start()
    {
        lineRendererComponents = GetComponent<LineRenderer>();
    }

    public void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 30f, ColisionMask))
        {
            if (hit.collider)
            {
                lineRendererComponents.SetPosition(1, new Vector3(0, 0, hit.distance));
            }
        }
        else
        {
            lineRendererComponents.SetPosition(1, new Vector3(0, 0, 30));
        }
    }
}
