using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = Vector3.zero;
    public Vector2 limits = new Vector2(5, 3);

    [Space]
    [Header("Smooth Damp Time")]
    [Range(0, 1)]
    public float smoothTime;
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
        {
            // -----NOTE-----
            // All functionality has been commented out for now!
            // --------------

            // transform.localPosition = offset;
        }

        // FollowTarget(target);
    }

    // void LateUpdate()
    // {
    //     Vector3 localPos = transform.localPosition;

    //     transform.localPosition = new Vector3(
    //         Mathf.Clamp(localPos.x, -limits.x, limits.x),
    //         Mathf.Clamp(localPos.y, -limits.y, limits.y),
    //         localPos.z
    //     );
    // }

    public void FollowTarget(Transform t)
    {
        Vector3 localPos = transform.localPosition;
        Vector3 targetLocalPos = t.transform.localPosition;
        transform.localPosition = Vector3.SmoothDamp(
            localPos,
            new Vector3(targetLocalPos.x + offset.x, targetLocalPos.y + offset.y, localPos.z),
            ref velocity,
            smoothTime
        );
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.green;
    //     Gizmos.DrawLine(
    //         new Vector3(-limits.x, -limits.y, transform.position.z),
    //         new Vector3(limits.x, -limits.y, transform.position.z)
    //     );
    //     Gizmos.DrawLine(
    //         new Vector3(-limits.x, limits.y, transform.position.z),
    //         new Vector3(limits.x, limits.y, transform.position.z)
    //     );
    //     Gizmos.DrawLine(
    //         new Vector3(-limits.x, -limits.y, transform.position.z),
    //         new Vector3(-limits.x, limits.y, transform.position.z)
    //     );
    //     Gizmos.DrawLine(
    //         new Vector3(limits.x, -limits.y, transform.position.z),
    //         new Vector3(limits.x, limits.y, transform.position.z)
    //     );
    // }
}
