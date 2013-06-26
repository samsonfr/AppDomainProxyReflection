using System;

using ReflectionProxy;

namespace HostApplication
{
  [Serializable]
  public class AppDomainProxy : MarshalByRefObject, IDisposable
  {
     public AppDomainProxy() { }

    ~AppDomainProxy()
    {
      Dispose(false);
    }

    protected virtual void Dispose(bool bDisposing)
    {
      if (bDisposing)
      {
        // Free other state (managed objects).

        if (mpAppDomain != null)
        {
          AppDomain.Unload(mpAppDomain);
          mpAppDomain = null;
        }
      }
      // Free your own state (unmanaged objects).
      // Set large fields to null.
    }

    void IDisposable.Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    public AppDomain AppDomain
    {
      get
      {
        if (mpAppDomain == null)
        {
          mpAppDomain = AppDomain.CreateDomain(Guid.NewGuid().ToString("B"));
        }

        return mpAppDomain;
      }
    }

    public AssemblyProxy GetAssemblyProxy()
    {
      AssemblyProxy pAssemblyProxy = null;

      try
      {
        pAssemblyProxy = (AssemblyProxy)this.AppDomain.CreateInstanceAndUnwrap(typeof(AssemblyProxy).Assembly.FullName, typeof(AssemblyProxy).FullName);
      }
      catch (Exception pException)
      {
        System.Diagnostics.Trace.WriteLine(pException.Message);
        throw;
      }

      return pAssemblyProxy;
    }

    public object CreateInstance(string sAssemblyName, string sTypeName)
    {
      object pInstance = null;

      try
      {
        return this.AppDomain.CreateInstanceAndUnwrap(sAssemblyName, sTypeName);
      }
      catch (Exception pException)
      {
        Console.WriteLine(pException.Message);
      }

      return pInstance;
    }

    private AppDomain mpAppDomain = null;
  }
}
