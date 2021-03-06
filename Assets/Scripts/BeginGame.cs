using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginGame : MonoBehaviour
{
    public void LoadNext()
    {
        SceneManager.LoadScene("Second");
    }
}
