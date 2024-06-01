namespace FinanceManagerBack.Python
{
    public interface IPythonScriptExecutor
    {
        string RunPredictionScript(string category, int limit);

        string RunReceiptAnazyleScript(string imagePath);
    }
}