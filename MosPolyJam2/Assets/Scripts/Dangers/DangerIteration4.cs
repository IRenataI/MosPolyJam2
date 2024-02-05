using UnityEngine;

public class DangerIteration4 : BaseDanger
{
    [Header("Danger Iteration 4")]
    [SerializeField] private GameObject[] objects;
    private int counter;

    private void SwitchPositions(Transform t1, Transform t2)
    {
        Vector3 tmp = t1.transform.position;
        t1.position = t2.position;
        t2.position = tmp;
    }

    // public void SwitchObjects()
    // {
    //     Transform firstObjectTransform = objects[objects.Length - 1 - counter].transform;
    //     Transform secondObjectTransform = objects[objects.Length - 2 - counter].transform;

    //     SwitchPositions(firstObjectTransform, secondObjectTransform);

    //     counter++;
    // }

    public void Switch_BadDish1()
    {
        Transform firstObjectTransform = objects[1].transform;
        Transform secondObjectTransform = objects[0].transform;

        SwitchPositions(firstObjectTransform, secondObjectTransform);
        counter++;

        if (counter >= 2)
        {
            Success();
        }
    }

    public void Switch_BadDish2()
    {
        Transform firstObjectTransform = objects[2].transform;
        Transform secondObjectTransform = objects[1].transform;

        SwitchPositions(firstObjectTransform, secondObjectTransform);
        counter++;
    }
}