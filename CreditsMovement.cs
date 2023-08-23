using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMovement : MonoBehaviour
{
    public float moveSpeed = 10.0f;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = new Vector3(
            transform.position.x,
            transform.position.y + 1.0f,
            transform.position.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            newPosition,
            moveSpeed * Time.deltaTime
        );
    }
}
