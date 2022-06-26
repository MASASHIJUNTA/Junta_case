using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cDeadLine : MonoBehaviour
{
    [SerializeField] float Up = 17;
    [SerializeField] float Down = -2;
    [SerializeField] float Right = 20;
    [SerializeField] float Left = -20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > Up || transform.position.y < Down || transform.position.x > Right || transform.position.x < Left)
        {
            Destroy(this.gameObject);
        }
    }
}
