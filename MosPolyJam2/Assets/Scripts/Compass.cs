using UnityEngine;

public class Compass : MonoBehaviour
{
    [SerializeField] private Transform arrowTransform;
    private Transform cameraTransform;

    private void Update()
    {
        Vector3 newEulerRotation = cameraTransform.eulerAngles;

        newEulerRotation.x = newEulerRotation.z = 0f;

        arrowTransform.localEulerAngles = newEulerRotation;
    }

    public void SetCameraTransform(Transform cameraTransform)
    {
        this.cameraTransform = cameraTransform;
    }
}