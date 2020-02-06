using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //variables
    public float Health;
    public float MaxHealth;
    //waktu dimana musuh jadi ga bisa diserang
    //infinite time
    public float InvTime = 0;

    private void Update()
    {
        if (InvTime > 0)
        {
            InvTime -= Time.deltaTime;
        }
    }

}
