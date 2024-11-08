using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {   //Sadece bir tane manager olması icin yok ettim skillde engellemesin
        if (instance != null)
            Destroy(instance.gameObject);
        instance = this;
    }

    public void RestartScene()
    {
        Scene scene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(scene.name);

    }
}
