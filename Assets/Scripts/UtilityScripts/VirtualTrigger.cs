using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public struct VirtualTriggerContext
{
    public CallbackType type;
    public Collider collider;

    public VirtualTriggerContext(CallbackType _type, Collider _collider)
    {
        type = _type;
        collider = _collider;
    }
}

public enum CallbackType
{
    ENTER,
    EXIT,
    STAY
}

[System.Serializable]
public class VirtualTriggerEvent : UnityEvent<VirtualTriggerContext> { }

public class VirtualTrigger : MonoBehaviour
{
    public VirtualTriggerEvent triggerCallback = new VirtualTriggerEvent();
    public List<string> tagFilter = new List<string>();
    public LayerMask layerFilter;

    private void OnTriggerEnter(Collider other)
    {
        if (tagFilter.Contains(other.tag) || layerFilter == (layerFilter | (1 << other.gameObject.layer)))
            triggerCallback.Invoke(new VirtualTriggerContext(CallbackType.ENTER, other));
    }

    private void OnTriggerExit(Collider other)
    {
        if (tagFilter.Contains(other.tag) || layerFilter == (layerFilter | (1 << other.gameObject.layer)))
            triggerCallback.Invoke(new VirtualTriggerContext(CallbackType.EXIT, other));
    }

    private void OnTriggerStay(Collider other)
    {
        if (tagFilter.Contains(other.tag) || layerFilter == (layerFilter | (1 << other.gameObject.layer)))
            triggerCallback.Invoke(new VirtualTriggerContext(CallbackType.STAY, other));
    }
}
