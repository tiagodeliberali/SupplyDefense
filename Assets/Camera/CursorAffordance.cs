using Assets.Utils;
using UnityEngine;

[RequireComponent(typeof(CameraRaycaster))]
public class CursorAffordance : MonoBehaviour {
    [SerializeField] Texture2D walkCursor;
    [SerializeField] Texture2D targetCursor;
    [SerializeField] Texture2D unknownCursor;
    [SerializeField] Vector2 cursorHotspot = new Vector2(4, 4);

    CameraRaycaster cameraRaycaster;

	void Start () {
        cameraRaycaster = GetComponent<CameraRaycaster>();
        cameraRaycaster.OnLayerChanged += UpdateCursor;
    }

    private void UpdateCursor(Layer layer)
    {
        switch (layer)
        {
            case Layer.Walkable:
                Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
                break;
            case Layer.Enemy:
                Cursor.SetCursor(targetCursor, cursorHotspot, CursorMode.Auto);
                break;
            case Layer.RaycastEndStop:
                Cursor.SetCursor(unknownCursor, cursorHotspot, CursorMode.Auto);
                break;
            default:
                break;
        }
    }
}
