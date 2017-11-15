using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class works together with a transparent diffuse material
public class UnRenderMe : MonoBehaviour
{
    Color startColor, currColor;
    Transform cam;

    [SerializeField]
    float PerspectiveRange = 0.3f;
    [SerializeField]
    float Transparancy = 1;

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

        if (dot > PerspectiveRange)
            currColor = new Color(startColor.r, startColor.g, startColor.b, Mathf.Lerp(currColor.a, Transparancy - dot, Time.deltaTime));
        else
            currColor = Color.Lerp(currColor, startColor, Time.deltaTime);

        GetComponent<Renderer>().material.color = currColor;

    }
}
