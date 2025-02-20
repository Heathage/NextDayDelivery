using System.Collections.Generic;
using UnityEngine;

public class SingletonClass : MonoBehaviour
{
    #region Singleton
    public static SingletonClass Instance;
    private void Awake()
    {
        if (Instance) Destroy(this);
        else Instance = this;
    }
    #endregion

    public GameObject Drones;
}