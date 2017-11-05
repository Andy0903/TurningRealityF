using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowObject : MonoBehaviour
{
    [SerializeField]
    Color glowColor;
    [SerializeField]
    float lerpFactor = 10f;

    private List<Material> materials = new List<Material>();
    private Color currentColor;
    private Color targetColor;

    GameObject mainCamera;
    
    private void Start()
    {
        mainCamera = Camera.main.gameObject;
    }

    private void Awake()
    {
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            materials.AddRange(renderer.materials);
        }
    }

    private void OnDisable()
    {
        currentColor = Color.black;
        targetColor = Color.black;
    }

    private void Update()
    {
        currentColor = Color.Lerp(currentColor, targetColor, Time.deltaTime * lerpFactor);

        for (int i = 0; i < materials.Count; i++)
        {
            materials[i].SetColor("_GlowColor", currentColor);
        }
    }

    private void FixedUpdate()
    {
        if (mainCamera.GetComponent<Camera>().enabled == false) return;

        RaycastHit hit;
        Vector3 direction = (transform.position - Camera.main.transform.position).normalized;

        if (Physics.Linecast(Camera.main.transform.position, GetComponentInChildren<Renderer>().bounds.center, out hit))
        {
            if (hit.transform.tag != "Player")
            {
                //Debug.Log("Player is occluded by " + hit.transform.name);
                targetColor = glowColor;
            }
            else
            {
                targetColor = Color.black;
            }
        }
    }
}
