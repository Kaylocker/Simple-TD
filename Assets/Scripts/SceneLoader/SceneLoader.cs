using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string _gamescene;
    [SerializeField] private string _loadingScene;

    private readonly LevelNameData _levelNameData = new LevelNameData();

    private void Start()
    {
        if (!string.IsNullOrEmpty(_loadingScene))
        {
            StartCoroutine(AddScene(_loadingScene));
        }

        _loadingScene = _gamescene;
    }

    public void Load()
    {
        if (!string.IsNullOrEmpty(_levelNameData.GetName()))
        {
            StartCoroutine(ScenesController(_levelNameData.GetName()));
        }
    }

    private IEnumerator ScenesController(string sceneName)
    {
        yield return StartCoroutine(AddScene(sceneName));

        if (!string.IsNullOrEmpty(_loadingScene))
        {
            yield return StartCoroutine(RemoveOldscene());
            yield return StartCoroutine(UnloadOldScene());
        }
    }


    private IEnumerator AddScene(string name)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);

        while (!asyncOperation.isDone)
        {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(name));
    }

    private IEnumerator RemoveOldscene()
    {
        AsyncOperation asyncOperation = SceneManager.UnloadSceneAsync(_loadingScene);

        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }

    private IEnumerator UnloadOldScene()
    {
        AsyncOperation asyncOperation = Resources.UnloadUnusedAssets();

        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}
