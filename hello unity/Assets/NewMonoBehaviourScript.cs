using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 删除指定的游戏对象
    /// </summary>
    /// <param name="objectToDelete">要删除的游戏对象</param>
    public void DeleteObject(GameObject objectToDelete)
    {
        if (objectToDelete != null)
        {
            Destroy(objectToDelete);
        }
    }
}
