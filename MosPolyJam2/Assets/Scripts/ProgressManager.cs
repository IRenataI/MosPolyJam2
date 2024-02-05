using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ProgressManager : MonoBehaviour
{
    public UnityEvent OnComplete { get; private set; } = new();

    [SerializeField] private bool activateOnStart = true;
    [SerializeField] private ProgressPoint[] progressPoints;

    [Header("Refs")]
    [SerializeField] private NonPlayableCharacter npc;
    private NPCHealthSystem npcHealthSystem;
    [SerializeField] private TargetSwitcher targetSwitcher;

    [Header("Danger Timer")]
    [SerializeField] private TimerView dangerTimerView;

    [Header("UI")]
    [SerializeField] private GameObject aim;
    [SerializeField] private GameObject endGameCanvas;
    [SerializeField] private TextMeshProUGUI endGameLabel;
    [SerializeField] private string winText;
    [SerializeField] private string failureText;

    private int targetProgressPoint;
    private BaseDanger currentDanger;

    private void Start()
    {
        if (activateOnStart)
            StartLevel();

        aim.SetActive(true);
        endGameCanvas.SetActive(false);
        dangerTimerView.gameObject.SetActive(false);

        npcHealthSystem = npc.GetComponent<NPCHealthSystem>();
        npcHealthSystem.Died.AddListener(() => FinishLevel(false));
    }

    private void OnPointReached()
    {
        progressPoints[targetProgressPoint].onReached?.Invoke();

        currentDanger = progressPoints[targetProgressPoint].dangerAction;
        if (currentDanger != null)
        {
            npc.StartToFreeze(currentDanger.AnimationName, currentDanger, dangerTimerView);

            currentDanger.OnComplete.AddListener(OnDangerCompleted);
            //currentDanger.Init(dangerTimerView);
            //dangerTimerView.gameObject.SetActive(true);

            targetSwitcher.IsEnabled = true;

            return;
        }

        MoveToNextProgressPoint();
    }

    private void OnDangerCompleted(bool isSuccess)
    {
        currentDanger.OnComplete.RemoveListener(OnDangerCompleted);
        dangerTimerView.gameObject.SetActive(false);

        if (isSuccess)
        {

        }
        else
        {
            npcHealthSystem.TakeDamage();
        }

        npc.Unfreeze();

        targetSwitcher.CurrentCameraState = CameraStates.Spectator;
        targetSwitcher.IsEnabled = false;

        currentDanger = null;
    }

    public void MoveToNextProgressPoint()
    {
        targetProgressPoint++;

        if (targetProgressPoint < progressPoints.Length)
        {
            npc.StartMove(progressPoints[targetProgressPoint].worldPositionTransform.position);
        }
        else
        {
            FinishLevel(true);
        }
    }

    private void FinishLevel(bool isSuccess)
    {
        StopLevel();
        npc.StopMovingWhenPointReached = true;
        npc.PointReached.RemoveListener(OnPointReached);

        Transform lookAt = new GameObject("Look At Empty").transform;
        lookAt.position = npc.transform.position + 1.5f * Vector3.up;
        targetSwitcher.LookAt_EndGame(lookAt);

        aim.SetActive(false);
        endGameCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;

        if (isSuccess)
        {
            endGameLabel.text = winText;
        }
        else
        {
            endGameLabel.text = failureText;
        }

        OnComplete?.Invoke();
    }

    [ContextMenu("Start Level")]
    public void StartLevel()
    {
        targetProgressPoint = 1;
        npc.StopMovingWhenPointReached = false;
        npc.PointReached.AddListener(OnPointReached);

        npc.transform.position = progressPoints[0].worldPositionTransform.position;
        npc.StartMove(progressPoints[targetProgressPoint].worldPositionTransform.position);
    }

    public void ContinueLevel()
    {
        npc.ContinueMove();
    }

    public void StopLevel()
    {
        npc.StopMove();
    }

    #if UNITY_EDITOR

    [Header("Editor")]
    [SerializeField] private Transform[] progressPointsTransformsEditor;

    public void InitializeProgressPointsArray()
    {
        ProgressPoint[] backup = progressPoints;
        progressPoints = new ProgressPoint[progressPointsTransformsEditor.Length];

        for (int i = 0; i < progressPoints.Length; i++)
        {
            progressPoints[i].worldPositionTransform = progressPointsTransformsEditor[i];

            int backupIndex = FindInBackup(progressPointsTransformsEditor[i]);
            if (backupIndex >= 0)
            {
                progressPoints[i].dangerAction = backup[backupIndex].dangerAction;
                progressPoints[i].onReached = backup[backupIndex].onReached;
            }
        }

        UnityEditor.SceneManagement.EditorSceneManager.SaveScene(UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene());

        int FindInBackup(Transform targetTransform)
        {
            if (targetTransform == null)
                return -1;

            for (int i = 0; i < backup.Length; i++)
            {
                if (backup[i].worldPositionTransform == null)
                    continue;

                if (backup[i].worldPositionTransform == targetTransform ||
                    backup[i].worldPositionTransform.position == targetTransform.position)
                    return i;
            }

            return -1;
        }
    }

    #endif
}