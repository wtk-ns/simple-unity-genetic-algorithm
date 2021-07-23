using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics.CodeAnalysis;
using System.IO;

public class BotProperties : MonoBehaviour
{
    private Dictionary<string, float> jointTargetPositionList = new Dictionary<string, float>();
    private Dictionary<string, float> partSpring = new Dictionary<string, float>();
    private Dictionary<string, float> partDamper = new Dictionary<string, float>();
    private int timeBetweenConstractions;
    private Dictionary<string, int> isMuscleConstract = new Dictionary<string, int>();

    
    public BotProperties(GameObject botPrefab) {


        foreach (Transform obj in botPrefab.GetComponentsInChildren<Transform>()) {

            if (obj.gameObject.name.Contains(UtilityClass.botPartName)) {
                float posValue = (Random.value - 0.5f) * 2 * UtilityClass.targetPositionMultiplier;
                jointTargetPositionList.Add(obj.gameObject.name, posValue);

                int isConstract = Mathf.RoundToInt(Random.value);
                isMuscleConstract.Add(obj.gameObject.name, isConstract);

                float partS = Random.value * UtilityClass.springMultiplier;
                partSpring.Add(obj.gameObject.name, partS);

                float partD = Random.value * UtilityClass.damperMultiplier;
                partDamper.Add(obj.gameObject.name, partD);

            }
        }

        timeBetweenConstractions = Mathf.RoundToInt(Random.value * UtilityClass.timerMultiplier);
    }

    public BotProperties(Dictionary<string,float> tarPos, int timeBetw, Dictionary<string, int> isConstract,
        Dictionary<string, float> partSpring, Dictionary<string, float> partDamper) {
        this.jointTargetPositionList = tarPos;
        this.timeBetweenConstractions = timeBetw;
        this.isMuscleConstract = isConstract;
        this.partSpring = partSpring;
        this.partDamper = partDamper;
        
    }

    public Dictionary<string,float> getJointTargetPosition() {
        return this.jointTargetPositionList;
    }

    public Dictionary<string, float> getPartSpring()
    {
        return this.partSpring;
    }

    public Dictionary<string, float> getPartDamper()
    {
        return this.partDamper;
    }

    public Dictionary<string, int> getIsMuscleConstract() {
        return this.isMuscleConstract;
    }

    public int getTimeBetweenConstractions() {
        return this.timeBetweenConstractions;
    }

    public BotProperties mutateFrom(BotProperties properties) {

        Dictionary<string, float> newDict = new Dictionary<string, float>();
        Dictionary<string, float> newSpring = new Dictionary<string, float>();
        Dictionary<string, float> newDamper = new Dictionary<string, float>();
        Dictionary<string, int> newIsConstract = new Dictionary<string, int>();
        int newTime = properties.getTimeBetweenConstractions();
        BotProperties newProps;

        if (Random.value > (1 - UtilityClass.mistakePosibility))
        {
            foreach (string key in properties.getJointTargetPosition().Keys)
            {

                float newPos = properties.getJointTargetPosition()[key];
                if (Random.value > (1 - UtilityClass.mistakePosibility))
                {
                    newPos = (Random.value - 0.5f) * 2 * UtilityClass.targetPositionMultiplier;
                }

                int newConstr = properties.getIsMuscleConstract()[key];
                if (Random.value > (1 - UtilityClass.mistakePosibility))
                {
                    newConstr = Mathf.RoundToInt(Random.value);
                }

                float newS = properties.getPartSpring()[key];
                if (Random.value > (1 - UtilityClass.mistakePosibility))
                {
                    newS = Random.value * UtilityClass.springMultiplier;
                }

                float newD = properties.getPartDamper()[key];
                if (Random.value > (1 - UtilityClass.mistakePosibility))
                {
                    newD = Random.value * UtilityClass.damperMultiplier;
                }

                newDict.Add(key, newPos);
                newIsConstract.Add(key, newConstr);
                newSpring.Add(key, newS);
                newDamper.Add(key, newD);
            }


            if (Random.value > (1 - UtilityClass.mistakePosibility))
            {
                newTime = Mathf.RoundToInt(Random.value * UtilityClass.timerMultiplier);
            }

            newProps = new BotProperties(newDict, newTime, newIsConstract, newSpring, newDamper);

        }
        else
        {
            newProps = new BotProperties(properties.getJointTargetPosition(), properties.getTimeBetweenConstractions(),
                properties.getIsMuscleConstract(), properties.getPartSpring(), properties.getPartDamper());
        }

        return newProps;


    }

    public void writeToFile(string nof) {

        using (StreamWriter sw = new StreamWriter("C:\\geneticsUnity.txt", false, System.Text.Encoding.Default))
        {
            foreach(string key in jointTargetPositionList.Keys) {
                sw.WriteLine(key + ":" + jointTargetPositionList[key]);
            }
            sw.WriteLine("-----");
            sw.WriteLine(timeBetweenConstractions);
            sw.WriteLine("-----");
            foreach (string key in isMuscleConstract.Keys)
            {
                sw.WriteLine(key + ":" + isMuscleConstract[key]);
            }
            sw.WriteLine("-----");
            foreach (string key in partSpring.Keys)
            {
                sw.WriteLine(key + ":" + partSpring[key]);
            }
            sw.WriteLine("-----");
            foreach (string key in partDamper.Keys)
            {
                sw.WriteLine(key + ":" + partDamper[key]);
            }
            sw.WriteLine("eof");
            sw.Close();
            Debug.Log("Write done");
        }

    }



}
