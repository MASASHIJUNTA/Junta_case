using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cStringRender : MonoBehaviour
{
    MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = this.GetComponent<MeshRenderer>();
        meshRenderer.sortingLayerName = "NormalUI";
        meshRenderer.sortingOrder = -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
