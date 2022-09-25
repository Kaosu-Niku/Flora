using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OtherUI : MonoBehaviour
{
    [SerializeField] GameObject LoadingBack;//* 加載畫面
    [SerializeField] Slider LoadingSlider;//* 加載進度條
    [SerializeField] GameObject SaveImage;//* 存檔圖案
    private void OnEnable()
    {
        AllEventSO.LoadSceneEvent += ToLoadScene;
        AllEventSO.LoadSceneAsyncEvent += ToLoadSceneAsync;
        UiSystemSO.SaveImageAction += UseSaveImage;
    }
    private void OnDisable()
    {
        AllEventSO.LoadSceneEvent -= ToLoadScene;
        AllEventSO.LoadSceneAsyncEvent -= ToLoadSceneAsync;
        UiSystemSO.SaveImageAction -= UseSaveImage;
    }
    private void ToLoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    private void ToLoadSceneAsync(string sceneName)
    {
        StartCoroutine(ToLoadSceneAsyncIEnum(sceneName));
    }
    private void UseSaveImage(bool b)
    {
        SaveImage.SetActive(b);
    }
    private IEnumerator ToLoadSceneAsyncIEnum(string sceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;//? 使場景不會進行自動跳轉，isDone加載到0.9會停止，直到該屬性為true時才加載最後0.1
        if (LoadingBack)
            LoadingBack.SetActive(true);
        if (LoadingSlider)
        {
            LoadingSlider.gameObject.SetActive(true);
            LoadingSlider.maxValue = 1;
        }
        while (async.progress < 0.9f)
        {
            if (LoadingSlider)
                LoadingSlider.value = async.progress;//? 加載進度           
            yield return 0;
        }
        LoadingSlider.value = 1;
        yield return new WaitForSecondsRealtime(1);
        async.allowSceneActivation = true;
        yield return 0;
    }
}
