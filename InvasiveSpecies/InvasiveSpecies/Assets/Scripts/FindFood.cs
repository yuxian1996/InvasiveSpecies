using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindFood : MonoBehaviour {
    public string[] foodClass;
    public string[] foodTag;
    public float radius = 5.0f;

    int layerMask = 0;
    Transform target;
    FindPath findPath;

    // Use this for initialization
    void Start () {
        foreach (string mask in foodClass)
            layerMask += 1 << (LayerMask.NameToLayer(mask));

        if (GetComponent<FindPath>())
            findPath = GetComponent<FindPath>();
    }

    // Update is called once per frame
    void Update () {
        if (target == null)
        {
            target = findfirstFood();
            if(target)
                findPath.target = target;
            print(target);
        }
    }

    Transform findfirstFood()
    {
        //find object nearby
        Collider[] hitAnimal = Physics.OverlapSphere(transform.position, radius, layerMask);
        if (hitAnimal.Length >= 1)
            foreach (Collider animal in hitAnimal)
            {
                if (foodTag.Length >= 1)
                    foreach (string tag in foodTag)
                        if (animal.tag == tag)
                            return animal.transform;
                return animal.transform;
            }
        return null;
    }

    //destroy when hit
    private void OnTriggerEnter(Collider other)
    {
        if (target && other.gameObject == target.gameObject)
            Destroy(target.gameObject);
    }
}
