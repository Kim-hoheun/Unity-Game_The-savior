using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MoveMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void GameExit()
    {
        Application.Quit();
    }
    public void gameStart()
    {
        SceneManager.LoadScene(1);
    }
    public void explain()
    {
        SceneManager.LoadScene(10);
    }
}
