using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            StartCoroutine(FadeOutIn());
        }

        IEnumerator FadeOutIn()
        {
            yield return FadeOut(3f);
            yield return FadeIn(1f);
        }

        public void FadeOutImmediate()
        {
            if (canvasGroup == null)
            {
                return;
            }
            //Here is the problem when canvasGroup is null
            canvasGroup.alpha = 1;
        }

        public IEnumerator FadeOut(float time)
        {
            if (canvasGroup.alpha == 0)
                canvasGroup.GetComponent<CanvasScaler>().scaleFactor = 1;
            while (canvasGroup.alpha < 1)
            {
                canvasGroup.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                canvasGroup.alpha += Time.deltaTime / time;
                yield return null;
            }
        }

        public IEnumerator FadeIn(float time)
        {
            if (canvasGroup.alpha == 0)
                canvasGroup.GetComponent<CanvasScaler>().scaleFactor = 0;
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime / time;
                yield return null;
            }
            
        }
    }

}
