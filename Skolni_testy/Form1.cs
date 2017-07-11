using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using MaterialSkin;
using MaterialSkin.Controls;

using Skolni_testy.Controllers;
using Skolni_testy.App;

namespace Skolni_testy
{
    public partial class Form1 : MaterialForm
    {
        public Form1()
        {
            InitializeComponent();

            SetUpController.SetUpDB();


            // set material graphics
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);

            var appContext = new SkolniTestyAppContext(new Router(), new ViewManager(this));
            appContext.Router.Context = appContext;
            appContext.ViewManager.Context = appContext;

            appContext.Router.SwitchTo("MainScreen", "Index", null);
        }
    }
}
