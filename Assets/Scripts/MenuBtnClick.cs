using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBtnClick : MonoBehaviour
{
    public void StartBtnClick() {
        SceneManager.LoadScene("Game");
    }
    public void EndBtnClick() {
        Application.Quit();
    }
}
