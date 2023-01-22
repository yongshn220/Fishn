using UnityEngine;

public class CameraProjection : MonoBehaviour
{
    public new Camera camera;

    public float transition;
    public float transitionSpeed = 2.0f;

    public float perspectiveFOV = 60f;
    public float orthographicSize = 10f;

    void Update()
    {
        if (camera.orthographic)
        {
            camera.orthographicSize = Mathf.Lerp(orthographicSize, perspectiveFOV, transition);
        }
        else
        {
            camera.fieldOfView = Mathf.Lerp(perspectiveFOV, orthographicSize, transition);
        }
        transition += Time.deltaTime * transitionSpeed;
    }
}



