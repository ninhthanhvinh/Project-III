using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameScene : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button quitButton;
    // Start is called before the first frame update
    void Start() { 


        GameManager gminstance = FindObjectOfType<GameManager>();
        startButton.onClick.AddListener(gminstance.StartGame);
        continueButton.onClick.AddListener(gminstance.ContinueGame);
        quitButton.onClick.AddListener(gminstance.QuitGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
