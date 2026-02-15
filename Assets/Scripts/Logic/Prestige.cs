using System;

public static class Prestige
{
    public static int CalculatePureChromaFromChroma(double chromaAmount)
    {
        int pureChromaGained = (int)Math.Floor(Math.Log10(chromaAmount)) - 5;
        pureChromaGained = pureChromaGained < 0 ? 0 : (int)Math.Pow(2, 1 + pureChromaGained);
        return pureChromaGained;
    }

    public static float CalculateIncreaseFromPureChroma(int pureChromaAmount)
    {
        return pureChromaAmount * 0.1f;
    }
}
