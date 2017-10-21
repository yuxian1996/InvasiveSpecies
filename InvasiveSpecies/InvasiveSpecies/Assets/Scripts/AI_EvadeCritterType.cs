using UnityEngine;
using System.Collections;

public class AI_EvadeCritterType : MonoBehaviour {

	public string critterType = "Carnivore";

    public float safeDistance = 12f;

	Critter myCritter;

	// Use this for initialization
	void Start () {
		myCritter = GetComponent<Critter>();
	}

	void DoAIBehaviour() {

		if(myCritter.isInHedge) {
			// We are hidden, so we don't have to evade anything.
			return;
		}



		if(Critter.crittersByType.ContainsKey(critterType) == false) {
			// We have nothing to eat!
			return;
		}

		// Find the closest edible critter to us.
		Critter closest = null;
		float dist = Mathf.Infinity;

		foreach(Critter c in Critter.crittersByType[critterType]) {
			if(c.health <= 0) {
				// This is already dead, ignore it.
				continue;
			}

			float d = Vector3.Distance(this.transform.position, c.transform.position);

			if(closest == null || d < dist) {
				closest = c;
				dist = d;
			}

		}

		if(closest == null) {
			// No valid food targets exist.
			return;
		}


		// Now we want to move towards this closest edible critter

		Vector3 dir = closest.transform.position - this.transform.position;
		dir *= -1;  // We are running AWAY from this.

        // IF the Badger is right on top of us, we'd like a weight of 100
        // But if we are at a safe distance don't move any further

        float weight;
        float speed;
        WeightedDirection wd;

        if (dist < safeDistance)
        {
            weight = 10 / (dist * dist);
            speed = 10 / (dist * dist);

            wd = new WeightedDirection(dir, weight, 2, WeightedDirection.BlendingType.EXCLUSIVE);
        }
        else
        {
            weight = 0;

            wd = new WeightedDirection(dir, weight);
        }
            
		myCritter.desiredDirections.Add( wd );
	}
}
