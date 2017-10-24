using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {
    static SceneManager _instance;

    public GameObject prefabCarcasse;

    void Awake()
    {
        _instance = this;
    }

    public static SceneManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public static void createCarcasse(Transform transform)
    {
        GameObject obj = Instantiate(Instance.prefabCarcasse, transform.position, Quaternion.Euler(0, 0, 0), transform.parent);
    }
}
