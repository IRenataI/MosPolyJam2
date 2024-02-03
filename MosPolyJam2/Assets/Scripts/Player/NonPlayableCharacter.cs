using UnityEngine;
using UnityEngine.Events;

public class NonPlayableCharacter : MonoBehaviour
{
    [HideInInspector] public UnityEvent PointReached;

    [SerializeField, Range(0.1f, 5f)] private float movingSpeed = 5f;

    private Vector3 startPosition, targetPosition;
    private bool isMoving;
    private Animator animator;

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
            StopMove();
            PointReached?.Invoke();
            return;
        }

        transform.position += movingDistance * nonNormalizedDirection.normalized;
    }

    public void StartMove(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
        startPosition = transform.position;

        transform.LookAt(targetPosition, Vector3.up);

        ContinueMove();
    }

    [ContextMenu("Continue Move")]
    public void ContinueMove()
    {
        isMoving = true;

        animator.SetTrigger("Move");
    }

    [ContextMenu("Stop Move")]
    public void StopMove()
    {
        isMoving = false;

        animator.SetTrigger("Idle");
    }

    public void Freeze()
    {
        StopMove();
        animator.speed = 0f;
    }

    public void Unfreeze()
    {
        ContinueMove();
        animator.speed = 1f;
    }
}