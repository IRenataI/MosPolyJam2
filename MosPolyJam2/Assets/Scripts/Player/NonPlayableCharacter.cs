using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NonPlayableCharacter : MonoBehaviour
{
    public UnityEvent PointReached { get; private set; } = new();
    [HideInInspector] public bool StopMovingWhenPointReached = true;

    [SerializeField, Range(0.1f, 5f)] private float movingSpeed = 5f;
    [SerializeField] private float rotationTime = 1f;

    private Vector3 targetPosition;
    private bool isMoving;
    private Animator animator;

    private Coroutine lookAtCoroutine;

    private BaseDanger currentDanger;
    private TimerView timerView;
    private TargetSwitcher targetSwitcher;

    private string movingAnimationTriggerName = "Move";

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        MoveToNextProgressPoint();
    }

    private void MoveToNextProgressPoint()
    {
        if (!isMoving)
            return;

        Vector3 nonNormalizedDirection = targetPosition - transform.position;

        float movingDistance = movingSpeed * Time.deltaTime;
        float targetDistance = nonNormalizedDirection.magnitude;

        if (movingDistance >= targetDistance)
        {
            transform.position = targetPosition;

            if (StopMovingWhenPointReached)
                StopMove();

            PointReached?.Invoke();
            return;
        }

        transform.position += movingDistance * nonNormalizedDirection.normalized;
    }

    private IEnumerator LookAt(Vector3 lookToPosition, float time)
    {
        Quaternion lookRotation = Quaternion.LookRotation(lookToPosition - transform.position, Vector3.up);
        
        if (time <= 0)
        {
            transform.rotation = lookRotation;
            yield break;
        }

        for (float t = Time.deltaTime; t <= time; t += Time.deltaTime)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, t / time);
            yield return new WaitForEndOfFrame();
        }
    }

    public void StartMove(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;

        ContinueMove();
    }

    [ContextMenu("Continue Move")]
    public void ContinueMove()
    {
        isMoving = true;

        if (lookAtCoroutine != null)
            StopCoroutine(lookAtCoroutine);
        lookAtCoroutine = StartCoroutine(LookAt(targetPosition, rotationTime));

        animator.SetTrigger(movingAnimationTriggerName);
        if(AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySound("Walk");
        }
    }

    [ContextMenu("Stop Move")]
    public void StopMove()
    {
        isMoving = false;

        animator.SetTrigger("Idle");
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StopSound("Walk");
        }
    }

    public void StartToFreeze(string animationName, BaseDanger danger, TimerView timer, TargetSwitcher targetSwitcher)
    {
        isMoving = false;

        if(currentDanger == null)
        {
            currentDanger = danger;
            timerView = timer;
            this.targetSwitcher = targetSwitcher;
        }

        if (AudioManager.Instance != null)
        {
            //playSound
            AudioManager.Instance.StopSound("Walk");
        }

        animator.Play(animationName);
    }

    public void Freeze()
    {
        if(currentDanger != null)
        {
            currentDanger.Init(timerView);
            timerView.gameObject.SetActive(true);
            targetSwitcher.IsEnabled = true;

            currentDanger = null;
        }
        animator.speed = 0f;
    }

    public void Unfreeze()
    {
        animator.speed = 1f;
    }

    public void SetMovingAnimation(string triggerName)
    {
        movingAnimationTriggerName = triggerName;
        Debug.Log("Setted moving animation trigger name: " + triggerName);
    }

    public void SetMovingSpeed(float value)
    {
        movingSpeed = value;
        Debug.Log("Setted moving speed: " + value);
    }
}