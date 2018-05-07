using UnityEngine;

public class Lower_Arm_L : MonoBehaviour
{
    static int LOWER_ARM_L_ID = 1;
    float timeToGo;
    Quaternion offset, orig;
    bool isCalibrated;

    // Use this for initialization
    void Start()
    {
        timeToGo = Time.fixedTime + 0.1f;
        Debug.Log("Lower Arm L is up" + transform.rotation.eulerAngles);
        
        // world rotation offset
        offset = Quaternion.Euler(transform.rotation.eulerAngles);
        isCalibrated = false;
        // world rotation offset
        //offset.x = -90;
        //offset.y = 180;
        //offset.z = -90;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCalibrated)
        {
            if (UDPScript.IsDataReady(LOWER_ARM_L_ID))
            {
                orig = Quaternion.Euler(UDPScript.GetRoll(LOWER_ARM_L_ID), UDPScript.GetYaw(LOWER_ARM_L_ID), UDPScript.GetPitch(LOWER_ARM_L_ID));
                isCalibrated = true;
            }
            return;
        }

        if (Time.fixedTime >= timeToGo)
        {
            //transform.rotation = Quaternion.Euler(new Vector3(offset.x - UDPScript.GetRoll(LOWER_ARM_L_ID), offset.y + UDPScript.GetYaw(LOWER_ARM_L_ID),
            //    -UDPScript.GetPitch(LOWER_ARM_L_ID) + offset.z));

            //Quaternion q = Quaternion.Euler(offset.x - UDPScript.GetRoll(LOWER_ARM_L_ID), offset.y + UDPScript.GetYaw(LOWER_ARM_L_ID),
            //offset.z - UDPScript.GetPitch(LOWER_ARM_L_ID));
            //transform.localRotation = Quaternion.Inverse(transform.parent.rotation) * q;

            Quaternion q = Quaternion.Euler(UDPScript.GetRoll(LOWER_ARM_L_ID), UDPScript.GetYaw(LOWER_ARM_L_ID), -UDPScript.GetPitch(LOWER_ARM_L_ID));
            transform.rotation = Quaternion.Inverse(orig) * q * offset;

            timeToGo = Time.fixedTime + 0.1f;

        }
    }
}