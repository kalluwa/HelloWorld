using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ScriptIn14Days.src
{
    class BasicInvoker
    {
        
        /// <summary>
        /// basic invoke function to invoke c# static method
        /// </summary>
        /// <param name="className"></param>
        /// <param name="methodName"></param>
        /// <param name="parameters"></param>
        public static void InvokeStatic(string className, string methodName, object[] parameters)
        {
            Type type = Type.GetType(className, true, true);
            Type[] types = parameters.Select(ss => ss.GetType()).ToArray();
            
            MethodInfo method = type.GetMethod(methodName, types);
            method.Invoke(null, parameters);
        }

        /// <summary>
        /// run object's function
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="className"></param>
        /// <param name="methodName"></param>
        /// <param name="parameters"></param>
        public static void InvokeInstance(object instance, string methodName, object[] parameters)
        {
            Type type = instance.GetType();//Type.GetType(className, true, true);
            Type[] types = parameters.Select(ss => ss.GetType()).ToArray();

            MethodInfo method = type.GetMethod(methodName, types);
            method.Invoke(instance, parameters);
        }
        #region Quick Function

        public static void Print(string str)
        {
            BasicInvoker.InvokeStatic("System.Console", "WriteLine", new string[] { str});
        }

        public static string Time()
        {
            return DateTime.Now.ToString();
        }


        public static void OutputToDebugWindow(Form1 form,string str)
        {
            //InvokeInstance(form, "Output", new[] { str });
            form.Output(str);
        }
        #endregion
    }
}
