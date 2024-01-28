using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering.Universal; // 引入2D光源命名空间

public class RotateLightColor : MonoBehaviour
{
    public float rotationSpeed = 1.0f; // 颜色旋转速度
    private Light2D light2D; // 光源组件
    private bool rotateOn;

    void Start()
    {
        // 获取Light2D组件
        light2D = GetComponent<Light2D>();
    }

    void Update()
    {
        if (!rotateOn) return;

        if (light2D != null)
        {
            // 获取当前颜色的HSV值
            Color.RGBToHSV(light2D.color, out float H, out float S, out float V);

            // 更新色相值，实现旋转效果
            H += rotationSpeed * Time.deltaTime;

            // 确保色相值在0到1的范围内
            if (H > 1) H -= 1;

            // 将更新后的HSV值转换回RGB颜色，并应用到光源
            light2D.color = Color.HSVToRGB(H, S, V);
        }
    }
    [Button]
    public void StartRotate()
    {
        rotateOn = true;
    }
    [Button]
    public void EndRotate()
    {
        rotateOn = false;
    }
}

