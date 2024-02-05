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
        GameObject firstObject = objects[1];
        GameObject secondObject = objects[0];
        objects[1] = secondObject;
        objects[0] = firstObject;

        SwitchPositions(firstObject.transform, secondObject.transform);
        counter++;

        if (counter >= 2)
        {
            Success();
        }
    }

    public void Switch_BadDish2()
    {
        GameObject firstObject = objects[2];
        GameObject secondObject = objects[1];
        objects[2] = secondObject;
        objects[1] = firstObject;

        SwitchPositions(firstObject.transform, secondObject.transform);
        counter++;
    }
}