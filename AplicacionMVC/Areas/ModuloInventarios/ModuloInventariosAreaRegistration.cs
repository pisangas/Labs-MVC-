using System.Web.Mvc;

namespace AplicacionMVC.Areas.ModuloInventarios
{
    public class ModuloInventariosAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ModuloInventarios";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ModuloInventarios_default",
                "ModuloInventarios/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}