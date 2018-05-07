using UnityEngine;

public class Upper_Arm_R : MonoBehaviour
{
    static int UPPER_ARM_R_ID = 2;
    float timeToGo;
    Quaternion offset, orig;
    bool isCalibrated;

    // Use this for initialization
    void Start()
    {
        timeToGo = Time.fixedTime + 0.1f;
        Debug.Log("Upper Arm R is up" + transform.rotation.eulerAngles);
        //offset = new Vector3(0, 180, -90);
        offset = Quaternion.Euler(transform.rotation.eulerAngles);
        isCalibrated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCalibrated)
        {
            if (UDPScript.IsDataReady(UPPER_ARM_R_ID))
            {
                orig = Quaternion.Euler(UDPScript.GetRoll(UPPER_ARM_R_ID), UDPScript.GetYaw(UPPER_ARM_R_ID), UDPScript.GetPitch(UPPER_ARM_R_ID));
                isCalibrated = true;
            }
            return;
        }


        if (Time.fixedTime >= timeToGo)
        {
            //transform.rotation = Quaternion.Euler(offset.x - UDPScript.GetRoll(UPPER_ARM_R_ID), offset.y + UDPScript.GetYaw(UPPER_ARM_R_ID), 
            //    offset.z + UDPScript.GetPitch(UPPER_ARM_R_ID));

            //Quaternion q = Quaternion.Euler(offset.x - UDPScript.GetRoll(UPPER_ARM_R_ID), offset.y + UDPScript.GetYaw(UPPER_ARM_R_ID),
            //    offset.z + UDPScript.GetPitch(UPPER_ARM_R_ID));
            //transform.localRotation = Quaternion.Inverse(transform.parent.rotation) * q;


            Quaternion q = Quaternion.Euler(-UDPScript.GetRoll(UPPER_ARM_R_ID), UDPScript.GetYaw(UPPER_ARM_R_ID), UDPScript.GetPitch(UPPER_ARM_R_ID));
            transform.rotation = Quaternion.Inverse(orig) * q * offset;

            timeToGo = Time.fixedTime + 0.1f;
        }
    }
}