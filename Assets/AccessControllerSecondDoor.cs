using UnityEngine;
using System.Collections;

public class AccessControllerSecondDoor : MonoBehaviour {

    [SerializeField]
    GameObject Success;

    [SerializeField]
    GameObject Error;

    [SerializeField]
    Collider player;

    [SerializeField]
    Animator anim;

    [SerializeField]
    Light bodySensor;

    static bool passedWeight;
    static bool passedHand;
    static bool passedBody;
    static bool isFailed;

    int degree = 0;

    bool notPassedYet;

	// Use this for initialization
	void Start () {
	
        passedWeight = false;
        passedHand = false;
        passedBody = false;
        isFailed = false;
        notPassedYet = true;
	}
	
	// Update is called once per frame
	void Update () {
	
        if (isFailed)
        {
            passedWeight = false;
            passedHand = false;
            passedBody = false;
            StartCoroutine(AccessControl._instance.alarm(true));
        }

        if (!CardRetriever.hasCard && notPassedYet)
        {
            bodySensor.gameObject.SetActive(true);
            AccessControl._instance.sun.gameObject.SetActive(false);
            AccessControl._instance.elum.gameObject.SetActive(false);
            AccessControl._instance.point.gameObject.SetActive(false);
            StartCoroutine(bodySensorActivate());
        }
	}
        
    void OnTriggerEnter(Collider collider){

        if (collider == player && !CardRetriever.hasCard)
        {
            anim.SetBool("stand", true);

            if (SwitchPlayer._instance.weight == SwitchPlayer.EMPLOYEE_WEIGHT)
            {
                StartCoroutine(detect(Error, true));
            }
            else
            {
                StartCoroutine(detect(Success, false));
            }
        }
    }

    void OnTriggerExit(Collider collider){

        if (collider == player && !CardRetriever.hasCard)
        {
            anim.SetBool("stand", false);
            Error.SetActive(true);

            if (!isFailed)
            {
                Success.gameObject.SetActive(true);
            }
        }
    }

    IEnumerator detect(GameObject wObject, bool t){
    
        yield return new WaitForSeconds(1.2f);

        wObject.SetActive(false);
        passedWeight = t;
        isFailed = !t;
    }

    IEnumerator bodySensorActivate(){

        while(degree <= 1800){

            yield return new WaitForSeconds(0.2f);


            bodySensor.transform.Rotate(new Vector3(0, -1, 0));
            bodySensor.spotAngle++;
            bodySensor.spotAngle %= 180;
            degree++;
        }

        bodySensor.gameObject.SetActive(false);

        if (SwitchPlayer._instance.player == SwitchPlayer.BURGLAR)
        {
            isFailed = true;
        }
        else
        {
            passedBody = true;

            AccessControl._instance.sun.gameObject.SetActive(true);
            AccessControl._instance.elum.gameObject.SetActive(true);
            AccessControl._instance.point.gameObject.SetActive(true);
        }
    }
}
