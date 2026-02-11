using UnityEngine;

[System.Serializable] // 确保在编辑器中可见
public class NPCData : MonoBehaviour {
    public string npcName;      // 名字
    public Sprite portrait;     // 立绘
    public Color bgColor = Color.white; // 新增：背景颜色，默认为白色
    
    [TextArea]
    public string dialogue;     // 对话内容
}