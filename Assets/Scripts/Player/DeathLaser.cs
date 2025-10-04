using System;
using System.Collections;
using UnityEngine;

public class DeathLaser : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;

    public void ThrowLaser(Vector3 objective, float duration)
    { 
        StartCoroutine(ThrowLaserCoroutine(objective, duration));
    }

    private void Update()
    {
        if (lineRenderer.enabled)
        {
            lineRenderer.SetPosition(0, transform.position);
        }
    }

    private IEnumerator ThrowLaserCoroutine(Vector3 objective, float duration)
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(1, objective);
        yield return new WaitForSeconds(duration);
        lineRenderer.enabled = false;
    }

}
