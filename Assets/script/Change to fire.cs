using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Changetofire : MonoBehaviour
{
    public void ChangefiresceneBtn()
    {
        SceneManager.LoadScene("FireScene 1");
    }
}
