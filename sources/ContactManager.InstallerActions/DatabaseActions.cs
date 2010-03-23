using System.ComponentModel;
using System.Configuration.Install;
using System.Windows.Forms;


namespace ContactManager.InstallerActions
{
    [RunInstaller(true)]
    public partial class DatabaseActions : Installer
    {
        public DatabaseActions()
        {
            InitializeComponent();
        }

        public override void Install(System.Collections.IDictionary stateSaver)
        {
            MessageBox.Show("Install");
            base.Install(stateSaver);

            // Todo: Write Your Custom Install Logic Here 
        }

    }
}
