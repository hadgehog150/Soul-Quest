using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator anim1, anim2, anim3, anim4;
    public GameObject settingPanel;

    private void Start()
    {
        Cursor.visible = true;
    }

    public void NewGame() {
        anim1.SetBool("isClick", true);
        PlayerPrefs.SetInt("PositionPlayer", 0);
        SceneManager.LoadScene(1);
    }

    public void Settings()
    {
        anim2.SetBool("isClick", true);
        settingPanel.SetActive(true);
    }

    public void SettingsExit()
    {
        anim3.SetBool("isClick", false);
        anim2.SetBool("isClick", false);
        anim3.SetBool("isClick", false);
        settingPanel.SetActive(false);
    }


    public void Continue()
    {
        anim3.SetBool("isClick", true);
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        anim4.SetBool("isClick", true);
        Application.Quit();
    }
}
