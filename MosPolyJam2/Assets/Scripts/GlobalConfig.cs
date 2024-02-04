using UnityEngine;

public class GlobalConfig : MonoBehaviour
{
    public static string ConfigSO 
    {
        get
        {
            return configSO;
        }
    }

    private static string configSO;
}