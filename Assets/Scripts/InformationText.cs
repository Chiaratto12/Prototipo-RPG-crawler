using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationText : MonoBehaviour
{
    public Vector3 offset;
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 1.5f;
        offset = new Vector3(2, 0, 0);

        Destroy(gameObject, timer);
        transform.position += offset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
