using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "玩家資料", menuName = "遊戲資料/敵人資料")]
public class EnemyData : ScriptableObject
{
    [Header("移動速度"), Range(0, 50)]
    public float moveSpeed = 15;
    [Header("血量"), Range(0, 50000)]
    public int hp = 100;
    [Header("最大血量"), Range(100, 500)]
    public int maxHp = 100;
    [Header("攻擊力"), Range(100, 500)]
    public float attack = 15;
    [Header("攻擊冷卻時間"), Range(0, 200)]
    public float attackCD = 2.5f;
    [Header("攻擊距離"), Range(0, 2000)]
    public float attackRange = 15;
    [Header("攻擊延遲"), Range(0, 2000)]
    public float attackDelay = 0.5f;
    [Header("感應距離"), Range(0, 2000)]
    public float detectRange = 8;
}
