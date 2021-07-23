using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityClass : MonoBehaviour
{
    private const int populationSize = 50;
    private const int distanceBetweenBots = 10;
    private const int randomSeed = 3335561;

    public readonly static string botPartName = "BotPart";
    public readonly static string botHeadName = "BotHead";
    public readonly static int targetPositionMultiplier = 40;
    public readonly static float mistakePosibility = 0.3f; // percent/100
    public readonly static string saveFileName = "C:\\geneticsUnity.txt"; 
    public readonly static string statFile = "C:\\statUnity.txt";
    public readonly static int timerMultiplier = 400;
    public readonly static int timeOfOnePopulation = 500;
    public readonly static int springMultiplier = 5000;
    public readonly static int damperMultiplier = 200;

    public static int popNum = 0;


    public static int getRandomSeed() {
        return randomSeed;
    }

    public static int getPopulationSize() {
        return populationSize;
    }

    public static int getDistanceBetweenBots() {
        return distanceBetweenBots;
    }


}
