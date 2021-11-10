using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameManager : MonoBehaviour
{
    public static NameManager Instance;
    public string pName;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        //(It would be better to only ever assign Instance if null to begin with, rather than
        //destroy copies.)

        Instance = this;
        DontDestroyOnLoad(gameObject);

    }
}
