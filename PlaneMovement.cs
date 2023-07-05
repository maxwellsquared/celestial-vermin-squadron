using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    public Transform dollyCart;     
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // move to dollycart position
        transform.position = dollyCart.position;

        // match dollycart's side-to-side rotation
        Vector3 newRotation = transform.eulerAngles;
        newRotation.y = dollyCart.transform.eulerAngles.y;
        transform.eulerAngles = newRotation;

    }
}
