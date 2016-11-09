using UnityEngine;
using System.Collections;

public class AccessControl : MonoBehaviour {

    public static AccessControl _instance = null;
    private int entered;
    private float period;

    public const int IN_THE_AREA = 1;
    public const int OUT_OF_THE_AREA = 2;
    public const int ACCESS_DENIED = 3;
    public const int APPROVED = 4;

    [SerializeField]
    private Light indicator;

    [SerializeField]
    private Light sun;

    [SerializeField]
    private Light elum;

    [SerializeField]
    private Collider player;

    [SerializeField]
    private Animator doorAnimator;

    public Color accessDenied;
    public Color approved;
    public Color stable;

    // Use this for initialization
    void Start () {
        entered = OUT_OF_THE_AREA;
        period = 0.5f;
        indicator.color = stable;

        _instance = this;
    }

    IEnumerator flash()
    {
        while (entered == IN_THE_AREA || entered == ACCESS_DENIED)
        {
            indicator.gameObject.SetActive(!indicator.gameObject.active);

            yield return new WaitForSeconds(period);
        }

        yield break;
    }

    void OnTriggerEnter (Collider collider)
    {
        if (collider == player)
        {
            if (entered != APPROVED && entered != ACCESS_DENIED)
            {
                entered = IN_THE_AREA;

                StartCoroutine(flash());
            }

            changeIndicatorColor(entered);
        }
    }

    void OnTriggerExit (Collider collider)
    {
        if (collider == player)
        {
            if (entered != ACCESS_DENIED && entered != APPROVED)
            {
                entered = OUT_OF_THE_AREA;
            }

            indicator.gameObject.SetActive(true);
            changeIndicatorColor(entered);
        }
    }

    public void changeIndicatorColor(int status)
    {
        if (entered != status)
            entered = status;
        
        switch (status)
        {
            case IN_THE_AREA:
                indicator.color = accessDenied;
                break;
            
            case OUT_OF_THE_AREA:
                indicator.color = stable;
                break;

            case APPROVED:
                indicator.color = approved;
                indicator.gameObject.SetActive(true);
                doorAnimator.SetBool("Open", true);
                StartCoroutine(closeTheDoor());
                break;

            case ACCESS_DENIED:
                indicator.color = accessDenied;
                period = 0.3f;
                indicator.range = 10f;
                sun.gameObject.SetActive(false);
                elum.gameObject.SetActive(false);
                break;
            
            default:
                print("Error assigning indicator's color due absence of color");
                break;
        }
    }

    IEnumerator closeTheDoor()
    {
        yield return new WaitForSeconds(3.5f);
        doorAnimator.SetBool("Open", false);
    }
}
