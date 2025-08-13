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
    [SerializeField] private GameObject noticeUICanvasObj;
    private static NoticeUIController singleton;

    void Awake()
    {
        if (singleton != null)
        {
            Destroy(gameObject);
            Destroy(noticeUICanvasObj);
            return;
        }
        singleton = this;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(noticeUICanvasObj);
    }

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
