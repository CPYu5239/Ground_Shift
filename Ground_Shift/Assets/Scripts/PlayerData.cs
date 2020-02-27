using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "玩家資料", menuName = "遊戲資料/玩家資料")]
public class PlayerData : ScriptableObject
{
    [Header("移動速度"), Range(15,50)]
    public float moveSpeed = 15;
    [Header("血量"), Range(0,500)]
    public int hp = 100;
    [Header("最大血量"), Range(100,500)]
    public int maxHp = 100;
    [Header("攻擊力"), Range(0, 500)]
    public float attack = 30;
    [Header("射擊攻擊力"), Range(0, 500)]
    public float shootAttack = 30;
    [Header("電量"),Range(0, 200)]
    public float battery = 50;
    [Header("最大電量"), Range(50,200)]
    public float maxBattery = 50;
    [Header("能量"), Range(0,100)]
    public float energy = 100;
    [Header("最大能量")]
    public float maxEnergy = 100;
}
