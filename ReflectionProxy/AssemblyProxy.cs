using System;
using System.Collections.Generic;
using System.Reflection;

namespace ReflectionProxy
{
  [Serializable]
  public class AssemblyProxy : MarshalByRefObject, IDisposable
  {
    // ============================================================================================
    // Construction / Destruction
    // ============================================================================================

    #region "Construction / Destruction"

    public AssemblyProxy() { }

    ~AssemblyProxy()
    {
      Dispose(false);
    }

    protected virtual void Dispose(bool bDisposing)
    {
      if (bDisposing)
      {
        // Free other state (managed objects).

        if (mpAssemblyResolver != null)
        {
          AppDomain.CurrentDomain.AssemblyResolve -= mpAssemblyResolver.ResolveAssembly;
          mpAssemblyResolver = null;
        }
      }
      // Free your own state (unmanaged objects).
      // Set large fields to null.
    }

    #endregion

    // ============================================================================================
    // Public member functions
    // ============================================================================================

    #region "Public member functions"

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    public void AddAssemblyProbe(string[] asExtraDirectories)
    {
      mpAssemblyResolver = new AssemblyResolver(asExtraDirectories);
      AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(mpAssemblyResolver.ResolveAssembly);
    }

    public TypeProxy FindClassInAssemblies(string[] asModules, string sClassName)
    {
      if (!String.IsNullOrEmpty(sClassName) && asModules != null && asModules.Length > 0)
      {
        foreach (string sCurAssemblyName in asModules)
        {
          Assembly pCurAssembly = Assembly.Load(sCurAssemblyName);

          foreach (Type pCurType in pCurAssembly.GetExportedTypes())
          {
            if (String.Compare(pCurType.FullName, sClassName, true) == 0 || String.Compare(pCurType.Name, sClassName) == 0)
            {
              return new TypeProxy(pCurType);
            }
          }
        }
      }

      return null;
    }

    #endregion

    // ============================================================================================
    // Private data members
    // ============================================================================================

    private AssemblyResolver mpAssemblyResolver = null;
  }
}
