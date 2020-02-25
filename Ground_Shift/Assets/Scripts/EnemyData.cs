using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "玩家資料", menuName = "遊戲資料/敵人資料")]
public class EnemyData : ScriptableObject
{
    [Header("移動速度"), Range(15, 50)]
    public float moveSpeed = 15;
    [Header("血量"), Range(0, 50000)]
    public int hp = 100;
    [Header("最大血量"), Range(100, 500)]
    public int maxHp = 100;
    [Header("攻擊冷卻時間"), Range(0, 200)]
    public float attackCD = 5;
    [Header("攻擊距離"), Range(50, 2000)]
    public float attackRange = 50;
}
