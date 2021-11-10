using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavedName : MonoBehaviour
{
    public static SavedName Instance;
    public Text pName;
    // Start is called before the first frame update
    void Start()
    {
        pName = GetComponent<Text>();
        //pName.text = "PPPPPP";
    }

    private void Awake()
    {
        //Destroys the extra "public static MainManager Instance;" created if script is called again:
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
