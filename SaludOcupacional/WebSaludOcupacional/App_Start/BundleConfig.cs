using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace WebSaludOcupacional.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/js/pages/examples/sign-in.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery2").Include(
                    "~/Content/plugins/jquery/jquery-3.3.1.min.js"
                    ,"~/Content/plugins/jquery/bootstrap.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                    "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                    "~/Scripts/js/jquery.min.js"
                    //"~/Scripts/jquery-latest.min.js"
                    ,"~/Scripts/bootstrap.js"
                    ,"~/Scripts/datatables/js/jquery.dataTables.js"
                    ,"~/Scripts/datatables-plugins/dataTables.bootstrap.min.js"
                    ,"~/Scripts/datatables-responsive/dataTables.responsive.js"
                    ,"~/Scripts/respond.js"
                    ,"~/Scripts/jquery.keyboard.min.js"
                    ,"~/Scripts/jquery.keyboard.extension-caret.min.js"
                    ,"~/Scripts/nprogress.js"
                    ,"~/Scripts/jquery.timepicker.min.js"
                    ));

            bundles.Add(new StyleBundle("~/Content/css2").Include(
                    "~/Content/plugins/bootstrap/css/bootstrap.css"
                    ,"~/Content/plugins/node-waves/waves.css"
                    ,"~/Content/plugins/animate-css/animate.css"
                    ,"~/Content/css/jquery-ui.min.css"
                    ,"~/Content/css/style.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                    "~/Content/jquery-ui.css"
                    ,"~/Content/keyboard.css"
                    ,"~/Scripts/datatables/css/dataTables.bootstrap.css"
                    ,"~/Content/font-awesome.min.css"
                    ,"~/Content/nprogress.css"
                    ,"~/Content/jquery.timepicker.min.css"));

            bundles.Add(new StyleBundle("~/Content/DateRangePickerCss").Include(
                    "~/Content/daterangepicker.css"
                ));

            bundles.Add(new ScriptBundle("~/Script/DateRangePickerJs").Include(
                    "~/Scripts/moment.js"
                    ,"~/Scripts/es.js"
                    ,"~/Scripts/modernizr-2.6.2.js"
                    ,"~/Scripts/daterangepicker.js"
                ));
            bundles.Add(new ScriptBundle("~/Script/DatekendoPickerJs").Include(
                    "~/Scripts/js/kendo.all.min.js"
                    ,"~/Scripts/js/kendo.culture.es-ES.min.js"
                ));


            bundles.Add(new StyleBundle("~/Content/WizardCss").Include(
                    /*
                    "~/Content/css/kendo.common-material.min.css"
                    "~/Content/css/kendo.common.min.css"
                    "~/Content/css/kendo.default.min.css"
                    "~/Content/css/kendo.default.mobile.min.css"
                    "~/Content/css/kendo.material.min.css"
                    "~/Content/css/kendo.material.mobile.min.css"
                    */
                    "~/Content/css/bootstrap-material-datetimepicker.css"
                    ,"~/Content/css/jquery-clockpicker.min.css"
                    ,"~/Content/css/asColorPicker.css" 
                    ,"~/Content/css/bootstrap-datepicker.min.css"
                    ,"~/Content/css/bootstrap-timepicker.min.css"
                    ,"~/Content/css/daterangepicker.css"
                ));

            bundles.Add(new ScriptBundle("~/Content/CSS3").Include(
                   "~/Content/TEMPLE/assets/plugins/bootstrap/css/bootstrap.min.css"
                   ,"~/Content/TEMPLE/assets/plugins/chartist-js/dist/chartist.min.css"
                   ,"~/Content/TEMPLE/assets/plugins/chartist-js/dist/chartist-init.css"
                   //"~/Content/TEMPLE/assets/plugins/chartist-plugin-tooltip-master/dist/chartist-plugin-tooltip.css"
                   ,"~/Content/TEMPLE/assets/plugins/c3-master/c3.min.css"
                   ,"~/Content/TEMPLE/css/style.css"
                   ,"~/Content/TEMPLE/css/colors/blue.css"
               ));

            bundles.Add(new ScriptBundle("~/Content/TEMPLE/assets/plugins/ScriptJS").Include(
                   "~/Content/TEMPLE/assets/plugins/bootstrap/js/tether.min.js"
                   ,"~/Content/TEMPLE/assets/plugins/bootstrap/js/bootstrap.min.js"
                   ,"~/Content/TEMPLE/assets/plugins/sticky-kit-master/dist/sticky-kit.min.js"
                   ,"~/Content/TEMPLE/assets/plugins/chartist-js/dist/chartist.min.js"
                   //"~/Content/TEMPLE/assets/plugins/chartist-plugin-tooltip-master/dist/chartist-plugin-tooltip.min.js"
                   ,"~/Content/TEMPLE/assets/plugins/d3/d3.min.js"
                   ,"~/Content/TEMPLE/assets/plugins/c3-master/c3.min.js"
               ));

            bundles.Add(new ScriptBundle("~/Content/TEMPLE/js/AJAXJS").Include(
                   "~/Content/TEMPLE/js/waves.js"
                   ,"~/Content/TEMPLE/js/sidebarmenu.js"
                   //,"~/Content/TEMPLE/js/custom.min.js"
                   ,"~/Content/TEMPLE/js/dashboard1.js"
               ));
            bundles.Add(new StyleBundle("~/Scripts/js/Master").Include(
                    //"~/Scripts/js/kendo.culture.es-ES.min.js"
                    //"~/Scripts/js/MasterRutinas.js"
                    "~/Scripts/js/moment.js"
                    ,"~/Scripts/js/bootstrap-material-datetimepicker.js"
                    ,"~/Scripts/js/jquery-clockpicker.min.js"
                ));
        }
    }
}