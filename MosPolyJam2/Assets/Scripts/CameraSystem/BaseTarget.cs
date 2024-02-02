using System.Collections;
using UnityEngine;

public class BaseTarget : Target
{
    public override IEnumerator Activate()
    {
        StartCoroutine(base.Activate());

        isActive = true;
        yield return new WaitForSeconds(2f);
        isActive = false;
        Debug.Log($"{this.name} has been deactivated");
    }
}
