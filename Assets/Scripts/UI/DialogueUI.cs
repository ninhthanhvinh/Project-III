using Dialogue;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class DialogueUI : MonoBehaviour
    {
        PlayerConversant playerConversant;
        [SerializeField] TextMeshProUGUI conversantName;
        [SerializeField] TextMeshProUGUI AIText;
        [SerializeField] Button nextButton;
        [SerializeField] Button quitButton;
        [SerializeField] GameObject AIResponse;
        [SerializeField] Transform choiceRoot;
        [SerializeField] GameObject choicePrefab;
             
        // Start is called before the first frame update
        void Start()
        {
            playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
            nextButton.onClick.AddListener(() => playerConversant.Next());
            quitButton.onClick.AddListener(() => playerConversant.Quit());
            quitButton.onClick.AddListener(() => Cursor.visible = false);
            playerConversant.OnConversationUpdated += UpdateUI;
            UpdateUI();
        }

        // Update is called once per frame
        void Update()
        {
            if (gameObject.activeSelf && Input.GetKeyDown(KeyCode.Escape))
            {
                playerConversant.Quit();
            }

            if (gameObject.activeSelf)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        void UpdateUI()
        {
            gameObject.SetActive(playerConversant.IsActive());
            if (!playerConversant.IsActive())
            {
                return;
            }

            conversantName.text = playerConversant.GetCurrentConversantName();
            AIResponse.SetActive(!playerConversant.IsChoosing());
            choiceRoot.gameObject.SetActive(playerConversant.IsChoosing());
            if (playerConversant.IsChoosing())
            {
                BuildChoiceList();
            }
            else
            {
            AIText.text = playerConversant.GetText();
            nextButton.gameObject.SetActive(playerConversant.HasNext());
            }
        }

        private void BuildChoiceList()
        {
            choiceRoot.DetachChildren();
            foreach (DialogueNode choice in playerConversant.GetChoices())
            {
                GameObject choiceInstance = Instantiate(choicePrefab, choiceRoot);
                var textComp = choiceInstance.GetComponentInChildren<TextMeshProUGUI>();
                textComp.text = choice.GetText();
                Button button = choiceInstance.GetComponentInChildren<Button>();
                button.onClick.AddListener(() =>
                {
                    playerConversant.SelectChoice(choice);
                    UpdateUI();
                });
            }
        }
    }
}


