using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    AsyncOperation async;
    float delayTime = 0.0f;
    public Image loadingBar;

    void Start()
    {
        StartCoroutine(LoadingNextScene(GameManager.Instance.nextSceneName));
    }

    void Update()
    {
        if(async != null) loadingBar.fillAmount = delayTime / 3.0f;
        delayTime += Time.deltaTime;
    }

    IEnumerator LoadingNextScene(string nextSceneName)
    {
        async = SceneManager.LoadSceneAsync(nextSceneName);
        async.allowSceneActivation = false;

        while (async.progress < 0.9f) // async.isDone 도 있는데 체크시점이 애매함
        {
            loadingBar.fillAmount = async.progress;
            yield return true;
        }

        while (async.progress >= 0.9f)
        {
            yield return new WaitForSeconds(0.1f);
            if (delayTime > 3.0f)
                break;
        }

        async.allowSceneActivation = true;
    }


}
