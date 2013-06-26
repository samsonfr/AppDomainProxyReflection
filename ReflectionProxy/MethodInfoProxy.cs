using System;
using System.Reflection;

namespace ReflectionProxy
{
  [Serializable]
  public class MethodInfoProxy : MarshalByRefObject
  {
    public MethodInfoProxy(MethodInfo pMethodInfo)
    {
      mpMethodInfo = pMethodInfo;
    }

    public string DeclaringTypeFullName
    {
      get { return mpMethodInfo.DeclaringType.FullName; }
    }

    public string Name
    {
      get { return mpMethodInfo.Name; }
    }

    public object Invoke(object pInstance, object[] apParams)
    {
      return mpMethodInfo.Invoke(pInstance, apParams);
    }

    public object InvokeStatic(object[] apParams)
    {
      return mpMethodInfo.Invoke(null, apParams);
    }

    private MethodInfo mpMethodInfo = null;
  }
}
