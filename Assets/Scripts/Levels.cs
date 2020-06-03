using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    public int whatLvl;
    public void levels()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + whatLvl);
    }
}
