using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    [SerializeField] private LoadingScreen loadingScreen;

    private void Awake()
    {
        LoadGameScene();
    }


    public void LoadGameScene()
    {
        StartCoroutine(AsynkLoader(1));
    }

    private IEnumerator AsynkLoader(int sceneIndex)
    {
        AsyncOperation asynkLoad = SceneManager.LoadSceneAsync(sceneIndex);
        asynkLoad.allowSceneActivation = false;
        while (asynkLoad.isDone)
        {
            yield return null;
        }

        loadingScreen.Hide();
        asynkLoad .allowSceneActivation = true;
    }
}
