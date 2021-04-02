using UnityEngine;
using UnityEngine.UI;

public class ScreenAdapt : MonoBehaviour
{
    public CanvasScaler canvasScalerTemp;
    void Awake () 
    {
        float standardWidth = 1080f;           //初始标准宽度
        float standardHeight = 1920f;          //初始标准高度
        float deviceWidth = 0f;                //当前设备宽度
        float deviceHeight = 0f;               //当前设备高度
        
        float adjustor = 0f;                   //屏幕矫正系数
        
        // 获取当前设备的宽高
        deviceWidth = Screen.width;
        deviceHeight = Screen.height;
        
        // 计算宽高比例
        float standardAspect = standardWidth / standardHeight;
        float deviceAspect = deviceWidth / deviceHeight;
        
        // 计算矫正系数
        if (deviceAspect < standardAspect)
        {
            adjustor = standardAspect / deviceAspect;
        }
        
        
        // 判断是否采用高度适配还是宽度适配
        if (adjustor == 0)
        {
            canvasScalerTemp.matchWidthOrHeight = 1;
        }
        else
        {
            canvasScalerTemp.matchWidthOrHeight = 0;
        }
    }
}
