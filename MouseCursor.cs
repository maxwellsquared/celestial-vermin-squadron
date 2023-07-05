using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    public float zOffset = 10f;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        // get mouse position
        Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector3 worldMousePos = Camera.main.ViewportToWorldPoint(
            new Vector3(mousePos.x, mousePos.y, zOffset)
        );
        transform.localPosition = transform.parent.InverseTransformPoint(worldMousePos);
    }
}
