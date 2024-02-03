using Cinemachine;
using UnityEngine;

public class TargetSwitcher : MonoBehaviour
{
    public bool IsEnabled { get; private set; } = true;

    [Header("Cameras")]
    public Camera NormalCamera;
    public CinemachineVirtualCamera VirtualCamera;
    private Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

    [Header("Refs")]
    public Transform NPCObject;

    [Header("Interaction settings")]
    [SerializeField] private float interactionMaxDistance = 50f;
    [Space(5)]
    [SerializeField] private KeyCode interactionKey = KeyCode.E;
    [SerializeField] private float interactionTimer = 1.5f;
    private float timer = 0f;
    [Space(5)]
    [SerializeField] private KeyCode backKey = KeyCode.Escape;

    private IInteractable currentInteractable;
    private Target target;
    
    public void SetTargetObject(Transform targetObject, Target target)
    {
        VirtualCamera.Follow = targetObject;
        VirtualCamera.LookAt = targetObject;

        this.target = target;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        if (!IsEnabled)
            return;

        SearchInteractable();
        Interact();
        Activate();

        if (Input.GetKeyDown(backKey))
        {
            SetTargetObject(NPCObject, null);
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
        if (currentInteractable == null || Input.GetKey(interactionKey) != true)
        {
            timer = 0f;
            return;
        }

        timer += Time.deltaTime;
        Debug.Log($"Interation time {timer}");

        if (timer >= interactionTimer)
        {
            timer = 0f;
            currentInteractable.Interact(this);
        }
        
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
        Ray ray = NormalCamera.ScreenPointToRay(screenCenter);

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
