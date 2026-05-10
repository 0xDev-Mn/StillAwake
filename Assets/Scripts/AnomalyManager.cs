using System.Collections.Generic;
using UnityEngine;

public class AnomalyManager : MonoBehaviour
{
    public List<Anomaly> anomalies;

    [Header("Loop Settings")]
    public int maxAnomaliesPerLoop = 2;
    public int loopCount = 0;
    public int targetLoops = 10;

    private EndingManager endingManager;

    void Start()
    {
        endingManager = FindObjectOfType<EndingManager>();
    }

    public void StartLoop()
    {
        // 🔥 CHECK END CONDITION FIRST (IMPORTANT FIX)
        if (loopCount >= targetLoops)
        {
            TriggerEnding();
            return;
        }

        if (anomalies.Count == 0)
        {
            Debug.LogWarning("No anomalies assigned!");
            return;
        }

        // INCREASE LOOP COUNT
        loopCount++;
        Debug.Log("Loop: " + loopCount + " / " + targetLoops);

        // RESET anomalies
        foreach (var a in anomalies)
        {
            if (a.isActive && !a.isFixed)
            {
                a.Fix();
            }

            a.isActive = false;
            a.isFixed = false;
        }

        // SCALE DIFFICULTY
        int currentMax = maxAnomaliesPerLoop;

        if (loopCount > 3) currentMax = 2;
        if (loopCount > 6) currentMax = 3;

        int count = Random.Range(1, currentMax + 1);

        List<int> usedIndexes = new List<int>();

        for (int i = 0; i < count; i++)
        {
            int randomIndex;

            do
            {
                randomIndex = Random.Range(0, anomalies.Count);
            }
            while (usedIndexes.Contains(randomIndex));

            usedIndexes.Add(randomIndex);
            anomalies[randomIndex].Activate();
        }
    }

    void TriggerEnding()
    {
        Debug.Log("Triggering Ending");

        if (endingManager != null)
        {
            endingManager.TriggerEnding();
        }
        else
        {
            Debug.LogError("EndingManager not found!");
        }
    }

    public bool AllFixed()
    {
        foreach (var a in anomalies)
        {
            if (a.isActive && !a.isFixed)
                return false;
        }
        return true;
    }
}
