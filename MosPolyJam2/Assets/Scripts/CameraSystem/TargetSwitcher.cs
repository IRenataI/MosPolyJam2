using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class TargetSwitcher : MonoBehaviour
{
    public bool IsEnabled { get; set; }

    public CameraStates CurrentCameraState
    {
        get { return currentCameraState; }
        set
        {
            if (currentCameraState == value)
                return;

            currentCameraState = value;

            if (currentCameraState == CameraStates.Virtual)
            {
                virtualCamera.enabled = true;
                spectatorCamera.enabled = false;
            }
            else if(currentCameraState == CameraStates.Spectator)
            {
                spectatorCamera.transform.position = virtualCamera.transform.position;

                spectatorCamera.enabled = true;
                virtualCamera.enabled = false;

                SetTarget(null);
            }
        }
    }
    private CameraStates currentCameraState = CameraStates.Spectator;

    public Vector3 NPCFollowOffset => followOffset;

    [Header("Cameras")]
    [SerializeField] private Camera normalCamera;
    [Space(10)]
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Vector3 followOffset = new(0f, 2.5f, 0f);
    [Space(10)]
    [SerializeField] private CinemachineVirtualCamera spectatorCamera;

    private Vector3 screenCenter = new(Screen.width / 2f, Screen.height / 2f, 0f);
    private CinemachineTransposer virtualCameraTransposer;

    [Header("Interaction settings")]
    [SerializeField] private float interactionMaxDistance = 50f;
    [Space(5)]
    [SerializeField] private KeyCode interactionKey = KeyCode.E;
    [SerializeField] private float interactionTimer = 1.5f;
    private float timer = 0f;

    private IInteractable currentInteractable;
    private BaseTarget target;

    [Header("Refs")]
    [SerializeField] private Transform npcObject;
    [SerializeField] private Image timerImage;

    public void SetTargetObject(Transform targetObject, Vector3 followOffset)
    {
        virtualCamera.Follow = targetObject;
        virtualCamera.LookAt = targetObject;

        virtualCameraTransposer.m_FollowOffset = followOffset;
    }

    public void SetTarget(BaseTarget target)
    {
        if (this.target != null)
        {
            this.target.IsEnabled = false;
        }

        this.target = target;
        if (target != null)
        {
            target.IsEnabled = true;
        }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        virtualCameraTransposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        SetTargetObject(npcObject, followOffset);
        SetTarget(null);
    }

    private void LateUpdate()
    {
        if (!IsEnabled)
            return;

        SearchInteractable();
        InteractTarget();
    }

    private void SearchInteractable()
    {
        IInteractable interactable = RaycastToInteractable();
        if (currentInteractable == interactable)
            return;

        currentInteractable?.Deselect();
        currentInteractable = interactable;
        currentInteractable?.Select();
    }

    private void InteractTarget()
    {
        if (currentInteractable == null || Input.GetKey(interactionKey) != true)
        {
            timer = 0f;
            timerImage.fillAmount = 0f;
            //currentInteractable = null;
            //ClearInteraction(); // every frame calls deselect() -> select() -> deselect() -> ...
            return;
        }

        if (currentInteractable.GetTarget().IsEnabled)
            return;

        if (timer >= interactionTimer)
        {
            currentInteractable.Interact(this);
            CurrentCameraState = CameraStates.Virtual;

            ClearInteraction();
        }
        else
        {
            timer += Time.deltaTime;
            timerImage.fillAmount = timer / interactionTimer;
        }
    }

    private void ClearInteraction()
    {
        timer = 0f;

        if (currentInteractable != null)
        {
            currentInteractable.Deselect();
            currentInteractable = null;

            timerImage.fillAmount = 0f;
        }
    }

    private IInteractable RaycastToInteractable()
    {
        Ray ray = normalCamera.ScreenPointToRay(screenCenter);

        IInteractable output = null;
        if (Physics.Raycast(ray, out RaycastHit hitInfo, interactionMaxDistance))
        {
            if (!hitInfo.transform.TryGetComponent(out output))
            {
                ClearInteraction();
            }
        }
        else
        {
            ClearInteraction();
        }

        return output;
    }
}

[System.Serializable]
public enum CameraStates { Spectator, Virtual };
