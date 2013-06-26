using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ReflectionProxy;

namespace HostApplication
{
  public partial class FrmMain : Form
  {
    public FrmMain()
    {
      InitializeComponent();

      if (cboAssemblyName.Items.Count > 0)
      {
        cboAssemblyName.SelectedIndex = 0;
      }

      if (cboClassName.Items.Count > 0)
      {
        cboClassName.SelectedIndex = 0;
      }
    }

    private void cmdExecute_Click(object sender, EventArgs e)
    {
try
{
  using (AppDomainProxy pAppDomainProxy = new AppDomainProxy())
  {
    AssemblyProxy pAssemblyProxy = pAppDomainProxy.GetAssemblyProxy();
    TypeProxy pTypeProxy = pAssemblyProxy.FindClassInAssemblies(new string[] { cboAssemblyName.Text }, cboClassName.Text);

    if (pTypeProxy != null)
    {
      MethodInfoProxy pMethodInfoProxy = pTypeProxy.FindMethod("GetValue", new string[] { "System.String" });

      if (pMethodInfoProxy != null)
      {
        object pInstance = pAppDomainProxy.CreateInstance(cboAssemblyName.Text, cboClassName.Text);
        object pResult = pMethodInfoProxy.Invoke(pInstance, new object[] { txtParameter.Text });
        txtOutput.Text = Convert.ToString(pResult);
        MessageBox.Show("Now is the time to look both AppDomain before it gets deleted!");
      }
    }
  }
}
catch (Exception pException)
{
  MessageBox.Show(pException.Message);
}
    }
  }
}
