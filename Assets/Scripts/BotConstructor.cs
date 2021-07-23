using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BotConstructor : MonoBehaviour
{
    public GameObject botPrefab;
    private List<GameObject> population;
    private List<object> bestWeights;
    private GameObject test;
    private Bot bestBot = null;

   
    void Start()
    {
        
        Random.InitState(UtilityClass.getRandomSeed());
        population = makePopulation(new BotProperties(botPrefab));

        



    }

    int timerTicker = 0;
    void FixedUpdate()
    {
        
        timerTicker++;
        if (timerTicker == UtilityClass.timeOfOnePopulation)
        {
            timerTicker = 0;
            GameObject bestObj = getBest();
            bestBot = (Bot)bestObj.GetComponent("Bot");
            

            foreach (GameObject obj in population) {
                Destroy(obj);
            }
            Destroy(test);
            population = null;

            
            
            /*
            using (StreamWriter sw = new StreamWriter(UtilityClass.statFile, true, System.Text.Encoding.Default))
            {
                sw.WriteLine(getBestLen(best));
                sw.Close();
            }
            */

            population = makePopulation(bestBot.getBotProperties());

            

        }
        
    }

    void OnApplicationQuit()
    {

        bestBot.getBotProperties().writeToFile(UtilityClass.saveFileName);
        Debug.Log("Method onQuit Done");

    }

    private string getBestLen(GameObject best) {

        string xcoord = "";
        foreach (Transform a in best.GetComponentsInChildren<Transform>())
        {
            if (a.name.Equals(UtilityClass.botHeadName)) {
                xcoord = "" + a.position.x;
            }
        
        }
        
        return xcoord;
    }

    private GameObject getBest() {

        GameObject best = null;
        float coorX = 0;

        foreach (GameObject obj in population) {

            Transform botHead = null;

            foreach (Transform transform in obj.GetComponentsInChildren<Transform>()) {
                if (transform.name.Equals(UtilityClass.botHeadName)) {
                    botHead = transform;
                }
            }


            if (botHead.position.x > coorX && botHead.position.x > 0) {

                coorX = botHead.position.x;
                best = obj;

            }
        
        }

        if (best == null)
        {
            best = population[0];
            Debug.Log("BEST NULL");
        }

        Debug.Log(best.name);

        return best;
    
    }

    private List<GameObject> makePopulation(BotProperties bestProperties) {

        UtilityClass.popNum++;
        Debug.Log("Start making population " + UtilityClass.popNum);

        List<GameObject> temp = new List<GameObject>();

        test = makeTest(bestProperties);
        temp.Add(makeOne(bestProperties, 0));
        for (int i = 1; i < UtilityClass.getPopulationSize(); i++) {

            temp.Add(makeOne(bestProperties.mutateFrom(bestProperties), i));           
        }

        Debug.Log("Population done");
        return temp;

    }

    private GameObject makeTest(BotProperties properties) {
        Vector3 newPos = new Vector3(0, 0, -1 * UtilityClass.getDistanceBetweenBots());
        GameObject newBot = GameObject.Instantiate(botPrefab, newPos, Quaternion.identity);
        newBot.name = "bot_" + "test" + "_" + UtilityClass.popNum;
        Bot botScript = (Bot)newBot.GetComponent("Bot");
        botScript.setProperties(properties);

        return newBot;
    }

    private GameObject makeOne(BotProperties properties, int i) {


        Vector3 newPos = new Vector3(0, 0, i * UtilityClass.getDistanceBetweenBots());
        GameObject newBot = GameObject.Instantiate(botPrefab, newPos, Quaternion.identity);
        newBot.name = "bot_" + i + "_" + UtilityClass.popNum;
        Bot botScript = (Bot)newBot.GetComponent("Bot");
        botScript.setProperties(properties);

        return newBot;


    }

    

}
