using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject bot;
    private float rotDegree = 1.0472f;
    private float degRot;
    private Quaternion newRot;

    void Start()
    {
        degRot = Mathf.Abs(Mathf.Sin(rotDegree/ 2));
        newRot = new Quaternion(0, degRot, 0, Mathf.Abs(Mathf.Cos(rotDegree / 2)));
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Quaternion curRot = bot.transform.localRotation;
        bot.transform.rotation = Quaternion.RotateTowards(curRot,newRot,10*Time.deltaTime);
        
    }
}
