using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicToggle : MonoBehaviour
{
    public CharacterController playerControl;
    public Camera cinematicCam;
    public Camera playerCam;

    public void ToggleCinemaMode()
    {
        cinematicCam.enabled = !cinematicCam.enabled;
        playerCam.enabled = !playerCam.enabled;
        playerControl.enabled = !playerControl.enabled;
    }
}
