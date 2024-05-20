using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZamanGeri : MonoBehaviour {

	public bool isRewinding = false;
	public string KeyTrigger;
	public bool UseInputTrigger;
	private bool hasAnimator = false;
	private bool hasRb = false;
	public Animator animator;
	public float geriAlSeconds = 5;
    private LinkedList<PointInTime> PointsInTime;
	void Start () {

		if (GetComponent<Animator> ()) {
			hasAnimator = true;
			animator = GetComponent<Animator> ();
		}

		if (GetComponent<Rigidbody> ())
			hasRb = true;


	}

	void Update () {
		if (UseInputTrigger)
			if (Input.GetKey (KeyTrigger))
				StartRewind ();

	}

	void FixedUpdate(){
		ChangeTimeScale (geriAlSeconds);
		if (isRewinding) {
			Rewind ();
		}else{
			Time.timeScale = 1f;
		}
	}

	void Rewind(){
		if (PointsInTime.Count > 0 ) {
			PointInTime PointInTime = PointsInTime.First.Value;
			transform.position = PointInTime.position;
			transform.rotation = PointInTime.rotation;
            PointsInTime.RemoveFirst();
		}
	}


	void StartRewind(){
		isRewinding = true;
		if(hasAnimator)
			animator.enabled = false;

		if (hasRb)
			GetComponent<Rigidbody> ().isKinematic = true;
	}



	void ChangeTimeScale(float speed){
		Time.timeScale = speed;
		if (speed > 1)
			Time.fixedDeltaTime = 0.02f / speed;
		else
			Time.fixedDeltaTime = speed * 0.02f;
	}



}

