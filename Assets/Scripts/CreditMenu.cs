using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void Back()
    {
        SceneManager.LoadScene("Main_Menu");
    } 
}
