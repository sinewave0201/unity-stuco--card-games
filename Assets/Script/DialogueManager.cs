using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour {
    public GameObject panel;        
    public Image backgroundImage;   // 新增：拖入你的对话框背景图 (Panel 或 Image)
    public Image portraitImage;     
    public TMP_Text nameText;       
    public TMP_Text contentText;    

    private bool isTalking = false;

    void Start() {
        panel.SetActive(false); 
    }

    public IEnumerator TypeEffect(string fullText) {
        contentText.text = ""; 
        foreach (char c in fullText.ToCharArray()) {
            contentText.text += c; 
            yield return new WaitForSeconds(0.05f); 
        }
    }

    public void StartConversation(NPCData info) {
        isTalking = true;
        panel.SetActive(true);
        
        // 更新 UI 内容
        nameText.text = info.npcName;
        portraitImage.sprite = info.portrait;
        portraitImage.preserveAspect = true;

        // --- 核心修改：更换背景颜色 ---
        if (backgroundImage != null) {
            backgroundImage.color = info.bgColor;
        }

        StopAllCoroutines(); 
        StartCoroutine(TypeEffect(info.dialogue));
    }

    public void EndConversation() {
        isTalking = false;
        panel.SetActive(false);
    }
}