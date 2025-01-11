using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public event UnityAction onLoadingOver;
    [SerializeField] private LoadingScreen loadingScreen;

    private void Awake()
    {
        LoadGameScene();
        onLoadingOver += () => Debug.Log("onLoadingOver");
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

        loadingScreen.Hide(onLoadingOver);
        asynkLoad.allowSceneActivation = true;
    }

    public void FakeLoad(float waitTime, UnityAction callBack)
    {
        StartCoroutine(FakeLoadRutine(waitTime, callBack));
    }

    private IEnumerator FakeLoadRutine(float waitTime, UnityAction callBack)
    {
        loadingScreen.Show();
        yield return new WaitForSeconds(loadingScreen.time+ waitTime);
        loadingScreen.Hide(callBack);
    }
}

