using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour {

    private GameSystem m_Instance;
    public GameSystem Instance { get { return m_Instance; } }

    public static ArrayList herbivores, carnivores, plants;

    void Awake()
    {
        m_Instance = this;
    }

    void OnDestroy()
    {
        m_Instance = null;
    }

    void Start()
    {
        herbivores = new ArrayList();
        carnivores = new ArrayList();
        plants = new ArrayList();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Log message");

            foreach(var herbivore in herbivores)
                Debug.Log(herbivore);

            foreach (var herbivore in carnivores)
                Debug.Log(herbivore);

            foreach (var herbivore in plants)
                Debug.Log(herbivore);
        }
    }

    void OnGui()
    {
        // common GUI code goes here
    }

    // etc.
}
