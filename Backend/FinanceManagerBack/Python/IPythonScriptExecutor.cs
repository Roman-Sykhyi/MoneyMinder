namespace FinanceManagerBack.Python
{
    public interface IPythonScriptExecutor
    {
        string RunPredictionScript(string category, int limit);

        void RunReceiptAnazyleScript();
    }
}