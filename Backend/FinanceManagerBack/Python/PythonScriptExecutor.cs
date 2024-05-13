using Microsoft.Extensions.Configuration;
using Python.Runtime;
using System.IO;

namespace FinanceManagerBack.Python
{
    public class PythonScriptExecutor : IPythonScriptExecutor
    {
        IConfiguration _config;

        public PythonScriptExecutor(IConfiguration config)
        {
            _config = config;

            var dllPath = _config.GetValue<string>("Python:DLLPath");
            Runtime.PythonDLL = dllPath;

            PythonEngine.Initialize();
        }

        public string RunPredictionScript(string category, int limit)
        {
            using(Py.GIL())
            {
                dynamic os = Py.Import("os");
                dynamic sys = Py.Import("sys");

                var filePath = _config.GetValue<string>("Python:PredictionScriptName");
                sys.path.append(os.path.dirname(os.path.expanduser(filePath)));
                var fromFile = Py.Import(Path.GetFileNameWithoutExtension(filePath));

                var dataPath = _config.GetValue<string>("Python:PredictionDataPath");

                var dataPathAttribute = new PyString($"{dataPath}\\data_{category.ToLower()}.csv");
                var limitAttribute = new PyInt(limit);

                var result = fromFile.InvokeMethod("main", new PyObject[] { dataPathAttribute, limitAttribute });

                return result.ToString();
            }
        }

        public void RunReceiptAnazyleScript()
        {
            throw new System.NotImplementedException();
        }
    }
}
