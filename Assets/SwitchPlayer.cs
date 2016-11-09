using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SwitchPlayer : MonoBehaviour {

    public static SwitchPlayer _instance = null;

    public int player;

    public const int EMPLOYEE = 1;
    public const int BURGLAR = 2;

    [SerializeField]
    private RawImage burglar;

    [SerializeField]
    private RawImage employee;

    void Start()
    {
        _instance = this;
    }

	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            employee.gameObject.SetActive(true);
            burglar.gameObject.SetActive(false);

            player = EMPLOYEE;
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            employee.gameObject.SetActive(false);
            burglar.gameObject.SetActive(true);

            player = BURGLAR;
        }
	}
}
