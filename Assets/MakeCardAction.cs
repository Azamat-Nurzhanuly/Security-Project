using UnityEngine;
using System.Collections;

public class MakeCardAction : MonoBehaviour {

    [SerializeField]
    private Collider player;

    [SerializeField]
    private GameObject burglarCard;

    [SerializeField]
    private GameObject employeeCard;

    [SerializeField]
    private GameObject card;

    [SerializeField]
    private Animator cardAnimator;

    [SerializeField]
    private Light indicator;

    [SerializeField]
    private Transform camera;

    void OnTriggerStay(Collider collider)
    {
        if (collider == player && Input.GetKeyDown(KeyCode.E) 
            && (SwitchPlayer._instance.player == SwitchPlayer.BURGLAR || SwitchPlayer._instance.player == SwitchPlayer.EMPLOYEE))
        {
            switch (SwitchPlayer._instance.player)
            {
                case SwitchPlayer.EMPLOYEE:
                    employeeCard.SetActive(true);
                    burglarCard.SetActive(false);
                    break;

                case SwitchPlayer.BURGLAR:
                    employeeCard.SetActive(false);
                    burglarCard.SetActive(true);
                    break;

                default:
                    print("Error in assigning the card's material " + SwitchPlayer._instance.player);
                    break;
            }
                    
            cardAnimator.SetBool("enter", true);
            SwitchPlayer._instance.burglar.gameObject.SetActive(false);
            SwitchPlayer._instance.employee.gameObject.SetActive(false);

            if (SwitchPlayer._instance.player == SwitchPlayer.EMPLOYEE)
            {
                StartCoroutine(accessApproved(true));
            }
            else if (SwitchPlayer._instance.player == SwitchPlayer.BURGLAR)
            {
                StartCoroutine(accessApproved(false));
            }
        }
    }

    IEnumerator accessApproved(bool success) {

        yield return new WaitForSeconds(2.2f);

        if (success)
        {
            AccessControl._instance.changeIndicatorColor(AccessControl.APPROVED);
        }
        else
        {
            AccessControl._instance.changeIndicatorColor(AccessControl.ACCESS_DENIED);
        }
    }
}
