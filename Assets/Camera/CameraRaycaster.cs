using Assets.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraRaycaster : MonoBehaviour
{
    public delegate void RaycasterLayerEvent(Layer layer);
    public event RaycasterLayerEvent OnLayerChanged;

    public delegate void RaycasterClickEvent(RaycastHit raycastHit, Layer layer);
    public event RaycasterClickEvent OnLayerClick;

    private float maxRaycastDepth = 100f;
    private Layer[] layerPriorities = { Layer.Enemy, Layer.Walkable };
    private RaycastHit raycastHit;
    private Layer layerHit;
    private Layer previousLayerHit;
    private Camera viewCamera;

    void Start ()
    {
        viewCamera = Camera.main;
	}

    void Update ()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            layerHit = Layer.UI;
        }
        else
        {
            raycastHit.distance = maxRaycastDepth;
            layerHit = Layer.RaycastEndStop;

            foreach (Layer layer in layerPriorities)
            {
                var hit = RaycastForLayer(layer);
                if (hit.HasValue)
                {
                    raycastHit = hit.Value;
                    layerHit = layer;
                    break;
                }
            }
        }

        if (previousLayerHit != layerHit)
        {
            previousLayerHit = layerHit;
            if (OnLayerChanged != null) OnLayerChanged(layerHit);
        }
	}

    RaycastHit? RaycastForLayer(Layer layer)
    {
        int layerMask = 1 << (int)layer;
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit, maxRaycastDepth, layerMask);

        if (hasHit)
        {
            return hit;
        }

        return null;
    }
}
