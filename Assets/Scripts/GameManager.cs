using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public void TogglePause(bool toggle)
    { }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        if (Instance != null)
        {
            Destroy(gameObject.GetComponent<GameManager>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
