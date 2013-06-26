using System;

namespace PluginB
{
  [Serializable]
  public class ClassB : MarshalByRefObject
  {
    public string GetValue(string sParam)
    {
      return String.Format("{0} from ClassB", sParam);
    }
  }
}
