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
using System.Globalization;
using System.Threading;

namespace Skolni_testy
{
    public partial class Form1 : MaterialForm
    {
        public Form1()
        {
            InitializeComponent();

            var db = SetUpController.SetUpDB();

            //set language
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("cs-CZ");
            this.Text = Properties.Translations.AppName;

            // set material graphics
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            
            var appContext = new SkolniTestyAppContext(new Router(), new ViewManager(this), db);
            appContext.Router.Context = appContext;
            appContext.ViewManager.Context = appContext;

            appContext.Router.SwitchTo("TeacherTests", "Show", new Dictionary<string, object> { { "test", db.Tests.First() } });
            //appContext.Router.SwitchTo("MainScreen", "Index", new Dictionary<string, object> { { "id", db.Tests.First().Id } });
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

       
    }
}
