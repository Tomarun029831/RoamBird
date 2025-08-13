using System.Threading.Tasks;
using UnityEngine;

public class NoticeUIController : MonoBehaviour
{
    [SerializeField] private GameObject loginFailedNoticeBar;
    [SerializeField] private Animator loginFailedNotionBarAnimator;
    [SerializeField] private GameObject loginSuccessNoticeBar;
    [SerializeField] private Animator loginSuccessNoticeBarAnimator;
    [SerializeField] private GameObject loginProgressNoticeBar;
    [SerializeField] private Animator loginProgressNoticeBarAnimator;
    const string PopInAndOut = "PopInAndOut";

    public async void PopLoginProgressNotice()
    {
        loginProgressNoticeBar.SetActive(true);
        loginProgressNoticeBarAnimator.SetTrigger(PopInAndOut);
        await Task.Delay(4000);
        loginProgressNoticeBar.SetActive(false);
    }

    public async void PopLoginFailedNotice()
    {
        loginFailedNoticeBar.SetActive(true);
        loginFailedNotionBarAnimator.SetTrigger(PopInAndOut);
        await Task.Delay(4000);
        loginFailedNoticeBar.SetActive(false);
    }

    public async void PopLoginSuccessNotice()
    {
        loginSuccessNoticeBar.SetActive(true);
        loginSuccessNoticeBarAnimator.SetTrigger(PopInAndOut);
        await Task.Delay(4000);
        loginSuccessNoticeBar.SetActive(false);
    }
}
