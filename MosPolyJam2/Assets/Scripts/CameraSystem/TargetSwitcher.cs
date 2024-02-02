using Cinemachine;
using UnityEngine;

public class TargetSwitcher : MonoBehaviour
{
    public bool isEnabled { get; private set; } = true;

    [Header("Refs")]
    public Camera normalCamera;
    public CinemachineVirtualCamera virtualCamera;
    [Space(5)]
    public Transform playerObject;

    [Header("Interaction settings")]
    [SerializeField] private float interactionMaxDistance;
    [SerializeField] private KeyCode interactionKey;
    [SerializeField] private KeyCode backKey;

    private IInteractable currentInteractable;
    private Target target;
    private Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

    public void SetTargetObject(Transform targetObject, Target target)
    {
        virtualCamera.Follow = targetObject;
        virtualCamera.LookAt = targetObject;

        this.target = target;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        if (!isEnabled)
            return;

        SearchInteractable();
        Interact();
        Activate();

        if (Input.GetKeyDown(backKey))
        {
            SetTargetObject(playerObject, null);
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

    private void Interact()
    {
        if (currentInteractable == null || Input.GetKeyDown(interactionKey) != true)
            return;

        currentInteractable.Interact(this);
    }

    private void Activate()
    {
        if (target == null || Input.GetKeyDown(target.activationKey) != true)
            return;

        StartCoroutine(target.Activate());
    }

    private void ClearInteraction()
    {
        currentInteractable?.Deselect();
        currentInteractable = null;
    }

    private IInteractable RaycastToInteractable()
    {
        Ray ray = normalCamera.ScreenPointToRay(screenCenter);

        IInteractable output = null;
        if (Physics.Raycast(ray, out RaycastHit hitInfo, interactionMaxDistance))
        {
            hitInfo.transform.TryGetComponent(out output);
        }
        else
        {
            ClearInteraction();
        }

        return output;
    }
}
