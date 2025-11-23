using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using System;

public class EventManager : MonoBehaviour
{

    public enum EventType { Plate, Gift, Phone } //오브젝트 종류
    public EventType eventType;
    public static event Action<EventType> OnEvent;
    private XRGrabInteractable grab;

    private void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        grab.selectEntered.AddListener(OnGrab);
    }

    private void OnDisable()
    {
        grab.selectEntered.RemoveListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args) //오브젝트 종류 전달
    {
        OnEvent?.Invoke(eventType);
    }



}

