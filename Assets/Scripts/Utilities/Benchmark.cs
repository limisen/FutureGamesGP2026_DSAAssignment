using UnityEngine;

public static class Benchmark
{
    public static float startTime;
    public static float endTime;
    public static float endTimeTicks;

    private static System.Diagnostics.Stopwatch stopwatch;

    public static float timeTaken
    {
        get { return endTime - startTime; }
    }
    public static float timeTakenTicks
    {
        get { return endTimeTicks; }
    }

    public static void StartBenchmark()
    {
        Debug.Log("Benchmarking started...");
        startTime = Time.realtimeSinceStartup;
        stopwatch = System.Diagnostics.Stopwatch.StartNew();
    }

    public static void EndBenchmark()
    {
        Debug.Log("Benchmarking ended.");
        endTime = Time.realtimeSinceStartup;
        stopwatch.Stop();
        endTimeTicks = stopwatch.ElapsedTicks;
    }
}
