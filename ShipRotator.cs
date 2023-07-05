using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRotator : MonoBehaviour
{
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = transform.parent.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
    }
}
