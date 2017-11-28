using Assets.Utils;
using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
    public Layer[] layerPriorities = { Layer.Enemy, Layer.Walkable };

    [SerializeField] float distanceToBackground = 100f;
    Camera viewCamera;

    private RaycastHit raycastHit;
    public RaycastHit Hit
    {
        get { return raycastHit; }
    }

    private Layer layerHit;
    public Layer LayerHit
    {
        get { return layerHit;  }
    }

    void Start ()
    {
        viewCamera = Camera.main;
	}

    void Update ()
    {
        foreach (Layer layer in layerPriorities)
        {
            var hit = RaycastForLayer(layer);
            if(hit.HasValue)
            {
                raycastHit = hit.Value;
                layerHit = layer;
                return;
            }
        }

        raycastHit.distance = distanceToBackground;
        layerHit = Layer.RaycastEndStop;

        
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
