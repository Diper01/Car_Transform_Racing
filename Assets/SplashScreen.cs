using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        //StartCoroutine(LoadGame());
        //ApplicationChrome.statusBarState = ApplicationChrome.navigationBarState = ApplicationChrome.States.TranslucentOverContent;
        SceneManager.LoadScene(1);
    }

    //IEnumerator LoadGame()
    //{
    //    yield return new WaitForSeconds(2);
    //    SceneManager.LoadScene(1);
    //}
}
