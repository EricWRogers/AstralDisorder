using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using OmnicatLabs.Timers;
using OmnicatLabs.Tween;

public class HitToPush : MonoBehaviour, ISaveable
{
    public float pushForce = 10f;
    public float lifetime = 2f;
    public float shrinkTime = .7f;
    public UnityEvent onHit = new UnityEvent();
    private List<GameObject> pieces = new List<GameObject>();
    private List<Vector3> startingPositions = new List<Vector3>();
    private List<Vector3> startingScales = new List<Vector3>();
    private List<Quaternion> startingRotations = new List<Quaternion>();

    private void Start()
    {
        pieces.AddRange(GetComponentsInChildren<Collider>().ToList().Where(col => col.isTrigger).Select(col => col.gameObject));
        startingPositions.AddRange(GetComponentsInChildren<Transform>().ToList().Select(trans => trans.localPosition));
        startingScales.AddRange(GetComponentsInChildren<Transform>().ToList().Select(trans => trans.localScale));
        startingRotations.AddRange(GetComponentsInChildren<Transform>().ToList().Select(trans => trans.localRotation));
        SaveManager.Instance.Track(this);
    }

    public void Push(Vector3 impactDirection)
    {
        onHit.Invoke();
        foreach(var piece in pieces)
        {
            var rb = piece.GetComponent<Rigidbody>();
            rb.GetComponent<Collider>().isTrigger = false;
            rb.useGravity = true;
            rb.AddForce(impactDirection * pushForce, ForceMode.Impulse);
        }
        GetComponent<BoxCollider>().enabled = false;
        TimerManager.Instance.CreateTimer(lifetime, Shrink);
    }

    private void Shrink()
    {
        foreach(var piece in GetComponentsInChildren<Transform>())
        {
            piece.TweenScale(Vector3.zero, shrinkTime, () => GetComponentsInChildren<Transform>().ToList().ForEach(piece => piece.gameObject.SetActive(false)));
        }
    }

    public void OnTrack()
    {
        
    }

    public void OnReset()
    {
        var allObjs = GetComponentsInChildren<Transform>(true).ToList().Select(trans => trans.gameObject).ToList();

        for (int i = 0; i < allObjs.Count; i++)
        {
            if (allObjs[i].TryGetComponent<Rigidbody>(out var rb) && pieces.Contains(allObjs[i]))
            {
                rb.useGravity = false;
                rb.velocity = Vector3.zero;
                rb.GetComponent<Collider>().isTrigger = true;
            }
            allObjs[i].transform.localPosition = startingPositions[i];
            allObjs[i].transform.localScale = startingScales[i];
            allObjs[i].transform.localRotation = startingRotations[i];
            allObjs[i].SetActive(true);
        }
        allObjs[0].GetComponent<BoxCollider>().enabled = true;
    }
}
