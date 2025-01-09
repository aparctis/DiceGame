using UnityEngine;

public class CameraFitter : MonoBehaviour
{

    private float standartX = 1080.0f;
    private float standartY = 2400.0f;
    [SerializeField, Range(0, 1.0f)] float fitToWidth = 1.0f;

    private void Start()
    {
        FitOrtographicCamera();
    }
    private void FitOrtographicCamera()
    {
        Camera camera = Camera.main;
        float standartProportion = standartX / standartY;

        float size = camera.orthographicSize;
        //Debug.Log("standartFOV " + size);

        float newX = Screen.width;
        float newY = Screen.height;
        float newProportion = newX / newY;
        float multiple = newProportion / standartProportion;
        //Debug.Log("multiple " + multiple);


        float newSize = size / multiple;
        //Debug.Log("newFOV " + newSize);


        camera.orthographicSize = Mathf.Lerp(size, newSize, fitToWidth);
    }
}