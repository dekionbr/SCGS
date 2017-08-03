using System.Web;
using System.Web.Optimization;

namespace SCGS.WEB
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            //<!-- jQuery -->
            //<script src="js/jquery.js"></script>

            //<!-- Bootstrap Core JavaScript -->
            //<script src="js/bootstrap.min.js"></script>

            //<!-- Morris Charts JavaScript -->
            //<script src="js/plugins/morris/raphael.min.js"></script>
            //<script src="js/plugins/morris/morris.min.js"></script>
            //<script src="js/plugins/morris/morris-data.js"></script>

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery-{version}.js",
                       "~/Scripts/jquery-{version}.intellisense.js"));

            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
                        "~/Scripts/bootstrap-datepicker.js",
                        "~/Scripts/locales/bootstrap-datepicker.pt-BR.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));



            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                       "~/Scripts/bootstrap.js"));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                       "~/Scripts/modernizr-*"));

            //<!-- Bootstrap Core CSS -->
            //<link href="css/bootstrap.min.css" rel="stylesheet">

            //<!-- Custom CSS -->
            //<link href="css/sb-admin.css" rel="stylesheet">

            //<!-- Morris Charts CSS -->
            //<link href="css/plugins/morris.css" rel="stylesheet">

            //<!-- Custom Fonts -->
            //<link href="font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap/css/bootstrap.css",
                        "~/Content/bootstrap/css/sb-admin.css",
                        "~/Content/bootstrap/css/plugins/morris.css",
                        "~/Content/bootstrap/font-awesome/css/font-awesome.css"));

            bundles.Add(new StyleBundle("~/Content/login").Include(
                        "~/Content/login.css"));


            bundles.Add(new StyleBundle("~/Content/datepicker").Include(
                        "~/Content/bootstrap-datepicker.css",
                        "~/Content/bootstrap-datepicker.standalone.css"));



            bundles.Add(new StyleBundle("~/Content/select2").Include(
                        "~/Content/select2-bootstrap.css"));


            bundles.Add(new ScriptBundle("~/bundles/morris").Include(
                       "~/Scripts/plugins/morris/raphael.js",
                       "~/Scripts/plugins/morris/morris.js",
                       "~/Scripts/plugins/morris/morris-data.js"));



            //<!-- Flot Charts JavaScript -->
            //<!--[if lte IE 8]><script src="js/excanvas.min.js"></script><![endif]-->
            //<script src="js/plugins/flot/jquery.flot.js"></script>
            //<script src="js/plugins/flot/jquery.flot.tooltip.min.js"></script>
            //<script src="js/plugins/flot/jquery.flot.resize.js"></script>
            //<script src="js/plugins/flot/jquery.flot.pie.js"></script>
            //<script src="js/plugins/flot/flot-data.js"></script>

            bundles.Add(new ScriptBundle("~/bundles/flot").Include(
                       "~/Scripts/plugins/flot/jquery.flot.js",
                       "~/Scripts/plugins/flot/jquery.flot.tooltip.js",
                       "~/Scripts/plugins/flot/jquery.flot.resize.js",
                       "~/Scripts/plugins/flot/jquery.flot.pie.js",
                       "~/Scripts/plugins/flot/jquery.flot.categories.js",
                       "~/Scripts/plugins/flot/flot-data.js"));

            bundles.Add(new ScriptBundle("~/script/funcionario").Include(
                      //"~/Scripts/main.js",
                      "~/Scripts/funcionario.js"));


            bundles.Add(new ScriptBundle("~/script/atendimento").Include(
                      //"~/Scripts/main.js",
                      "~/Scripts/Relatorios/atendimentos.js"));

            bundles.Add(new ScriptBundle("~/Script/Relatorios/Consultas/GerenteGeral").Include(
                     "~/Scripts/Relatorios/consulta.js",
                    "~/Scripts/Relatorios/consulta.gerente_geral.js"));

            bundles.Add(new ScriptBundle("~/Script/Relatorios/Consultas/Gerente").Include(
                    "~/Scripts/Relatorios/consulta.js",
                    "~/Scripts/Relatorios/consulta.gerente.js"));

            bundles.Add(new ScriptBundle("~/Script/Relatorios/Consultas").Include(
                    "~/Scripts/Relatorios/consulta.js"));


            bundles.Add(new ScriptBundle("~/Script/Relatorios/Endemias").Include(
                    "~/Scripts/Relatorios/endemias.js"));

        }
    }
}