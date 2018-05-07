using UnityEngine;

public class Head : MonoBehaviour
{
    //static int HEAD_ID = 15;
    float timeToGo;
    Vector3 offset;

    // Use this for initialization
    void Start()
    {
        timeToGo = Time.fixedTime + 0.1f;
        Debug.Log("Head is up" + transform.rotation.eulerAngles);
        offset = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.fixedTime >= timeToGo)
        {
            //transform.rotation = Quaternion.Euler(offset.x + UDPScript.GetRoll(HEAD_ID), offset.y + UDPScript.GetYaw(HEAD_ID), offset.z - UDPScript.GetPitch(HEAD_ID));
            timeToGo = Time.fixedTime + 0.1f;
        }
    }
}