using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitBehaviour : MonoBehaviour
{
	public GameObject[] checkpoints;

	private int currCheckpointIndex = 0;
	private GameObject currCheckpoint;
	private Animator anim;
	private GameObject oldCheckpoint;

    // Start is called before the first frame update and is used for initialization
    void Start()
    {
		anim = GetComponentInChildren<Animator>();
		// selecting and looking at the first checkpoint
		currCheckpoint = checkpoints[currCheckpointIndex];
		transform.LookAt(currCheckpoint.transform);
		// This statement sets the hopping parameter to true triggering the hops
		anim.SetBool("hopping", true);
    }

    // Update is called once per frame
    void Update()
    {
		SetCheckpoint();

		// angles rabbit towards checkpoint
		Vector3 targetPosition = new Vector3(currCheckpoint.transform.position.x, 2.36f, currCheckpoint.transform.position.z);
		transform.LookAt(targetPosition, Vector3.up);
    }

	// updates target checkpoint when we've reached the current one
	private void SetCheckpoint()
	{
		float dist = Vector3.Distance(currCheckpoint.transform.position, transform.position);
		if (dist < 5 && currCheckpointIndex < checkpoints.Length -1)
		{
			currCheckpointIndex++;
			currCheckpoint = checkpoints[currCheckpointIndex];
		}
	}

	private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "tree" || other.gameObject.name == "bush" || other.gameObject.name == "rock")
		{
			transform.LookAt(other.gameObject.transform.position);
			anim.SetBool("hopping", false);
			int explore = Random.Range(1, 3);
			anim.SetInteger("explore", explore);
			oldCheckpoint = currCheckpoint;
			currCheckpoint = other.gameObject;
			Debug.Log(oldCheckpoint.gameObject.name);
		}
    }

	private void RenableHopping()
	{
		currCheckpoint = oldCheckpoint;
		anim.SetInteger("explore", 0);
		anim.SetBool("hopping", true);
	}
}
