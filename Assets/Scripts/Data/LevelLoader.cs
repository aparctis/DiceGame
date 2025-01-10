using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public UnityAction onLoadingOver;


    [SerializeField] private LoadingScreen loadingScreen;



    private void Awake()
    {
        LoadGameScene();
        loadingScreen.onLoadingScreenHided += () => onLoadingOver?.Invoke();
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
        asynkLoad.allowSceneActivation = true;
    }

    public void FakeLoad(float waitTime)
    {
        StartCoroutine(FakeLoadRutine(waitTime));
    }

    private IEnumerator FakeLoadRutine(float waitTime)
    {
        loadingScreen.Show();
        yield return new WaitForSeconds(loadingScreen.time);

        yield return new WaitForSeconds(waitTime);
        loadingScreen.Hide();
        yield return new WaitForSeconds(loadingScreen.time);


    }
}

