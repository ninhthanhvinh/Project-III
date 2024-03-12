using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotiUI : MonoBehaviour
{
    [SerializeField]
    Text notiText;

    void Start()
    {
        Invoke(nameof(Deactive), 5f);
    }

    public void ShowNoti(string message)
    {
        notiText.text = message;
    }

    private IEnumerator Deactive()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}
