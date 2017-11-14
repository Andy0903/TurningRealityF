using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class works together with a transparent diffuse material
public class UnRenderMe : MonoBehaviour
{
    Color startColor, currColor;
    Transform cam;

    void Start()
    {
        // Stores the initial color
        startColor = gameObject.GetComponent<Renderer>().material.color;
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    void Update()
    {
        // get scalar to check angle between object's local x-axis (right) and world's z-axis (forward)
        float dot = Vector3.Dot(cam.forward, transform.forward);

        if (dot > 0.3)
            currColor = new Color(startColor.r, startColor.g, startColor.b, Mathf.Lerp(currColor.a, 1 - dot, Time.deltaTime));
        else
            currColor = Color.Lerp(currColor, startColor, Time.deltaTime);

        GetComponent<Renderer>().material.color = currColor;

    }
}
