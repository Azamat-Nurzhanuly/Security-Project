using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SwitchPlayer : MonoBehaviour {

    public static SwitchPlayer _instance = null;

    public int player;

    public const int EMPLOYEE = 1;
    public const int BURGLAR = 2;

    public const float EMPLOYEE_WEIGHT = 80.5f;
    public const float BURGLAR_WEIGHT = 100.10f;

    public float weight;

    public RawImage burglar;
    public RawImage employee;

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
            weight = EMPLOYEE_WEIGHT;
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            employee.gameObject.SetActive(false);
            burglar.gameObject.SetActive(true);

            weight = BURGLAR_WEIGHT;

            player = BURGLAR;
        }
	}
}
