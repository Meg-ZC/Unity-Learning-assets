using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script handle all the control code, so detecting when the users click on a unit or building and selecting those
/// If a unit is selected it will give the order to go to the clicked point or building when right clicking.
/// </summary>
public class UserControl : MonoBehaviour
{
    public GameObject playerCar;
    public GameObject headOfCar;
    public GameObject rotateRelateTo;
    public GameObject bullets;
    public GameObject shootPlace;
    public Camera[] GameCamera;
    public Text texta;
    public float PanSpeed = 10.0f;/// <summary>
    private float yMouse;
    private float xMouse;
    private float sensity = 10;/// <summary>
    public bool alive = false;//进入游戏后设置为true
    // private Unit m_Selected = null;
    // public GameObject Marker;
    public class BuffTime
    {
        public bool buffAvailableToUse;
        public float intervalTime;
        public float realPassTime;
    }

    // enum MyEnum
    // {
    //     
    // }

    private List<BuffTime> unit = new List<BuffTime>();

    private void Awake()
    {
        float time = 0;
        while (time < 3.1)
        {
            time += Time.deltaTime;
        }
        // Marker.SetActive(false);
        unit.Add(new BuffTime(){buffAvailableToUse = false,intervalTime = 1.0f,realPassTime = 0});//固定0为shootIntervalTime，开始能发射，所以为faluse
        unit.Add(new BuffTime(){buffAvailableToUse = false,intervalTime = 10.0f,realPassTime = 0});//固定1为panspeedUP
        unit.Add(new BuffTime(){buffAvailableToUse = false,intervalTime = 3.0f,realPassTime = 0});//固定2为alive
        unit.Add(new BuffTime(){buffAvailableToUse = false,intervalTime = 3.0f,realPassTime = 0});//固定3为shootSpeedUp
        alive = true;
    }

    private void Update()
    {
        if (unit[2].buffAvailableToUse != true)
        {
            PlayerMovement(); //获取wsad或上下左右来在xz平面运动
            CameraMovement();//鼠标移动视角和车身转向    有固定鼠标隐藏鼠标的功能
            ChangeCameras();//按F切换视角
            Shoot();//按空格发射子弹
            textChange();
            // if (realIntervalTime >= shootIntervalTime)
            // {
            //     realIntervalTime = 0;
            //     ableToShoot = true;
            // }
            // realIntervalTime += Time.deltaTime;
        }
        timeCollider();
        // if (Input.GetMouseButtonDown(0))//鼠标左键放出射线，选中物体，录入物体的信息
        // {
        //     var ray = GameCamera.ScreenPointToRay(Input.mousePosition);
        //     RaycastHit hit;
        //     if (Physics.Raycast(ray, out hit))
        //     {
        //         //the collider could be children of the unit, so we make sure to check in the parent
        //         var unit = hit.collider.GetComponentInParent<Unit>();
        //         m_Selected = unit;
        //         
        //         
        //         //check if the hit object have a IUIInfoContent to display in the UI
        //         //if there is none, this will be null, so this will hid the panel if it was displayed
        //         var uiInfo = hit.collider.GetComponentInParent<UIMainScene.IUIInfoContent>();
        //         UIMainScene.Instance.SetNewInfoContent(uiInfo);
        //     }
        // }
        // else if (m_Selected != null && Input.GetMouseButtonDown(1))//鼠标右键，到达指定地点或开始运送货物
        // {//right click give order to the unit
        //     var ray = GameCamera.ScreenPointToRay(Input.mousePosition);
        //     RaycastHit hit;
        //     if (Physics.Raycast(ray, out hit))
        //     {
        //         var building = hit.collider.GetComponentInParent<Building>();
        //         
        //         if (building != null)
        //         {
        //             m_Selected.GoTo(building);
        //         }
        //         else
        //         {
        //             m_Selected.GoTo(hit.point);
        //         }
        //     }
        // }
        //
        // MarkerHandling();
    }


    void textChange()
    {
        string buff = "BUFF:  ";
        if (unit[3].buffAvailableToUse)
        {
            buff += "射速UP  ";
        }

        if (unit[1].buffAvailableToUse)
        {
            buff += "移速UP";
        }

        texta.text = buff;
    }
    void PlayerMovement()
    {
        Vector2 move;
        if (unit[1].buffAvailableToUse)
        {
            move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized * PanSpeed *2;
        }
        else
        {
            move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized * PanSpeed;
        } //设置小车往各个方向的速度不变}

        playerCar.transform.Translate(new Vector3(-move.x, 0, -move.y) * Time.deltaTime);
    }

    void CameraMovement()
    {
        yMouse = sensity * Input.GetAxis("Mouse X");
        xMouse = sensity * Input.GetAxis("Mouse Y");
        playerCar.transform.Rotate(0.0f, yMouse, 0.0f);
        headOfCar.transform.RotateAround(rotateRelateTo.transform.position, rotateRelateTo.transform.up, xMouse);
        if (CheckAngle(headOfCar.transform.localEulerAngles.x)>50.0f)
        {
            headOfCar.transform.localEulerAngles = new Vector3(50.0f,0,0);
        }
        if (CheckAngle(headOfCar.transform.localEulerAngles.x)<-25.0f)
        {
            headOfCar.transform.localEulerAngles = new Vector3(-25.0f,0,0);
        }
        Cursor.visible = false;//set mouse invisible and locked in center
        Cursor.lockState = CursorLockMode.Locked;
    }
    void ChangeCameras()
    {
        if (Input.GetKeyDown(KeyCode.F)) //press F to change camera
        {
        if (GameCamera[0].gameObject.activeSelf == true)
        {
            GameCamera[0].gameObject.SetActive(false);
            GameCamera[1].gameObject.SetActive(true);
        }
        else
        {
            GameCamera[1].gameObject.SetActive(false);
            GameCamera[0].gameObject.SetActive(true);
        }
        }
    }
    float CheckAngle(float value)//设置角度为有效值
    {
        float angle = value - 180;
        if (angle > 0)
        {
            return angle - 180; }
        if (value == 0)
        {
            return 0; }
        return angle + 180;
    }
    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space)&& ! unit[0].buffAvailableToUse)
        {
        Instantiate(bullets, shootPlace.transform.position, headOfCar.transform.rotation);
        unit[0].buffAvailableToUse = true;
        }
    }
    void timeCollider()
    {
        foreach (var VARIABLE in unit)//为每个buff添加计时器
        {
            if (VARIABLE.realPassTime < VARIABLE.intervalTime&& VARIABLE.buffAvailableToUse)
                VARIABLE.realPassTime += Time.deltaTime;
            else if(VARIABLE.realPassTime > VARIABLE.intervalTime&& VARIABLE.buffAvailableToUse)
            {
                VARIABLE.realPassTime = 0;
                VARIABLE.buffAvailableToUse = false;
                if (VARIABLE == unit[3])
                {
                    Debug.Log("shoot speed down");
                    unit[0].intervalTime = 1.0f;
                }
                else if (VARIABLE == unit[1])
                {
                    Debug.Log("speed down");
                }
                else if (unit[2].buffAvailableToUse)
                {
                    Debug.Log("not alive");
                }
            }
        }
    }
    public void ChangeSpeed()
    {
        unit[1].buffAvailableToUse = true;
    }
    public void ShootSpeed()
    {
        unit[3].buffAvailableToUse = true;
        unit[0].intervalTime = 0.5f;
    }
    // Handle displaying the marker above the unit that is currently selected (or hiding it if no unit is selected)
    // void MarkerHandling()//设置绿标到选中物体
    // {
    //     if (m_Selected == null && Marker.activeInHierarchy)
    //     {
    //         Marker.SetActive(false);
    //         Marker.transform.SetParent(null);
    //     }
    //     else if (m_Selected != null && Marker.transform.parent != m_Selected.transform)
    //     {
    //         Marker.SetActive(true);
    //         Marker.transform.SetParent(m_Selected.transform, false);
    //         Marker.transform.localPosition = Vector3.zero;
    //     }    
    // }//
}
