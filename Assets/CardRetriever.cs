using UnityEngine;
using System.Collections;

public class CardRetriever : MonoBehaviour {

    [SerializeField]
    private Collider player;
    [SerializeField]
    private Light indicator;
    private float period;
    [SerializeField]
    private Animator cardAnimator;

    public static bool hasCard;

    public Color approved;
    public Color stable;

    void Start () {
        period = 0.5f;
        indicator.color = approved;
        hasCard = true;

        StartCoroutine(flash());
    }

    IEnumerator flash()
    {
        while (hasCard)
        {
            indicator.gameObject.SetActive(!indicator.gameObject.active);

            yield return new WaitForSeconds(period);
        }

        yield break;
    }

    IEnumerator reveal()
    {
        yield return new WaitForSeconds(1f);
        SwitchPlayer._instance.employee.gameObject.SetActive(true);
    }

    void OnTriggerStay(Collider collider)
    {        
        if (collider == player && Input.GetKeyDown(KeyCode.E) && AccessControl._instance.doorClosed)
        {
            print("ASD");
            cardAnimator.SetBool("take", true);
            hasCard = false;
            indicator.gameObject.SetActive(true);
            indicator.color = stable;
            StartCoroutine(reveal());
        }
    }
}
