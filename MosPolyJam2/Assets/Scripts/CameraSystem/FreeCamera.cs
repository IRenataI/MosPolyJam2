using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
[RequireComponent(typeof(Rigidbody))]
public class FreeCamera : MonoBehaviour
{
    public Transform RotationCostylTransform { get; private set; }

    [SerializeField] private float speed = 5f;
    
    private CinemachineVirtualCamera cvCamera;
    private CinemachinePOV cameraPOV;
    private Rigidbody rb;

    private void Start()
    {
        cvCamera = GetComponent<CinemachineVirtualCamera>();
        cameraPOV = cvCamera.GetCinemachineComponent<CinemachinePOV>();

        rb = GetComponent<Rigidbody>();

        RotationCostylTransform = new GameObject("Rotation Costyl").transform;
    }

    private void FixedUpdate()
    {
        if (!cvCamera.enabled)
            return;

        Move();
    }

    private void Move()
    {
        Vector3 rotation = new(cameraPOV.m_VerticalAxis.Value, cameraPOV.m_HorizontalAxis.Value, 0f);
        RotationCostylTransform.eulerAngles = rotation;

        float x = Input.GetAxis("Vertical");
        float z = Input.GetAxis("Horizontal");
        float y = 0f;

        if (Input.GetKey(KeyCode.Space))
            y = 1f;
        else if (Input.GetKey(KeyCode.LeftShift))
            y = -1f;

        rb.AddForce(speed * (x * RotationCostylTransform.forward + y * RotationCostylTransform.up + z * RotationCostylTransform.right));

        // transform.position +=   speed * Time.deltaTime *
        //                         (x * rotationCostylTransform.forward +
        //                         y * rotationCostylTransform.up +
        //                         z * rotationCostylTransform.right);
    }
}