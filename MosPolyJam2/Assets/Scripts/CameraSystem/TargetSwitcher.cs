using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class TargetSwitcher : MonoBehaviour
{
    public bool IsEnabled = true; //{ get; private set; }

    public BaseDanger danger; // to delete

    [Header("Cameras")]
    [SerializeField] private Camera normalCamera;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private Vector3 screenCenter = new(Screen.width / 2f, Screen.height / 2f, 0f);

    [Header("Refs")]
    [SerializeField] private Transform npcObject;

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
    
    [Header("UI")]
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
        Interact();
        Activate();

        if (Input.GetKeyDown(backKey))
        {
            danger.Complete();
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

    private void Interact()
    {
        if (currentInteractable == null || Input.GetKey(interactionKey) != true)
        {
            timer = 0f;
            return;
        }

        timer += Time.deltaTime;
        timerImage.fillAmount = timer / interactionTimer;
        // Debug.Log($"Interation time {timer}");

        if (timer >= interactionTimer)
        {
            timer = 0f;
            timerImage.fillAmount = 0f;
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
            hitInfo.transform.TryGetComponent(out output);
        }
        else
        {
            ClearInteraction();
        }

        return output;
    }
}
