using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
/*
* Class for buttons that open Google Play Games- leaderboard.
* Opens a login confirmation popup, if user is not authenticated.
* DOTween tweening library used for smooth transitions.
*/
public class LeadeboardButtonBehaviour<T>  : SingletonBehaviour<T> where T : MonoBehaviour {
    public Button btn;
    public LoginPopup loginPopup;
    public float loginCallbackMessageEndPositionY = 67f;
    public RectTransform loginMessageRt;
    public TextMeshProUGUI loginSuccessCallbackText;
    public Color successGreen;
    public Color failRed;

    public bool startScreenLeaderboard;
    public virtual void Start () {
        btn.onClick.AddListener(LeaderboardButtonAction);
    }

    public virtual void LeaderboardButtonAction()
    {
        if (Social.localUser.authenticated)
        {
            if(!startScreenLeaderboard)
            {
                if (GameMode.IsClassic())
                    ServiceManager.Singleton.ShowDistanceLeaderboard();
                else
                    ServiceManager.Singleton.ShowSprintLeaderboard();
            } else
            {
                ServiceManager.Singleton.ShowLeaderboards();
            }
        }
        else
            loginPopup.ShowPopup();
    }
    public void LoginSuccesful(bool success)
    {
        if (success)
        {
            loginSuccessCallbackText.text = "Successfully logged in";
            loginSuccessCallbackText.color = successGreen;
        }
        else
        {
            loginSuccessCallbackText.text = "Login failed";
            loginSuccessCallbackText.color = failRed;
        }
        RevealLoginMessage();
    }
    public void RevealLoginMessage()
    {
        loginMessageRt.localScale = Vector2.zero;
        loginMessageRt.DOAnchorPosY(loginCallbackMessageEndPositionY, 0.35f).SetEase(Ease.OutCubic).OnComplete(HideLoginMessage);
        loginMessageRt.DOScale(1f, 0.3f).SetEase(Ease.OutCubic);
    }
    public void HideLoginMessage()
    {
        loginMessageRt.DOAnchorPosY(0f, 0.2f).SetEase(Ease.InCubic).SetDelay(1.5f);
        loginMessageRt.DOScale(0f, 0.2f).SetEase(Ease.InCubic).SetDelay(1.5f);
    }
}
