using UnityEngine;
using UnityEngine.Events;

public class ProgressManager : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnComplete;

    [SerializeField] private bool activateOnStart = true;
    [SerializeField] private ProgressPoint[] progressPoints;
    [Header("Refs")]
    [SerializeField] private NonPlayableCharacter npc;
    [SerializeField] private TargetSwitcher targetSwitcher;

    private int targetProgressPoint;
    private BaseDanger currentDanger;

    private void Start()
    {
        if (activateOnStart)
            StartLevel();
    }

    private void OnPointReached()
    {
        progressPoints[targetProgressPoint].onReached?.Invoke();

        currentDanger = progressPoints[targetProgressPoint].dangerAction;
        if (currentDanger != null)
        {
            npc.StartToFreeze(currentDanger.AnimationName);

            currentDanger.OnComplete.AddListener(OnDangerCompleted);
            currentDanger.Init(); 

            targetSwitcher.IsEnabled = true;

            return;
        }

        MoveToNextProgressPoint();
    }

    private void OnDangerCompleted()
    {
        currentDanger.OnComplete.RemoveListener(OnDangerCompleted);
        npc.Unfreeze();
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
            FinishLevel();
        }
    }

    private void FinishLevel()
    {
        StopLevel();
        npc.StopMovingWhenPointReached = true;
        npc.PointReached.RemoveListener(OnPointReached);

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