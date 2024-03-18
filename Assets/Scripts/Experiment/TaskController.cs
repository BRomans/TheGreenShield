using System.Collections.Generic;
using UnityEngine;
using Gtec.Benchmark;


public class TaskController : MonoBehaviour
{
    [SerializeField]
    private int nClasses;

    [SerializeField]
    private int nTrials;

    [SerializeField]
    [Tooltip("Performance calculator")]
    protected List<PerformanceMetrics> _performanceCalculators;

    [SerializeField]
    private Collider StartTask;

    [SerializeField]
    private Collider EndTask;

    private int enemyCounter = 0;

    private int currentClassId = 0;

    void Start()
    {
        _performanceCalculators.ForEach(p => p.Initialize(nClasses, nTrials));
    }

    public void TaskTriggerEntered(string triggerName)
    {

        if (triggerName == "StartTask")
        {
            if (enemyCounter == 0)
            {
                StartTaskTimer();
            }
        }
        if (triggerName == "EndTask")
        {
            if (enemyCounter == nTrials)
            {
                StopTaskTimer();
            }
        }
    }

    public void SetCurrentClassId(int classId)
    {
        currentClassId = classId;
    }

    public void EnemyHit(int cueId)
    {
        enemyCounter++;
        _performanceCalculators.ForEach(p => p.AddCorrectlyClassified(cueId, currentClassId));
        if (enemyCounter == nTrials)
            StopTaskTimer();
    }

    public void EnemyMissed(int cueId)
    {
        enemyCounter++;
        _performanceCalculators.ForEach(p => p.AddMissClassified(cueId, currentClassId));
        if (enemyCounter == nTrials)
            StopTaskTimer();
    }

    public void StartTaskTimer()
    {
        _performanceCalculators.ForEach(p => p.StartTimer());
    }

    public void StopTaskTimer()
    {
        enemyCounter = 0;
        _performanceCalculators.ForEach(p => p.StopTimer());
        _performanceCalculators.ForEach(calculator => calculator.RetrieveTaskScore());
        _performanceCalculators.ForEach(calculator => calculator.Initialize(nClasses, nTrials));
    }
}