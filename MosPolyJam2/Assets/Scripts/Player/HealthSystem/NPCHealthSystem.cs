using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHealthSystem : MonoBehaviour
{
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

        Destroy(healthObjects[healthCount].gameObject);

        if(healthCount <= 0)
        {
            // end game
        }
    }
}
