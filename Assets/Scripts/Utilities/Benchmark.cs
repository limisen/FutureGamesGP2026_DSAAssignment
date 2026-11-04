using UnityEngine;

public static class Benchmark
{
    public static float startTime;
    public static float endTime;

    public static float timeTaken
    {
        get { return endTime - startTime; }
    }

    public static void StartBenchmark()
    {
        Debug.Log("Benchmarking started...");
        startTime = Time.realtimeSinceStartup;
    }

    public static void EndBenchmark()
    {
        Debug.Log("Benchmarking ended.");
        endTime = Time.realtimeSinceStartup;

    }
}
