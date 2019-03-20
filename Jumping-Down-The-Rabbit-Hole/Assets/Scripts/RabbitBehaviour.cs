using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitBehaviour : MonoBehaviour
{
	public GameObject[] checkpoints;
	//public GameObject rabbitModel;
	private int currCheckpointIndex = 0;
	private GameObject currCheckpoint;
	private Animator anim;
	private float originaly;

    // Start is called before the first frame update
    void Start()
    {
		anim = GetComponentInChildren<Animator>();
		currCheckpoint = checkpoints[currCheckpointIndex];
		transform.LookAt(currCheckpoint.transform);
    }

    // Update is called once per frame
    void Update()
    {
		SetCheckpoint();
		Vector3 targetPosition = new Vector3(currCheckpoint.transform.position.x, 2.36f, currCheckpoint.transform.position.z);
		transform.LookAt(targetPosition, Vector3.up);
    }

	private void SetCheckpoint()
	{
		float dist = Vector3.Distance(currCheckpoint.transform.position, transform.position);
		if (dist < 5 && currCheckpointIndex < checkpoints.Length -1)
		{
			currCheckpointIndex++;
			currCheckpoint = checkpoints[currCheckpointIndex];
			anim.SetBool("hopping", true);
			//transform.LookAt(currCheckpoint.transform);
		}
	}

	private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "tree_basic")
		{
			transform.LookAt(other.gameObject.transform.position);
			Debug.Log(other.gameObject.transform.position);
			anim.SetBool("hopping", false);
			int explore = Random.Range(1, 3);
			anim.SetInteger("explore", explore);
		}
    }

	private void RenableHopping()
	{
		anim.SetInteger("explore", 0);
		anim.SetBool("hopping", true);
	}
}
