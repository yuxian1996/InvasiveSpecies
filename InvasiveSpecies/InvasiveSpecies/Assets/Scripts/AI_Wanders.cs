using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI_Wanders : MonoBehaviour {

	Critter myCritter;
    AI_SeekFood hunger;

	Vector3 fromDir;
	Vector3 toDir;

    float speed = 1;

	// Use this for initialization
	void Start () {
		myCritter = GetComponent<Critter>();
        hunger = GetComponent<AI_SeekFood>();
		//fromDir = Random.onUnitSphere;
		//toDir = Random.onUnitSphere;

        InvokeRepeating("SetNewDir", 1, 1.5f);
    }

    void SetNewDir()
    {
        fromDir = Random.onUnitSphere;
        toDir = Random.onUnitSphere;

        if (speed == 0)
            speed = 1;
        else
            speed = 0;
    }

	void DoAIBehaviour() {
		Vector3 dir = toDir - fromDir;
        WeightedDirection wd;

        if (hunger.isHungry == false)
            wd = new WeightedDirection(dir, 0.01f, speed, WeightedDirection.BlendingType.FALLBACK);
        else
            wd = new WeightedDirection(dir, 0);

        myCritter.desiredDirections.Add( wd );
	}

}
