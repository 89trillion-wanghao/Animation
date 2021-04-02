using UnityEngine;
/// <summary>
/// 动画事件监听
/// </summary>
public class AnimationEvents : MonoBehaviour
{
    /// <summary>
    /// 开箱动画结束的监听事件
    /// </summary>
    public void OpenAnimationEnd()
    {
        GameManager.Instance.AnimationEvent();
    }
}
