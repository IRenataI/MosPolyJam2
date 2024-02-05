using UnityEngine;
using UnityEngine.Events;

public class NPCHealthSystem : MonoBehaviour
{
    public UnityEvent Died { get; private set; } = new();

    [SerializeField] private int healthCount;
    [SerializeField] private GameObject healthIconPrefab;
    [SerializeField] private Transform healthParent;

    private GameObject[] healthObjects;

    private void Start()
    {
        healthObjects = new GameObject[healthCount];

        for(int i = 0; i < healthCount; i++)
        {
            healthObjects[i] = Instantiate(healthIconPrefab, healthParent);
        }
    }

    public void TakeDamage()
    {
        healthCount--;

        Destroy(healthObjects[healthCount]);

        if(healthCount <= 0)
        {
            Died?.Invoke();
        }
    }
}
