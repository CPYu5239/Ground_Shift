using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject cam;
    public static bool is3D;   //是否為3D模式
    public static string nowElement = "Default";
    public PlayerData data;
    public GameObject defaultBall;
    public GameObject waterBall;
    public GameObject fireBall;
    public GameObject iceBall;
    public GameObject earthBall;
    public static GameObject player;

    Animator ani;
    Vector3 originalPos;  //紀錄玩家在3D模式中的Z軸位置

    private void Start()
    {
        ani = GameObject.Find("Camera").GetComponent<Animator>();
        player = GameObject.Find("玩家");
    }

    private void Update()
    {
        //切換視角
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            CameraSwitch();
        }

        #region 電量控制
        if (data.battery <= 0 && is3D)
        {
            CameraSwitch();
        }

        if (is3D)
        {
            data.battery -= Time.deltaTime * 10;
        }
        else if (data.battery <= data.maxBattery)
        {
            data.battery += Time.deltaTime * 20;
        }
        #endregion

        #region 能量控制
        if (data.energy <= 0 && nowElement != "Default")
        {
            ChangeElement("Default");
        }

        if (data.energy > 0 && nowElement != "Default")
        {
            data.energy -= Time.deltaTime * 1.5f;
        }
        #endregion
    }

    /// <summary>
    /// 攝影機視角的切換
    /// </summary>
    private void CameraSwitch()
    {
        if (is3D)
        {
            originalPos = player.transform.position;
            ani.SetTrigger("Switch2D");
            ani.SetBool("is3D", false);
            is3D = false;
        }
        else
        {
            ani.SetTrigger("Switch3D");
            ani.SetBool("is3D", true);
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, originalPos.z);
            is3D = true;
        }
    }

    /// <summary>
    /// 切換屬性
    /// </summary>
    /// <param name="gotoElement">要切換的屬性</param>
    public void ChangeElement(string gotoElement)
    {
        GameObject newBall;

        switch (gotoElement)
        {
            case "Default":
                newBall = Instantiate(defaultBall, player.transform.position, Quaternion.identity);
                Destroy(player);
                player = newBall;
                break;
            case "Water":
                newBall = Instantiate(waterBall, player.transform.position, Quaternion.identity);
                Destroy(player);
                player = newBall;
                break;
            case "Fire":
                newBall = Instantiate(fireBall, player.transform.position, Quaternion.identity);
                Destroy(player);
                player = newBall;
                break;
            case "Ice":
                newBall = Instantiate(iceBall, player.transform.position, Quaternion.identity);
                Destroy(player);
                player = newBall;
                break;
            case "Earth":
                newBall = Instantiate(earthBall, player.transform.position, Quaternion.identity);
                Destroy(player);
                player = newBall;
                break;
            default:
                break;
        }

        nowElement = gotoElement;
        data.energy = data.maxEnergy;
    }
}
