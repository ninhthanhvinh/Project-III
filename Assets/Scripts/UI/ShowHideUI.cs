using Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace InventoryExample.UI
{
    public class ShowHideUI : MonoBehaviour
    {

        [SerializeField] KeyCode toggleKey = KeyCode.Escape;
        [SerializeField] GameObject uiContainer = null;

        PlayerController player;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }

        // Start is called before the first frame update
        void Start()
        {
            uiContainer.SetActive(false);
            Cursor.visible = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(toggleKey))
            {
                Toggle();
            }
            if (Input.GetKeyDown(KeyCode.Escape) && uiContainer.activeSelf)
            {
                Toggle();
            }
        }

        public void Toggle()
        {
            uiContainer.SetActive(!uiContainer.activeSelf);
            if(uiContainer.activeSelf)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.visible = false;
            }
            player.CanAttack = !uiContainer.activeSelf;
        }
    }
}