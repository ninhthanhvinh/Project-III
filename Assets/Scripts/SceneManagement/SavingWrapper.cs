using System.Collections;
using RPG.Saving;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        [SerializeField] KeyCode saveKey = KeyCode.S;
        [SerializeField] KeyCode loadKey = KeyCode.L;
        [SerializeField] KeyCode deleteKey = KeyCode.Delete;
        const string defaultSaveFile = "save";

        [SerializeField] float fadeInTime = 0.2f;

        //private void Awake()
        //{
        //    StartCoroutine(LoadLastScene());
        //}

        IEnumerator LoadLastScene()
        {
            yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);

            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();

            yield return fader.FadeIn(fadeInTime);
        }

        private void Update()
        {
            if (Input.GetKeyDown(saveKey))
            {
                Save();
            }
            if (Input.GetKeyDown(loadKey))
            {
                Load();
            }
            if (Input.GetKeyDown(deleteKey))
            {
                Delete();
            }
        }

        public void ContinueGame()
        {
            StartCoroutine(LoadLastScene());
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);

        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }

        public void Delete()
        {
            GetComponent<SavingSystem>().Delete(defaultSaveFile);
        }
    }
}