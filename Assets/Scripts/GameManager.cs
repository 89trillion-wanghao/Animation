using System.Collections;
using DG.Tweening;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 流程控制
/// </summary>
public class GameManager : MonoBehaviour
{
    public RectTransform boxPanel;
    public RectTransform coinImgPos;
    public GameObject particleGo; 
    public Text coinText;
    
    public static GameManager Instance = null;      // 单例
    private GameObject coinGo;                      // 金币物体
    private int showCoinNum;                        // 需要显示金币的数量
    private int changeCoinNum;                      // 需要改变的金币数量
    
    /// <summary>
    /// 游戏入口
    /// </summary>
    private void Start()
    {
        Instance = this;
    }
    
    /// <summary>
    /// 购买按钮点击事件
    /// </summary>
    /// <param name="num">金币数量</param>
    public void BtnClick(int num)
    {
        if (num > 15)
        {
            showCoinNum = 15;
            changeCoinNum = num/15;
        }
        else
        {
            showCoinNum = num;
            changeCoinNum = 1;
        }
        Animator boxAnimator = Resources.Load<Animator>(Constant.BOXANIMATOR_PATH);
        Instantiate(boxAnimator.gameObject,boxPanel);
        boxAnimator.SetTrigger(Constant.BOXJUMP_PARAMETERS);
        coinGo = Resources.Load<GameObject>(Constant.COIN_PATH);
    }

    /// <summary>
    /// 动画结束事件
    /// </summary>
    public void AnimationEvent()
    {
        particleGo.SetActive(true);
        StartCoroutine(Init(showCoinNum));
    }
    
    /// <summary>
    /// 利用协程对金币进行初始化
    /// </summary>
    /// <param name="num">初始化的数量</param>
    /// <returns>等待时间</returns>
    IEnumerator Init(int num)
    {
        for(int i=0;i<num;i++)
        {
            
            GameObject tempGameObject = Instantiate(coinGo, this.transform);
            tempGameObject.transform.position = CalculatePos(this.transform);

            tempGameObject.transform.DOScale(new Vector3(0.4f, 0.4f, 0.4f), 1f)
                .OnComplete(() =>
                {
                    coinText.text = CalculateCoinNum();
                tempGameObject.transform.DOMove(new Vector3(coinImgPos.position.x, coinImgPos.position.y, 0), 0.4f)
                    .OnComplete(() =>
                    {
                        Destroy(tempGameObject);
                    });
            });
            
            yield return new WaitForSeconds(0.1f);
        }
        particleGo.SetActive(false);
    }
    
    /// <summary>
    /// 计算金币的初始位置
    /// </summary>
    /// <param name="tf">基础位置信息</param>
    /// <returns>位置数组</returns>
    private Vector3 CalculatePos(Transform tf)
    {
        float randomX=Random.Range(-0.1f, 0.1f);
        float randomY=Random.Range(-0.05f, 0.05f);
        randomX = Mathf.Round(randomX * 100) / 100;
        randomY = Mathf.Round(randomY * 100) / 100;
        return new Vector3(tf.transform.position.x + randomX,tf.transform.position.y+randomY,tf.transform.position.z);
    }
    
    /// <summary>
    /// 计算金币文本需要显示的数量
    /// </summary>
    /// <returns>金币数量文本的字符串</returns>
    private string CalculateCoinNum()
    {
        return (int.Parse(coinText.text)+changeCoinNum).ToString();
    }
    
}
