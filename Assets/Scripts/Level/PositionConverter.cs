using UnityEngine;

public class PositionConverter : MonoBehaviour
{
    private Camera _mainCamera;


    public Vector3 GetWorldPosition(RectTransform uiElement, float _heightOfPlacing)
    {
        if (_mainCamera == null) _mainCamera = Camera.main;

        Vector3[] corners = new Vector3[4];
        uiElement.GetWorldCorners(corners);

        Vector2 screenPoint = new Vector2(
            (corners[0].x + corners[2].x) * 0.5f,
            (corners[0].y + corners[2].y) * 0.5f
        );

        Ray ray = _mainCamera.ScreenPointToRay(screenPoint);

        Plane plane = new Plane(Vector3.up, new Vector3(0, _heightOfPlacing, 0));
        float distance;

        if (plane.Raycast(ray, out distance))
        {
            return ray.GetPoint(distance);
        }

        return Vector3.zero;
    }

}
