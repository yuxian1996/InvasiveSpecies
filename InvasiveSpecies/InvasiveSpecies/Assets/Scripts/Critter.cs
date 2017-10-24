using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Type;

public class Critter : MonoBehaviour {

	public float health = 100f;
	public float energy  = 100f;

	public float energyPerSecond = 5f;

	public float speed;
    private float baseSpeed;

	public string critterType = "Vegetable";

	static public Dictionary<string, List<Critter>> crittersByType;

	Vector3 velocity;

	public bool isInHedge = false;

	public List<WeightedDirection> desiredDirections;

    public Dictionary<string, NavMeshPath> paths;
    public Dictionary<string, int> pathOrder;
    NavMeshAgent agent;


    // Use this for initialization
    void Start () {
		// Make sure we're in the crittersByType list.
		if(crittersByType == null) {
			crittersByType = new Dictionary<string, List<Critter>>();
		}
		if(crittersByType.ContainsKey(critterType) == false) {
			crittersByType[critterType] = new List<Critter>();
		}
		crittersByType[critterType].Add(this);

        baseSpeed = speed;

        agent = GetComponent<NavMeshAgent>();

        paths = new Dictionary<string, NavMeshPath>();
        pathOrder = new Dictionary<string, int>();
	}

	void OnDestroy() {
		// Remove us from the crittersByType list.
		crittersByType[critterType].Remove(this);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// Critters lose energy per second.
		energy = Mathf.Clamp(energy - Time.deltaTime * energyPerSecond, 0, 100);

		if(energy <= 0) {
			// Lose health per second if we are starving
			health = Mathf.Clamp(health - Time.deltaTime * 5f, 0, 100);
		}

		if(health <= 0) {
			// We have been killed.
			// TODO: We could Instantiate a "death" object that has a
			// a play-once sound and a "splatter" effect.  Maybe leave
			// bones behind?
			Destroy(gameObject);
			return;
		}

			
		// Ask all of our AI scripts to tell us in which direction we should move
		desiredDirections = new List<WeightedDirection>();
		BroadcastMessage("DoAIBehaviour", SendMessageOptions.DontRequireReceiver);

		// Add up all the desired directions by weight
		Vector3 dir = Vector3.zero;
        float weight = 0;

        //set navigation path
        if (!agent)
            return;

        if (paths.ContainsKey(PathType.EVADE.ToString()))
        {
            agent.SetPath(paths[PathType.EVADE.ToString()]);
        }
        else if (paths.ContainsKey(PathType.SEEKFOOD.ToString()))
        {
            agent.SetPath(paths[PathType.SEEKFOOD.ToString()]);

        }
        else if (paths.ContainsKey(PathType.WANDER.ToString()))
        {
            agent.SetPath(paths[PathType.WANDER.ToString()]);
        }


        //foreach(WeightedDirection wd in desiredDirections) {
        //	// NOTE: If you are implementing EXCLUSIVE/FALLBACK blend modes, check here.

        //          if (wd.blending == WeightedDirection.BlendingType.EXCLUSIVE)
        //          {
        //              //weight = wd.weight;
        //              //dir = wd.direction;
        //              //speed = wd.speed;

        //              dir += wd.direction * wd.weight;

        //              foreach (WeightedDirection w in desiredDirections)
        //              {
        //                  if (wd != w && w.blending == WeightedDirection.BlendingType.EXCLUSIVE)
        //                  {
        //                      //if (wd.weight < w.weight)
        //                      //{
        //                      //    weight = w.weight;
        //                      //    dir = w.direction;
        //                      //    speed = w.speed;
        //                      //}

        //                      dir += w.direction * w.weight;
        //                  }
        //              }

        //              //dir = dir * weight;
        //              speed = baseSpeed;
        //              break;
        //          }
        //          else if (wd.blending == WeightedDirection.BlendingType.BLEND)
        //	    dir += wd.direction * wd.weight;
        //          else
        //              dir = wd.direction * wd.weight;

        //          speed = wd.speed;
        //}

        //velocity = Vector3.Lerp(velocity, dir.normalized * speed, Time.deltaTime * 5f);

        //// Move in the desired direction at our top speed.
        //// NOTE: WeightedDirection does include a currently unused parameter for speed
        //transform.Translate( velocity * Time.deltaTime );

        //      // foreach (var critter in desiredDirections)
        //      //Debug.Log(critter.direction);
        //      Debug.Log(speed);
    }

    // The base critter script probably doesn't need to know about collisions.
    // The response to collisions is going to be influenced by which
    // behaviours are running.

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.GetComponent<Hedge>() != null)
    //    {
    //        isInHedge = true;
    //    }
    //}

    //void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.GetComponent<Hedge>() != null)
    //    {
    //        isInHedge = false;
    //    }
    //}


}
