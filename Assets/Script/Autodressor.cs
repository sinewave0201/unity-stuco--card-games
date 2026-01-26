using UnityEngine;
using System.Collections.Generic;

public class AutoDresser : MonoBehaviour
{
    // 这里填你的“身体”模型（带SkinnedMeshRenderer的那个，通常叫Body或者LOD0）
    public SkinnedMeshRenderer targetBody;

    void Start()
    {
        // 1. 获取所有的骨骼信息（字典方便查找）
        Dictionary<string, Transform> boneMap = new Dictionary<string, Transform>();
        foreach (Transform bone in targetBody.bones)
        {
            if(!boneMap.ContainsKey(bone.name)) boneMap.Add(bone.name, bone);
        }

        // 2. 找到所有作为子物体的衣服
        SkinnedMeshRenderer[] clothes = GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (SkinnedMeshRenderer cloth in clothes)
        {
            if (cloth == targetBody) continue; // 跳过身体自己

            // 3. 重新分配骨骼
            Transform[] newBones = new Transform[cloth.bones.Length];
            for (int i = 0; i < cloth.bones.Length; i++)
            {
                // 如果衣服的这根骨头，身体里也有同名的，就用身体的
                if (cloth.bones[i] != null && boneMap.TryGetValue(cloth.bones[i].name, out Transform matchingBone))
                {
                    newBones[i] = matchingBone;
                }
            }
            
            // 4. 应用修改
            cloth.bones = newBones;
            cloth.rootBone = targetBody.rootBone; // 顺便把你刚才手动改的这个也自动修正
            
            Debug.Log($"已自动缝合: {cloth.name}");
        }
    }
}