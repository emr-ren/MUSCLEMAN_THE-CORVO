using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private string sceneName = "TheCorvoDemo";
    [SerializeField] private GameObject continueButton;
    [SerializeField] FadeScreenUI fadeScreen;

    public void ContunieGame()
    {
        StartCoroutine(LoadSceneWithFadeEffect(1.5f));
    }

    public void NewGame()
    {
        
        SceneManager.LoadScene(sceneName);
        
    }

    public void ExitGame()
    {
        Application.Quit();

        // Sadece Unity Editor'de çalışırken durdurmak için
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    IEnumerator LoadSceneWithFadeEffect(float _delay)
    {
        fadeScreen.FadeToOut();

        yield return new WaitForSeconds(_delay);

        SceneManager.LoadScene(sceneName);
    
    }
}