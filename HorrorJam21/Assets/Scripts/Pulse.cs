using System.Collections;
using UnityEngine;

public class Pulse : MonoBehaviour
{
    [SerializeField] private SphereCollider sphereCollider;
    [SerializeField] private float targetRadius = 5f;
    [SerializeField] private float growDuration = 1f;

    private void Awake()
	{
        StartCoroutine(GrowRadius());
    }

    IEnumerator GrowRadius()
    {
        float elapsed = 0f;
        float startRadius = 0f;
        sphereCollider.radius = startRadius;

        while (elapsed < growDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / growDuration);
            sphereCollider.radius = Mathf.Lerp(startRadius, targetRadius, t);
            yield return null;
        }

        sphereCollider.radius = targetRadius; // Ensure it's exact at the end
    }


    void OnTriggerEnter(Collider other)
    {
        var scannable = other.GetComponent<ScannableObject>();

        if (scannable != null)
        {
            //Debug.Log("found object to scan!");
            scannable.Pulse();
        }
    }

}
