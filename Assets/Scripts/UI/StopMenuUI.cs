using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopMenuUI : MonoBehaviour
{
    [SerializeField] Button resumeButton;
    [SerializeField] Button quitButton;
    private void OnEnable()
    {
        GameManager.instance.Pause();
    }

    // Start is called before the first frame update
    void Start()
    {
        resumeButton.onClick.AddListener(GameManager.instance.Resume);
        quitButton.onClick.AddListener(GameManager.instance.QuitGame);
    }

    private void OnDisable()
    {
        GameManager.instance.Resume();
    }
}
