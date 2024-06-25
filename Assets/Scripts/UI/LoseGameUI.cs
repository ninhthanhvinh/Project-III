using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseGameUI : MonoBehaviour
{
    [SerializeField] Button restartButton;
    [SerializeField] Button quitButton;
    [SerializeField] Button mainMenuButton;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(restartButton);
        restartButton.onClick.AddListener(GameManager.instance.RestartGame);
        quitButton.onClick.AddListener(GameManager.instance.QuitGame);
        mainMenuButton.onClick.AddListener(GameManager.instance.MainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
