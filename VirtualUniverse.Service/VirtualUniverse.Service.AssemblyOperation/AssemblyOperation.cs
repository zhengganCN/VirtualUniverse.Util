using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace VirtualUniverse.Service.AssemblyOperation
{
    /// <summary>
    /// 程序集操作
    /// </summary>
    public class AssemblyOperation
    {
        /// <summary>
        /// 加载与程序集名称匹配的程序集，匹配方式使用StartsWith
        /// </summary>
        /// <param name="assemblyNames">程序集名称（前缀匹配）</param>
        /// <returns></returns>
        public static Assembly[] LoadAssemblies(params string[] assemblyNames)
        {
            var runtimeLibraries = DependencyContext.Default.RuntimeLibraries;
            var assemblies = new HashSet<Assembly>();
            foreach (var assemblyName in assemblyNames)
            {
                var matchLibs = runtimeLibraries.Where(lib => lib.Name.StartsWith(assemblyName));
                foreach (var lib in matchLibs)
                {
                    assemblies.Add(AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(lib.Name)));
                }
            }
            return assemblies.ToArray();
        }
    }
}
