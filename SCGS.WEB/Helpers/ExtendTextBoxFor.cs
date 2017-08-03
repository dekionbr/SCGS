using SCGS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace SCGS.WEB.Helpers
{
    public static class ExtendTextBoxFor
    {
        /// <summary>
        /// Formata o campo de texto no padrão do tema
        /// 
        /// <div class="input-group">
        ///     <span class="input-group-addon" id="lbl-name">
        ///         <label for="IdInput">Label:</label>
        ///     </span>
        ///     <input id="IdInput" class="form-control" type="text" placeholder="my placeholder" aria-describedby="lbl-name" />
        /// </div>
        /// 
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static MvcHtmlString MyTextBoxFor<TModel, TProperty>(
        this HtmlHelper<TModel> helper,
        Expression<Func<TModel, TProperty>> expression,
        string label,
        string placeholder,
        object htmlAttributes = null,
        bool disabled = false
        )

        {
            MemberExpression member = (MemberExpression)expression.Body;
            var attributes = new RouteValueDictionary(htmlAttributes);

            if (disabled)
                attributes["disabled"] = "disabled";


            if(attributes["class"] == null)
                 attributes["class"] = "form-control";

            attributes["aria_describedby"] = "lbl-" + member.Member.Name;
            attributes["placeholder"] = placeholder;


            return MvcHtmlString.Create(string.Concat("<div class=\"input-group\">\n",
                                        string.Format("    <span class=\"input-group-addon\" id=\"lbl-{0}\">\n", member.Member.Name),
                                        string.Format("        <label for=\"{0}\">{1}:</label>\n", member.Member.Name, label),
                                                      "    </span>\n",
                                                         helper.TextBoxFor(expression, attributes).ToString() + "\n",
                                                      "</div>")
                                                      
                                                      );
        }





        /// <summary>
        /// 
        /// Formata o campo de select no padrão do tema apartir de um enum
        /// 
        /// <div class="input-group">
        ///     <span class="input-group-addon" id="lbl-name">
        ///         <label for="IdSelect">Label:</label>
        ///     </span>
        ///     <select id="IdSelect" class="form-control" aria-describedby="lbl-name">
        ///         <option>options</option>
        ///     </select>
        /// </div>
        ///
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expression"></param>
        /// <param name="label"></param>
        /// <param name="placeholder"></param>
        /// <returns></returns>

        public static MvcHtmlString MyDropDownListFor<TModel, TEnum>(
        this HtmlHelper<TModel> helper,
        Expression<Func<TModel, TEnum>> expression,
        string label,
        string placeholder,
        object htmlAttributes = null
        )
        {


            MemberExpression member = (MemberExpression)expression.Body;
            var attributes = new RouteValueDictionary(htmlAttributes);


            if (attributes["class"] == null)
                attributes["class"] = "form-control";

            attributes["aria_describedby"] = "lbl-" + member.Member.Name;
            attributes["placeholder"] = placeholder;


            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            IEnumerable<TEnum> values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();

            if (Enum.GetName(typeof(TEnum), 8) == TipoFuncionario.Funcionario.ToString())
                values = values.Take(7);

            IEnumerable<SelectListItem> items =
                values.Select(value => new SelectListItem
                {
                    Text = value.ToString(),
                    Value = value.ToString(),
                    Selected = value.Equals(metadata.Model)
                });

            return MvcHtmlString.Create(string.Concat("<div class=\"input-group\">\n",
                                        string.Format("    <span class=\"input-group-addon\" id=\"lbl-{0}\">\n", member.Member.Name),
                                        string.Format("        <label for=\"{0}\">{1}:</label>\n", member.Member.Name, label),
                                                      "    </span>\n",
                                                         helper.DropDownListFor(expression, items, placeholder, attributes).ToString() + "\n",
                                                      "</div>"));
        }













        /// <summary>
        /// 
        /// Formata o campo de select no padrão do tema apartir de um enum
        /// 
        /// <div class="input-group">
        ///     <span class="input-group-addon" id="lbl-name">
        ///         <label for="IdSelect">Label:</label>
        ///     </span>
        ///     <select id="IdSelect" class="form-control" aria-describedby="lbl-name">
        ///         <option>options</option>
        ///     </select>
        /// </div>
        ///
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expression"></param>
        /// <param name="label"></param>
        /// <param name="placeholder"></param>
        /// <returns></returns>

        public static MvcHtmlString MyDropDownListForEntity<TModel, TProperty>(
        this HtmlHelper<TModel> helper,
         Expression<Func<TModel, TProperty>> expression,
        IEnumerable<SelectListItem> items,
        string label,
        string placeholder)
        {
            return MvcHtmlString.Create(string.Concat("<div class=\"input-group\">\n",
                                        string.Format("    <span class=\"input-group-addon\" id=\"lbl-{0}\">\n", label),
                                        string.Format("        <label for=\"{0}\">{1}:</label>\n", label, label),
                                                      "    </span>\n",
                                                         helper.DropDownListFor(expression, items, placeholder, new
                                                         {
                                                             @class = "form-control",
                                                             @aria_describedby = "lbl-" + label,
                                                             @title = placeholder
                                                         }).ToString() + "\n",
                                                      "</div>"));
        }






    }
}