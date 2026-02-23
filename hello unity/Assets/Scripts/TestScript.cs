// 引用Unity引擎的命名空间
using UnityEngine;

// 定义一个名为PlayerController的类，它继承自MonoBehaviour
public class PlayerController : MonoBehaviour
{
    // 公开变量，用于设置玩家的移动速度，可以在Unity编辑器中调整
    public float moveSpeed = 5f;
    // 公开变量，用于设置鼠标的灵敏度，可以在Unity编辑器中调整
    public float mouseSensitivity = 2f;
    // 公开变量，用于引用玩家的摄像机变换组件
    public Transform playerCamera;

    // 私有变量，用于存储摄像机的垂直旋转角度
    private float verticalLookRotation = 0f;

    // Start方法在游戏开始时调用一次
    void Start()
    {
        // 锁定鼠标光标到游戏窗口中心
        Cursor.lockState = CursorLockMode.Locked;
        // 隐藏鼠标光标
        Cursor.visible = false;
    }

    // Update方法每帧调用一次
    void Update()
    {
        // --- 玩家移动 ---
        // 初始化移动输入
        float moveX = 0f;
        float moveZ = 0f;

        // 检测W键是否被按下，用于向前移动
        if (Input.GetKey(KeyCode.W))
        {
            moveZ = 1f;
        }
        // 检测S键是否被按下，用于向后移动
        if (Input.GetKey(KeyCode.S))
        {
            moveZ = -1f;
        }
        // 检测A键是否被按下，用于向左移动
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
        }
        // 检测D键是否被按下，用于向右移动
        if (Input.GetKey(KeyCode.D))
        {
            moveX = 1f;
        }

        // 根据玩家当前的朝向计算移动方向
        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;
        // 使用Translate方法移动玩家，并乘以Time.deltaTime以保证帧率无关的移动
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        // --- 摄像机控制 ---
        // 获取鼠标在X轴上的移动，并乘以灵敏度
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        // 获取鼠标在Y轴上的移动，并乘以灵敏度
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // 根据鼠标X轴的移动来旋转玩家的身体（水平旋转）
        transform.Rotate(Vector3.up * mouseX);

        // 根据鼠标Y轴的移动来计算摄像机的垂直旋转角度
        verticalLookRotation -= mouseY;
        // 使用Mathf.Clamp将垂直旋转角度限制在-90到90度之间，防止摄像机翻转
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);
        // 设置摄像机的本地欧拉角，只改变X轴的旋转（垂直方向）
        playerCamera.localEulerAngles = new Vector3(verticalLookRotation, 0, 0);
    }
}

