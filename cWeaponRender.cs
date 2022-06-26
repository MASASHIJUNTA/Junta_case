using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cWeaponRender : MonoBehaviour
{
    public GameObject ImageObject;

    MeshRenderer meshRenderer;

    public float DeleteTime = 0;
    float CountTime = 0;

    public bool Delete = false;

    float SetAlpha = 0;

    Color32 AddColor;

    public cCurveRotation CurveRotation;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = ImageObject.GetComponent<MeshRenderer>();
        meshRenderer.sortingLayerName = "Weapon";
        meshRenderer.sortingOrder = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Delete)
        {
            if(CurveRotation != null)
            {
                if(CurveRotation.Stop == false)
                {
                    CurveRotation.Stop = true;
                }
            }

            if (CountTime < DeleteTime)
            {
                CountTime += Time.deltaTime;

                if (CountTime > DeleteTime)
                {
                    CountTime = DeleteTime;
                }

                SetAlpha = 255 - 255 * CountTime / DeleteTime;

                AddColor = meshRenderer.material.color;
                AddColor.a = (byte)SetAlpha;
                meshRenderer.material.color = AddColor;

            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
