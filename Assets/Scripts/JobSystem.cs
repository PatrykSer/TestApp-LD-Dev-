using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

public class JobSystem : MonoBehaviour
{
    //variables
    [SerializeField] bool isUsedJobs;


    private void Update()
    {
        //Variables need to start calculation the CPU time.
        float startTime = Time.realtimeSinceStartup;

        //Check that jobs is used
        if (isUsedJobs)
        {
            //is yes make the list with all task
            NativeList<JobHandle> jobHandleList = new NativeList<JobHandle>(Allocator.Temp);

            for (int i = 0; i < 10; i++)
            {
                JobHandle jobHandle = HardTaskToDo();
                jobHandleList.Add(jobHandle);
            }
            JobHandle.CompleteAll(jobHandleList);
            jobHandleList.Dispose();
        }
        else
        {
            //Calculated the path for 10 differnt units
            for (int i = 0; i < 10; i++)
            {
                HardTask();
            }

        }
        //show ms in console
        Debug.Log(((Time.realtimeSinceStartup - startTime) * 1000f) + "ms");
    }

    private void HardTask()
    {
        //This is hard task witch the CPU must solve
        //the task is to calculate the plotted number
        float value = 0f;
        for (int i = 0; i < 75000; i++)
        {
            value = math.exp10(math.sqrt(value));
        }
    }
    //Start new task before the lastest finished
    private JobHandle HardTaskToDo()
    {
        HardTask task = new HardTask();
        return task.Schedule();
    }


}
[BurstCompile]
//Struct the JobSystem
public struct HardTask : IJob
{
    //Implementing the interface with Execute func 
    public void Execute()
    {
        float value = 0f;
        for (int i = 0; i < 75000; i++)
        {
            value = math.exp10(math.sqrt(value));
        }
    }
}
