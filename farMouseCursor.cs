using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class farMouseCursor : MonoBehaviour
{
    public Transform target;
    public Transform player;
    public GameObject midCrosshair;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position;
        transform.rotation = target.rotation;
        midCrosshair.transform.position = Vector3.Lerp(
            player.transform.position,
            transform.position,
            0.5f
        );
        midCrosshair.transform.rotation = transform.rotation;
    }
}
