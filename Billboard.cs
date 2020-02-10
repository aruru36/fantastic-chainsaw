using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour

/// <summary>
/// 常にカメラの方を向くオブジェクト回転をカメラに固定
/// </summary>

{
    public int _baseScale = 1;

    void LateUpdate()
    {
        // 回転をカメラと同期させる
        transform.rotation = Camera.main.transform.rotation;
        //transform.localScale = Vector3.one * _baseScale * GetDistance();
    }
    private float GetDistance()
    {
        return (transform.position - Camera.main.transform.position).magnitude;
    }

    
}

