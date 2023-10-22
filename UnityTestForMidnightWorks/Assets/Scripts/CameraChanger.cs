using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    public Camera firstPersonCamera;
    public Camera thirdPersonCamera;
    public GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        firstPersonCamera.enabled = true;
        thirdPersonCamera.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            player.GetComponent<CameraSpectator>().enabled = !player.GetComponent<CameraSpectator>().enabled;
            firstPersonCamera.enabled = !firstPersonCamera.enabled;
            thirdPersonCamera.enabled = !thirdPersonCamera.enabled;
        }
    }
}