using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ReflectionProxy
{
  [Serializable] // Otherwise I was getting is not marked as serializable
  public class AssemblyResolver : MarshalByRefObject
  {
    // ============================================================================================
    // Construction / Destruction
    // ============================================================================================

    #region "Construction / Destruction"

    public AssemblyResolver(params string[] asExtraDirectories)
    {
      mpListExtraDirectories = new List<string>(asExtraDirectories);
    }

    #endregion

    // ============================================================================================
    // Public member functions
    // ============================================================================================

    #region "Public member functions"

    public Assembly ResolveAssembly(object sender, ResolveEventArgs args)
    {
      return ResolveAssembly(args.Name);
    }

    public Assembly ResolveAssembly(string sAssemblyFullName)
    {
      Assembly pAssembly = null;

      try
      {
        string sAssemblyFileName = new AssemblyName(sAssemblyFullName).Name + ".dll";
        string sAssemblyPath = null;

        foreach (string sCurFolderPath in mpListExtraDirectories)
        {
          sAssemblyPath = Path.Combine(sCurFolderPath, sAssemblyFileName);
          pAssembly = LoadAssembly(sAssemblyPath);

          if (pAssembly != null)
          {
            return pAssembly;
          }
        }

        // Search beside the executing assembly

        string sFolderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        sAssemblyPath = Path.Combine(sFolderPath, sAssemblyFileName);

        pAssembly = LoadAssembly(sAssemblyPath);
      }
      catch { }

      return pAssembly;
    }

    #endregion

    // ============================================================================================
    // Private member functions
    // ============================================================================================

    #region "Public member functions"

    private Assembly LoadAssembly(string sAssemblyPath)
    {
      Assembly pAssembly = null;

      if (File.Exists(sAssemblyPath))
      {
        return Assembly.LoadFrom(sAssemblyPath);
      }

      return pAssembly;
    }

    #endregion

    // ============================================================================================
    // Private data members
    // ============================================================================================

    private List<string> mpListExtraDirectories = null;
  }
}
