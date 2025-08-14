using System.Threading.Tasks;
using UnityEngine;

public class NoticeUIController : MonoBehaviour
{
    [SerializeField] private GameObject loginFailedNoticeBar;
    [SerializeField] private Animator loginFailedNoticenBarAnimator;
    [SerializeField] private GameObject loginSuccessNoticeBar;
    [SerializeField] private Animator loginSuccessNoticeBarAnimator;
    [SerializeField] private GameObject loginProgressNoticeBar;
    [SerializeField] private Animator loginProgressNoticeBarAnimator;
    const string PopInAndOut = "PopInAndOut";
    [SerializeField] private GameObject noticeUICanvasObj;
    private static NoticeUIController singleton;

    void Awake() // HACK: `Missing Reference` in inspector, but it corrently works
    {
        if (singleton != null)
        {
            foreach (Transform child in singleton.transform)
                Destroy(child.gameObject);
            while (transform.childCount > 0)
            {
                Transform childTransform = transform.GetChild(0);
                childTransform.SetParent(singleton.transform, false);
            }
            Destroy(gameObject);
            return;
        }

        singleton = this;
        DontDestroyOnLoad(gameObject);
    }

    private async void PopNotice(GameObject bar, Animator animator)
    {
        if (bar == null || animator == null) return;

        bar.SetActive(true);
        animator.SetTrigger(PopInAndOut);
        await Task.Delay(4000);

        if (bar != null) bar.SetActive(false);
    }

    public void PopLoginProgressNotice() => PopNotice(loginProgressNoticeBar, loginProgressNoticeBarAnimator);
    public void PopLoginFailedNotice() => PopNotice(loginFailedNoticeBar, loginFailedNoticenBarAnimator);
    public void PopLoginSuccessNotice() => PopNotice(loginSuccessNoticeBar, loginSuccessNoticeBarAnimator);
}
