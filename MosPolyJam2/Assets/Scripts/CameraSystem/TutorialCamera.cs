using UnityEngine.Events;
using UnityEngine;
using Cinemachine;
using TMPro;

public class TutorialCamera : MonoBehaviour
{
    [Header("Description")]
    [TextArea] [SerializeField] private string descriptionText;
    [SerializeField] private TMP_Text description;
    [Header("End Stage")]
    [SerializeField] private KeyCode keyToEndStage;
    [SerializeField] private UnityEvent OnStageEnded;
    private CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        virtualCamera.enabled = false;
    }

    private void Update()
    {
        if (virtualCamera == null || !virtualCamera.enabled)
            return;

        if (Input.GetKeyDown(keyToEndStage))
        {
            EndStage();
        }
    }

    private void EndStage()
    {
        virtualCamera.enabled = false;

        description.text = "";

        OnStageEnded?.Invoke();
    }

    public void SetDescription()
    {
        description.text = descriptionText;
    }
}
