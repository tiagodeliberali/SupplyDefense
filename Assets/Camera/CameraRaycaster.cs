using Assets.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraRaycaster : MonoBehaviour
{
    public delegate void RaycasterLayerEvent(int layer);
    public event RaycasterLayerEvent OnLayerChanged;

    public delegate void RaycasterClickEvent(RaycastHit raycastHit, int layer);
    public event RaycasterClickEvent OnLayerClick;

    private float maxRaycastDepth = 100f;
    private Layer[] layerPriorities = { Layer.Enemy, Layer.Walkable };
    
    private int previousLayerHit;

    private void Update ()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            NotifyOnLayerChanged((int)Layer.UI);
            return;
        }

        RaycastHit? priorityHit = GetPriorityHit();

        if (!priorityHit.HasValue)
        {
            NotifyOnLayerChanged((int)Layer.RaycastEndStop);
            return;
        }

        int layer = priorityHit.Value.collider.gameObject.layer;
        NotifyOnLayerChanged(layer);

        if (Input.GetMouseButton(0))
        {
            if (OnLayerClick != null) OnLayerClick(priorityHit.Value, layer);
        }

    }

    private void NotifyOnLayerChanged(int newLayer)
    {
        if (previousLayerHit != newLayer)
        {
            previousLayerHit = newLayer;
            if (OnLayerChanged != null) OnLayerChanged(newLayer);
        }
    }

    private RaycastHit? GetPriorityHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] raycastHits = Physics.RaycastAll(ray, maxRaycastDepth);

        return FindHighPriorityHit(raycastHits);
    }

    private RaycastHit? FindHighPriorityHit(RaycastHit[] raycastHits)
    {
        Dictionary<int, RaycastHit> raycastHitLayers = new Dictionary<int, RaycastHit>();

        for (int i = 0; i < raycastHits.Length; i++)
            raycastHitLayers.Add(raycastHits[i].collider.gameObject.layer, raycastHits[i]);

        foreach (int layer in layerPriorities)
        {
            if (raycastHitLayers.ContainsKey(layer))
                return raycastHitLayers[layer];
        }

        return null;
    }
}
