using UnityEngine;
using UnityEngine.Rendering;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 5, -10);
    [SerializeField] private float cameraSpeed = 10f;

    private float cameraFOV = 50f;

    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();

        Instance = this;
    }


    private void Update()
    {
        CameraPositioner();
        
        _camera.fieldOfView = cameraFOV; // Set the camera's field of view

    }
    private void CameraPositioner()
    {
        if (target == null) return;

        Vector3 cameraPosition = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * cameraSpeed);
        transform.position = cameraPosition; // Smoothly move the camera to the target position with offset
    }

    public void SetCameraFOV(float fov)
    {
        cameraFOV = fov;
    }

    public float GetCameraFOV()
    {
        return cameraFOV;
    }
}
