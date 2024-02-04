using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class TargetSwitcher : MonoBehaviour
{
    public bool IsEnabled
    {
        get { return isEnabled; }
        set
        {
            if (value == isEnabled)
                return;

            isEnabled = value;
            if (isEnabled)
            {
                virtualCamera.enabled = true;
                spectatorCamera.Priority = 10;
                virtualCamera.Priority = 20;
                spectatorCamera.enabled = false;
            }
            else
            {
                spectatorCamera.transform.position = virtualCamera.transform.position;

                spectatorCamera.enabled = true;
                virtualCamera.Priority = 10;
                spectatorCamera.Priority = 20;
                virtualCamera.enabled = false;
            }
        }
    }
    private bool isEnabled;

    [Header("Cameras")]
    [SerializeField] private Camera normalCamera;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private CinemachineVirtualCamera spectatorCamera;
    private Vector3 screenCenter = new(Screen.width / 2f, Screen.height / 2f, 0f);

    [Header("Interaction settings")]
    [SerializeField] private float interactionMaxDistance = 50f;
    [Space(5)]
    [SerializeField] private KeyCode interactionKey = KeyCode.E;
    [SerializeField] private float interactionTimer = 1.5f;
    private float timer = 0f;
    [Space(5)]
    [SerializeField] private KeyCode backKey = KeyCode.Escape;

    private IInteractable currentInteractable;
    private BaseTarget target;

    [Header("Refs")]
    [SerializeField] private Transform npcObject;
    [SerializeField] private Image timerImage;

    public void SetTargetObject(Transform targetObject, BaseTarget target)
    {
        virtualCamera.Follow = targetObject;
        virtualCamera.LookAt = targetObject;

        this.target = target;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        SetTargetObject(npcObject, null);
    }

    private void LateUpdate()
    {
        if (!IsEnabled)
            return;

        SearchInteractable();
        InteractTarget();
        ActivateTarget();

        if (Input.GetKeyDown(backKey))
        {
            SetTargetObject(npcObject, null);
        }
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
            ClearInteraction();
            return;
        }

        if (timer >= interactionTimer)
        {
            currentInteractable.Interact(this);

            ClearInteraction();
        }
        else
        {
            timer += Time.deltaTime;
            timerImage.fillAmount = timer / interactionTimer;
        }
    }

    private void ActivateTarget()
    {
        if (target == null || Input.GetKeyDown(target.activationKey) != true || target.IsActiveted)
            return;

        target.Activate();
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
