using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float trasitionTime = 1f;
    public GameObject Boss;
    public Player player;

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LastNextLevel()
    {
        StartCoroutine(LastSence(SceneManager.GetActiveScene().buildIndex + 1));
    }
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        
    }
    public void DieActive()
    {
        StartCoroutine(DieEvent(SceneManager.GetActiveScene().buildIndex ));

    }

    IEnumerator DieEvent(int levelIndex)
    {
        player.GetComponent<Animator>().SetTrigger("Die");
        
        yield return new WaitForSeconds(1);

        //Player animation
        transition.SetTrigger("Start");

        //Wait
        yield return new WaitForSeconds(1);

        //Load Scene
        SceneManager.LoadScene(levelIndex);
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        //Player animation
        
        transition.SetTrigger("Start");

        //Wait
        yield return new WaitForSeconds(1);

        //Load Scene
        SceneManager.LoadScene(levelIndex);
    }
    IEnumerator LastSence(int levelIndex)
    {
        //Player animation
        transition.SetTrigger("BossDie_e");

        //Wait
        yield return new WaitForSeconds(1);
        Destroy(Boss);
        

        transition.SetTrigger("BossDie_s");

        yield return new WaitForSeconds(1);
        
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(levelIndex);
    }
    
}
