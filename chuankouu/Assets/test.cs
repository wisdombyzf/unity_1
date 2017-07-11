using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using UnityEngine;
using System.Text.RegularExpressions;

public class test : MonoBehaviour
{
    Transform obj_transform;
    Rigidbody obj_rigidbody;




    bool IsGO;
    private SerialPort sp;
    private Thread receiveThread;  //用于接收消息的线程
    private Thread sendThread;     //用于发送消息的线程
    private int flag;              //丢弃标志


    void Start()
    {
        sp = new SerialPort("COM8", 115200);  //这里的"COM3"是我的设备，可以在设备管理器查看。
                                              //9600是默认的波特率，需要和Arduino对应，其它的构造参数可以不用配置。
        sp.ReadTimeout = 500;
        sp.Open();

        

        obj_transform = gameObject.GetComponent<Transform>();//获取位置组件
        obj_rigidbody = gameObject.GetComponent<Rigidbody>();//获取刚体组件
        flag = 4;

    }

    
    // Update is called once per frame
    void Update()
    {

        flag = 4;
        if (this.sp != null && this.sp.IsOpen)
        {
            try
            {
                String strRec=null;
                while (flag>0)
                {
                    strRec = sp.ReadLine();            //SerialPort读取数据有多种方法，我这里根据需要使用了ReadLine(),读取字符串以/r/n结尾
                    flag--;
                }


                Debug.Log("Receive From Serial: " + strRec);
                /*
                    Regex accelerated_speed_x_regex = new Regex(@"accx\s*=(-?[\d\.]+)");          //x轴加速度
                    if (accelerated_speed_x_regex.IsMatch(strRec))
                    {
                        Match accelerated_speed_x_match = accelerated_speed_x_regex.Match(strRec);
                        Debug.Log(accelerated_speed_x_match.Groups[1].Value);
                        float accelerated_speed = float.Parse(accelerated_speed_x_match.Groups[1].Value);
                        obj_rigidbody.AddForce(Vector3.forward * accelerated_speed * 0.05f);
                    }


                    Regex accelerated_speed_y_regex = new Regex(@"accy\s*=(-?[\d\.]+)");          //y轴加速度
                    if (accelerated_speed_y_regex.IsMatch(strRec))
                    {
                        Match accelerated_speed_y_match = accelerated_speed_y_regex.Match(strRec);
                        Debug.Log(accelerated_speed_y_match.Groups[1].Value);
                        float accelerated_speed = float.Parse(accelerated_speed_y_match.Groups[1].Value);
                        obj_rigidbody.AddForce(Vector3.left * accelerated_speed * 0.05f);
                    }

                    Regex accelerated_speed_z_regex = new Regex(@"accz\s*=(-?[\d\.]+)");          //z轴加速度
                    if (accelerated_speed_z_regex.IsMatch(strRec))
                    {
                        Match accelerated_speed_z_match = accelerated_speed_z_regex.Match(strRec);
                        Debug.Log(accelerated_speed_z_match.Groups[1].Value);
                        float accelerated_speed = float.Parse(accelerated_speed_z_match.Groups[1].Value);
                        obj_rigidbody.AddForce(Vector3.up * accelerated_speed * 0.05f);
                    }
                    */
                obj_transform.eulerAngles = new Vector3(0f, 0f, 0f);
                Regex angular_speed_x_regex = new Regex(@"angx\s*=(-?[\d\.]+)");          //x轴角速度
                if (angular_speed_x_regex.IsMatch(strRec))
                {
                    Match angular_speed_x_match = angular_speed_x_regex.Match(strRec);
                    Debug.Log(angular_speed_x_match.Groups[1].Value);
                    float angular_speed = float.Parse(angular_speed_x_match.Groups[1].Value);                    
                    obj_transform.Rotate(Vector3.back * (angular_speed), Space.Self);
                }

                Regex angular_speed_y_regex = new Regex(@"angy\s*=(-?[\d\.]+)");          //y轴角速度
                if (angular_speed_y_regex.IsMatch(strRec))
                {
                    Match angular_speed_y_match = angular_speed_y_regex.Match(strRec);
                    Debug.Log(angular_speed_y_match.Groups[1].Value);
                    float angular_speed = float.Parse(angular_speed_y_match.Groups[1].Value);
                    obj_transform.Rotate(Vector3.left * (angular_speed), Space.Self);
                }


                Regex angular_speed_z_regex = new Regex(@"angz\s*=(-?[\d\.]+)");          //z轴角速度
                if (angular_speed_z_regex.IsMatch(strRec))
                {
                    Match angular_speed_z_match = angular_speed_z_regex.Match(strRec);
                    Debug.Log(angular_speed_z_match.Groups[1].Value);
                    float angular_speed = float.Parse(angular_speed_z_match.Groups[1].Value);
                    obj_transform.Rotate(Vector3.up * angular_speed, Space.Self);
                }

                /* String test = match.Groups[1].Value;
                 Debug.Log(test);

                 if (test.Equals("2"))
                 {
                     obj_transform.Translate(Vector3.forward, Space.Self);
                     Debug.Log("成功");
                 }*/
                /* if (strRec == 's')
                 {
                     my_obj.Translate(Vector3.back, Space.Self);

                 }
                 if (strRec == 'a')
                 {
                     my_obj.Translate(Vector3.left, Space.Self);

                 }
                 if (strRec == 'd')
                 {
                     my_obj.Translate(Vector3.right, Space.Self);

                 }*/



            }
            catch
            {
                Debug.Log("有误");
                //continue;
            }
            Debug.Log("有误22222");
        }
        else
        {
            Debug.Log("呵呵呵1");
        }






    }
}
