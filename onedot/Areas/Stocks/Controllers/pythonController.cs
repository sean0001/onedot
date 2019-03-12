using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System.Configuration;



namespace one.OneDot.Areas.Stocks.Controllers
{
    public class pythonController : Controller
    {
        // GET: Stocks/python
        public ActionResult Index(int? execCategory = 0)
        {

            var libs = ConfigurationManager.AppSettings["Python_libs"];
            var lib = ConfigurationManager.AppSettings["Python_lib"];

            if (execCategory == 1) {

                ScriptEngine engine = Python.CreateEngine();
                ICollection<string> Paths = engine.GetSearchPaths();
                Paths.Add(@"C:\Python352_64\libs");
                Paths.Add(@"C:\Python352_64\Lib");
                engine.SetSearchPaths(Paths);
                ScriptSource source = engine.CreateScriptSourceFromFile("H:\\Src\\onedot\\web\\onedot\\onedot\\PythonScript\\test.py");
                CompiledCode code = source.Compile();
                ScriptScope scope = code.DefaultScope;
                code.Execute();


                ///exist one parameter in python function.
                var TelephoneNumberF = scope.GetVariable<Func<object, object>>("TelephoneNumberF");
                dynamic obj = TelephoneNumberF("222");

                ///the python function list is null
                //var TelephoneNumberF = scope.GetVariable<Func<object>>("TelephoneNumberF");
                //dynamic obj = TelephoneNumberF();

                string t = obj[0]; 
                Console.Read(); 




                //var ipy = Python.CreateRuntime();

                //dynamic test = ipy.UseFile("H:\\Src\\onedot\\web\\onedot\\onedot\\PythonScript\\test.py");

                //test.simple();
            }
            

            return View();
        }
    }
}