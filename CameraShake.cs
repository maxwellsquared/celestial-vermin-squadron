using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
   // what the heck is a coroutine
   public IEnumerator ShakeBack (float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float z = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(originalPos.x, originalPos.y, z);

            elapsed += Time.deltaTime;

            // before continuing to the next iteration of the while loop, wait for the next frame
            yield return null;
        }

        transform.localPosition = originalPos;
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            // before continuing to the next iteration of the while loop, wait for the next frame
            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
