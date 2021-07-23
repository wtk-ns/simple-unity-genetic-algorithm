using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    public GameObject thisBot;


    private List<HingeJoint> botPartsJoints = new List<HingeJoint>();
    private GameObject botHead;
    private BotProperties props;
    
    private GameObject setBotHead() {

        GameObject bHead = null;

        foreach (Transform obj in thisBot.GetComponentsInChildren<Transform>())
        {
            if (obj.gameObject.name.Equals(UtilityClass.botHeadName)) {
                bHead = obj.gameObject;
            }
        
        }

        if (bHead == null) {
            throw new System.Exception("Error: Can't find botHead Bot.cs/getBotHead");
        }

        return bHead;

    }

    
    public void setProperties(BotProperties properties)
    {
        getBotPartsJoints();
        this.props = properties;
        this.botHead = setBotHead();
        setupBot();
       // botToString();

    }

    private void botToString() {

        string tpos = "";

        foreach(float a in props.getJointTargetPosition().Values) {
            tpos += a + " ";
        }

        Debug.Log(thisBot.name + " | " + tpos + " | " + props.getTimeBetweenConstractions());

    }

    private void setupBot() {

        for (int i = 0; i < botPartsJoints.Count; i++)
        {
            JointSpring spr = new JointSpring();
            
            spr = botPartsJoints[i].spring;
            props.getJointTargetPosition().TryGetValue(botPartsJoints[i].gameObject.name,out spr.targetPosition);
            props.getPartSpring().TryGetValue(botPartsJoints[i].gameObject.name, out spr.spring);
            props.getPartDamper().TryGetValue(botPartsJoints[i].gameObject.name, out spr.damper);

            botPartsJoints[i].spring = spr;
        }

    }


    public BotProperties getBotProperties() {
        return this.props;
    }

    private void getBotPartsJoints() {
        foreach (HingeJoint a in thisBot.GetComponentsInChildren<HingeJoint>()) {

            if (a.gameObject.name.Contains(UtilityClass.botPartName)) {
                botPartsJoints.Add(a);
            }
            
        }
    }




    private int checkTimer = 0;

    void FixedUpdate()
    {

        checkTimer++;

        if (checkTimer == props.getTimeBetweenConstractions())
        {
            checkTimer = 0;
            foreach (HingeJoint joint in botPartsJoints)
            {
                
                JointSpring newSpring = new JointSpring();
                newSpring = joint.spring;
                newSpring.targetPosition = -joint.spring.targetPosition*props.getIsMuscleConstract()[joint.gameObject.name];
                joint.spring = newSpring;
                
                
            }
        }
   
    }


}
