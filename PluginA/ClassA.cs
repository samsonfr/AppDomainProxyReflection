using System;

namespace PluginA
{
  [Serializable]
  public class ClassA : MarshalByRefObject
  {
    public string GetValue(string sParam)
    {
      return String.Format("{0} from ClassA", sParam);
    }
  }
}
