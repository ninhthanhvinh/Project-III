using RPG.SceneManagement;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] GameObject loseGameUI;
    [SerializeField] private GameObject loadingScreen;
    public UnityEvent OnLose;
    private void Awake()
    {
        if (instance == null) 
        { 
            instance = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(this);

        OnLose.AddListener(Pause);
        OnLose.AddListener(ShowLoseUI);
    }

    private void ShowLoseUI()
    {
        Instantiate(loseGameUI);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ContinueGame()
    {
        FindObjectOfType<SavingWrapper>().ContinueGame();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Resume();
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadScene(int index)
    {
        StartCoroutine(LoadSceneAsync(index));
    }

    private IEnumerator LoadSceneAsync(int index)
    {
        Debug.Log("Loading Scene: " + index);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index);
        GameObject screen = Instantiate(loadingScreen);
        Image loadingFillBar = screen.GetComponentsInChildren<Image>()[1];
        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            loadingFillBar.fillAmount = progress;
            yield return null;
        }
       Destroy(screen);
    }
}
