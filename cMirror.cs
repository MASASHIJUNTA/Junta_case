using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cMirror : MonoBehaviour
{
    [SerializeField]  ParticleSystem PlayEffect;
    [SerializeField]  GameObject AtkArea;

    [SerializeField] Vector3 Speed;
    bool move = true;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(move)
        transform.Translate(Speed.x * Time.deltaTime, -Speed.y * Time.deltaTime, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Floor")
        {
            this.GetComponent<BoxCollider>().enabled = false;
            move = false;

            PlayEffect.Play();

            this.GetComponent<cWeaponRender>().Delete = true;
            AtkArea.SetActive(true);

            this.GetComponent<cAudioCall>().enabled = true;
        }
    }
}
