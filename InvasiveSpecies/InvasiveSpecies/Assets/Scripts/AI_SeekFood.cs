using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using Type;

public class AI_SeekFood : MonoBehaviour {

	public string critterType = "Vegetable";

	public float eatingRange = 1f;
	public float eatHPPerSecond = 5f;
	public float eatHP2Energy = 2f;
    public float hungerThreshold = 40f;

    public bool isHungry = false;

    public int order = 0;

    private float speed;

	Critter myCritter;

    NavMeshAgent agent;
    

	// Use this for initialization
	void Start () {
		myCritter = GetComponent<Critter>();

        speed = myCritter.speed;

        if (GetComponent<NavMeshAgent>())
            agent = GetComponent<NavMeshAgent>();
	}

	void DoAIBehaviour() {

        if (Critter.crittersByType.ContainsKey(critterType) == false) {
            // We have nothing to eat!
			return;
		}

		// Find the closest edible critter to us.
		Critter closest = null;
		float dist = Mathf.Infinity;

		foreach(Critter c in Critter.crittersByType[critterType]) {


            if (c.health <= 0) {
				// This is already dead, ignore it.
				continue;
			}

            if (myCritter.energy > hungerThreshold && isHungry == false)
            {
                //I'm not hungry right now.
                continue;
            }
            else
            {
                isHungry = true;
            }

            if (isHungry && myCritter.energy > 80)
                isHungry = false;

			if(c.isInHedge) {
                // This possible target is hidden, so we can't chase it.
                Debug.Log("i'm hiding!");
                continue;
			}

			float d = Vector3.Distance(Vector3.ProjectOnPlane(this.transform.position, Vector3.down), 
                Vector3.ProjectOnPlane(c.transform.position, Vector3.down));

            //Debug.Log("eat dammit!" + d);

            if (d < myCritter.SeekRadius && (closest == null || d < dist)) {
				closest = c;
				dist = d;
			}

		}

		if(closest == null) {
			// No valid food targets exist.
			return;
		}

		if(dist < eatingRange) {
			float hpEaten = Mathf.Clamp(eatHPPerSecond * Time.deltaTime, 0, closest.health);
			closest.health -= hpEaten;
			myCritter.energy += hpEaten * hpEaten;
		}
		else {
			// Now we want to move towards this closest edible critter

			Vector3 dir = closest.transform.position - this.transform.position;

            //WeightedDirection wd;

            if (isHungry)
            {
                // wd = new WeightedDirection( dir, 1, 1.5f, WeightedDirection.BlendingType.EXCLUSIVE );
                if (agent)
                {
                    NavMeshPath path = new NavMeshPath();
                    agent.CalculatePath(dir * 0.1f + transform.position, path);
                    if (myCritter.paths.ContainsKey(PathType.SEEKFOOD.ToString()) )
                    {
                        if(myCritter.pathOrder[PathType.SEEKFOOD.ToString()] >= order)
                        {
                            myCritter.paths[PathType.SEEKFOOD.ToString()] = path;
                            myCritter.pathOrder[PathType.SEEKFOOD.ToString()] = order;
                        }
                    }
                    else
                    {
                        myCritter.paths.Add(PathType.SEEKFOOD.ToString(), path);
                        myCritter.pathOrder.Add(PathType.SEEKFOOD.ToString(), order);
                    }
                }
            }
            //clear path
            else
            {
                if (myCritter.paths.ContainsKey(PathType.SEEKFOOD.ToString()))
                {
                    myCritter.paths.Clear();
                    myCritter.pathOrder.Clear();
                }
            }

            //else
            //wd = new WeightedDirection(dir, 0);

            //myCritter.desiredDirections.Add( wd );

        }
    }
}
