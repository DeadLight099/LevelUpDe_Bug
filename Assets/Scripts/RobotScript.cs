using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class RobotScript : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offest;
    [SerializeField] private float transitionSpeed;
    [SerializeField] private LineRenderer lineRenderer;

    private void LateUpdate()
    {
        FollowPlayer();
    }
    private void FollowPlayer()
    {
        transform.position = Vector3.Lerp(transform.position,
            target.position + offest, 
            transitionSpeed * Time.deltaTime);
    }
    public IEnumerator SendLaser(Transform endPosition)
    {
        lineRenderer.enabled = true;
        Vector3 dir = endPosition.position - transform.gameObject.transform.position;
        lineRenderer.SetPosition(1, new Vector3(0, 0, 0));
        lineRenderer.SetPosition(1, dir);
        yield return new WaitForSeconds(0.1f);
        lineRenderer.enabled = false;
    }
}
