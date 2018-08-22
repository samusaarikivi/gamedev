using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
/*
* Class which every popup with Confirmation and Cancel buttons inherits from.
* For example Quit game- popup and Login to Google Play Games- popup.
* DOTween tweening library used for smooth transitions.
*/
public class Popup : MonoBehaviour
{
    [SerializeField] RectTransform popUp;
    [SerializeField] GameObject blocker;
    [SerializeField] Button cancelBtn;
    [SerializeField] Button confirmBtn;
    public virtual void Start()
    {
        popUp.gameObject.SetActive(false);
        blocker.SetActive(false);
    }
    public void ShowPopup()
    {
        popUp.gameObject.SetActive(true);
        popUp.localScale = Vector2.zero;
        popUp.DOScale(1, 0.4f).SetEase(Ease.OutCubic).OnComplete(ActivateTwoButtons);
        blocker.SetActive(true);
    }
    public void DismissPopup()
    {
        blocker.SetActive(false);
        DeactivateTwoButtons();
        popUp.transform.DOScale(0, 0.2f).SetEase(Ease.InCubic).OnComplete(() => popUp.gameObject.SetActive(false));
    }
    public void ActivateTwoButtons()
    {
        confirmBtn.interactable = true;
        cancelBtn.interactable = true;
    }
    public void DeactivateTwoButtons()
    {
        confirmBtn.interactable = false;
        cancelBtn.interactable = false;
    }
}