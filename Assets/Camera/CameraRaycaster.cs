using Assets.Utils;
using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
    [SerializeField] float distanceToBackground = 100f;

    public delegate void RaycasterLayerEvent(Layer layer);
    public event RaycasterLayerEvent OnLayerChanged;

    private Layer[] layerPriorities = { Layer.Enemy, Layer.Walkable };
    private RaycastHit raycastHit;
    private Layer layerHit;
    private Layer previousLayerHit;
    private Camera viewCamera;

    public RaycastHit Hit
    {
        get { return raycastHit; }
    }

    public Layer LayerHit
    {
        get { return layerHit; }
    }

    void Start ()
    {
        viewCamera = Camera.main;
	}

    void Update ()
    {
        raycastHit.distance = distanceToBackground;
        layerHit = Layer.RaycastEndStop;

        foreach (Layer layer in layerPriorities)
        {
            var hit = RaycastForLayer(layer);
            if(hit.HasValue)
            {
                raycastHit = hit.Value;
                layerHit = layer;
                break;
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
        bool hasHit = Physics.Raycast(ray, out hit, distanceToBackground, layerMask);

        if (hasHit)
        {
            return hit;
        }

        return null;
    }
}
