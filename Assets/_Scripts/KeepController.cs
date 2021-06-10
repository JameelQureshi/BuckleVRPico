using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepController : MonoBehaviour
{
    public static KeepController instance;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
