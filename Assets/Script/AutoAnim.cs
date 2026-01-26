using UnityEngine;

public class AutoAnim : MonoBehaviour
{
    private Animator anim;
    private Vector3 lastPosition;
    
    // 防抖计时器
    private float stopTimer = 0f;

    void Start()
    {
        anim = GetComponent<Animator>();
        lastPosition = transform.position;
    }

    void Update()
    {
        // 1. 计算水平移动距离 (忽略上下抖动)
        Vector3 currentPos = transform.position;
        // 只看 X 和 Z 轴的移动，不看 Y 轴 (防止跳跃或地面不平时误判)
        float distance = Vector2.Distance(new Vector2(currentPos.x, currentPos.z), new Vector2(lastPosition.x, lastPosition.z));
        
        float speed = distance / Time.deltaTime;

        // 2. 核心逻辑：带缓冲的判断
        bool isMoving = anim.GetBool("IsWalking");

        if (speed > 0.05f) // 只要有一点点速度
        {
            // 只要在动，立刻切换成走路
            isMoving = true;
            stopTimer = 0f; // 重置计时器
        }
        else
        {
            // 如果速度为0了，先别急，开始计时
            stopTimer += Time.deltaTime;

            // 只有当持续停止超过 0.15 秒，才真的认为停下了
            if (stopTimer > 0.15f)
            {
                isMoving = false;
            }
        }

        // 3. 应用结果
        anim.SetBool("IsWalking", isMoving);

        // 4. 更新位置
        lastPosition = transform.position;
    }
}