using UnityEngine;
using System.Collections;

public class FirstDoorCheck : MonoBehaviour {

    private Animator animator = null;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
    void OnTriggerEnter (Collider collider)
    {
        animator.SetTrigger("Open");
    }

    void OnTriggerExit (Collider collider)
    {
        animator.ResetTrigger("Open");
    }
}
