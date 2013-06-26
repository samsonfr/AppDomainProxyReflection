using System;
using System.Collections.Generic;
using System.Reflection;

namespace ReflectionProxy
{
  [Serializable]
  public class TypeProxy : MarshalByRefObject
  {
    public TypeProxy(Type pType)
    {
      mpType = pType;
    }

    public string Name
    {
      get { return mpType.Name; }
    }

    public MethodInfoProxy FindStaticMethod(string sMethodName, string[] asParamsTypes)
    {
      return FindMethod(sMethodName, BindingFlags.Static | BindingFlags.Public, asParamsTypes);
    }

    public MethodInfoProxy FindMethod(string sMethodName, string[] asParamsTypes)
    {
      return FindMethod(sMethodName, BindingFlags.Instance | BindingFlags.Public, asParamsTypes);
    }

    public MethodInfoProxy FindMethod(string sMethodName, BindingFlags eFlags, string[] asParamsTypes)
    {
      Type[] apTypes = new Type[asParamsTypes.Length];

      for (int nCur = 0; nCur < asParamsTypes.Length; ++nCur)
      {
        apTypes[nCur] = Type.GetType(asParamsTypes[nCur]);
      }

      MethodInfo pMethodInfo = mpType.GetMethod(sMethodName, eFlags, null, CallingConventions.Any, apTypes, null);

      if (pMethodInfo != null)
      {
        return new MethodInfoProxy(pMethodInfo);
      }

      return null;
    }

    private Type mpType = null;
  }
}
