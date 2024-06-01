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

            if(Runtime.PythonDLL == null)
            {
                Runtime.PythonDLL = dllPath;
                PythonEngine.Initialize();
            }
        }

        private string RunScript(PyObject[] attributes, string scriptPath)
        {
            using (Py.GIL())
            {
                dynamic os = Py.Import("os");
                dynamic sys = Py.Import("sys");

                sys.path.append(os.path.dirname(os.path.expanduser(scriptPath)));
                var fromFile = Py.Import(Path.GetFileNameWithoutExtension(scriptPath));

                var result = fromFile.InvokeMethod("main", attributes);

                return result.ToString();
            }
        }

        public string RunPredictionScript(string category, int limit)
        {
            var scriptPath = _config.GetValue<string>("Python:PredictionScriptName");

            var dataPath = _config.GetValue<string>("Python:PredictionDataPath");

            var dataPathAttribute = new PyString($"{dataPath}\\data_{category.ToLower()}.csv");
            var limitAttribute = new PyInt(limit);

            var attributes = new PyObject[] { dataPathAttribute, limitAttribute };

            return RunScript(attributes, scriptPath);
        }

        public string RunReceiptAnazyleScript(string imagePath)
        {
            var scriptPath = _config.GetValue<string>("Python:ReceiptAnazyleScriptName");
            var tesseractPath = _config.GetValue<string>("Python:TesseractPath");

            var tesseractPathAtribute = new PyString(tesseractPath);
            var imagePathAttribute = new PyString(imagePath);

            var attributes = new PyObject[] { tesseractPathAtribute, imagePathAttribute };

            return RunScript(attributes, scriptPath);
        }
    }
}