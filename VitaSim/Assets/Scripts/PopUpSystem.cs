using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpSystem : MonoBehaviour
{
    [Header("PopUpParameters")]
    [SerializeField] public GameObject popUpBox;
    [SerializeField] public Animator animator;
    [SerializeField] public TMP_Text popUpText;

    public void PopUp(string text, float duration)
    {
        popUpBox.SetActive(true);
        popUpText.text = text;
        animator.SetTrigger("pop");
        StartCoroutine(Delay(duration));

    }

    public IEnumerator Delay(float duration)
    {
        yield return new WaitForSeconds(duration);
        ClosePopUp();
    }

    public void ClosePopUp()
    {
        popUpBox.SetActive(false);
        popUpText.text = "";
        animator.SetTrigger("close");
    }
}
