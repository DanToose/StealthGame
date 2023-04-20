using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DelayedSwitch", 0.1f);
    }

    void DelayedSwitch()
    {
        // This returns the guard to its patrol.
        SceneManager.LoadScene(1);
    }
}
