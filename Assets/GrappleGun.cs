using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//start working on the joints
public class GrappleGun : MonoBehaviour
{
    public GameObject ropePrefab;
    public GameObject segmentPrefab;
    public float segmentDistance = .5f;
    public Transform launchPoint;
    public float force = 100f;
    public float segmentCutoff = .1f;
    public float maxDistance = 20f;

    private GameObject segmentInstance;
    private GameObject ropeInstance;
    private GameObject previousSegment;

    public void OnGrapple(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            ropeInstance = Instantiate(ropePrefab, launchPoint.position, ropePrefab.transform.rotation);

            ropeInstance.GetComponent<Rigidbody>().AddForce(new Vector3(0f, .5f, 1f) * force, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        if (ropeInstance != null)
        {
            
        }
    }

    private void Update()
    {
        if (ropeInstance != null)
        {
            if (previousSegment == null || (Vector3.Distance(launchPoint.position, previousSegment.transform.position) > segmentCutoff))
            {
                segmentInstance = Instantiate(segmentPrefab, launchPoint.transform.position, Quaternion.Euler(new Vector3(90f, 0f, 0f)), ropeInstance.transform);
                //if (previousSegment != null)
                //{
                //    //segmentInstance.transform.rotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
                //    //segmentInstance.transform.position = new Vector3(0f, previousSegment.transform.position.y + segmentDistance, 0f);
                //}
                //else
                //{
                //    segmentInstance.transform.position = Vector3.zero;
                //}
                segmentInstance.transform.LookAt(ropeInstance.transform);
                previousSegment = segmentInstance;
            }
            
            if (Vector3.Distance(ropeInstance.transform.position, launchPoint.transform.position) > maxDistance)
            {
                Destroy(ropeInstance);
            }
        }
    }
}
