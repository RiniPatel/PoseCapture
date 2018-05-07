using UnityEngine;

public class Upper_Arm_L : MonoBehaviour
{
    static int UPPER_ARM_L_ID = 0;
    float timeToGo;
    Quaternion offset;
    bool isCalibrated;
    Quaternion orig;

    // Use this for initialization
    void Start()
    {
        timeToGo = Time.fixedTime + 0.1f;
        Debug.Log("Upper Arm L is up" + transform.rotation.eulerAngles);
        // world rotation offset
        offset = Quaternion.Euler(transform.rotation.eulerAngles);
        isCalibrated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCalibrated)
        {
            if (UDPScript.IsDataReady(UPPER_ARM_L_ID))
            {
                orig = Quaternion.Euler(UDPScript.GetRoll(UPPER_ARM_L_ID), UDPScript.GetYaw(UPPER_ARM_L_ID), UDPScript.GetPitch(UPPER_ARM_L_ID));        
                isCalibrated = true;
            }
            return;
        }

        if (Time.fixedTime >= timeToGo)
        {
            //transform.rotation = Quaternion.Euler(offset.x + UDPScript.GetRoll(UPPER_ARM_L_ID), offset.y + UDPScript.GetYaw(UPPER_ARM_L_ID), offset.z - UDPScript.GetPitch(UPPER_ARM_L_ID));

            //Quaternion q = Quaternion.Euler(offset.x + UDPScript.GetRoll(UPPER_ARM_L_ID), offset.y + UDPScript.GetYaw(UPPER_ARM_L_ID), offset.z + UDPScript.GetPitch(UPPER_ARM_L_ID));
            Quaternion q = Quaternion.Euler(UDPScript.GetRoll(UPPER_ARM_L_ID), UDPScript.GetYaw(UPPER_ARM_L_ID), -UDPScript.GetPitch(UPPER_ARM_L_ID));
            //transform.localRotation = Quaternion.Inverse(transform.parent.rotation) * q;

            transform.rotation = Quaternion.Inverse(orig) * q * offset;

            timeToGo = Time.fixedTime + 0.1f;
        }
    }
}