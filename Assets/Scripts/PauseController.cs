using UnityEngine;

public class PauseController : MonoBehaviour {

    public Camera pauseCam;
    public GameObject playCam;

    void Start()
    {
        pauseCam.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (pauseCam.enabled == false)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                pauseCam.enabled = true;
                playCam.SetActive(false);
            }
            else
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = false;
                pauseCam.enabled = false;
                playCam.SetActive(true);
            }
        }
    }
}
