using KSystem.Model.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace KSystem
{
    public static class CustomHtmlHelpers
    {
        public static MvcHtmlString ImageCheckbox<TModel, TValue>(this 
                 HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string checkboxClassName)
        {
            TagBuilder Checkbox = new TagBuilder("div");
            TagBuilder HidenInput = new TagBuilder("input");
            var name = ExpressionHelper.GetExpressionText(expression);
            var value = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            if ((int)value.Model == 1)
            {
                Checkbox.AddCssClass(checkboxClassName + " on");
            }
            else
            {
                Checkbox.AddCssClass(checkboxClassName + " off");
            }
            Checkbox.Attributes["name"] = name.ToString();
            Checkbox.Attributes["id"] = name.ToString();
            Checkbox.InnerHtml = HidenInput.ToString();
            HidenInput.Attributes["name"] = name.ToString();
            HidenInput.Attributes["id"] = name.ToString();
            HidenInput.Attributes["value"] = value.Model.ToString();
            HidenInput.Attributes["type"] = "hidden";
            Checkbox.InnerHtml = HidenInput.ToString();
            return MvcHtmlString.Create(Checkbox.ToString());
        }

        public static MvcHtmlString Lock(this HtmlHelper html, int value)
        {
            TagBuilder Lock = new TagBuilder("div");
            TagBuilder IconContainer = new TagBuilder("div");
            IconContainer.AddCssClass("iconContainer");
            if (value == 1)
            {
                Lock.AddCssClass("lock on");
            }
            else if (value == 0)
            {
                Lock.AddCssClass("lock off");
            }
            IconContainer.InnerHtml = Lock.ToString();
            return MvcHtmlString.Create(IconContainer.ToString());
        }

        public static MvcHtmlString Online(this HtmlHelper html, int value)
        {
            TagBuilder Status = new TagBuilder("div");
            if (value == 1)
            {
                Status.AddCssClass("status online");
            }
            else if (value == 0)
            {
                Status.AddCssClass("status offline");
            }
            return MvcHtmlString.Create(Status.ToString());
        }
        public static MvcHtmlString ActionImage(this HtmlHelper html, string url, string imageClass, string description)
        {
            TagBuilder Link = new TagBuilder("a");
            TagBuilder DivImage = new TagBuilder("div");
            TagBuilder DivDescription = new TagBuilder("div");
            Link.AddCssClass("item");
            Link.MergeAttribute("href", url);
            DivImage.AddCssClass(imageClass);
            DivDescription.AddCssClass("description");
            DivDescription.InnerHtml = description;
            Link.InnerHtml = String.Concat(DivImage.ToString(), DivDescription.ToString());
            return MvcHtmlString.Create(Link.ToString());
        }

        public static MvcHtmlString InputDate<TModel, TValue>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expressionDate,
            Expression<Func<TModel, TValue>> expressionHourse,
            Expression<Func<TModel, TValue>> expressionMinute)
        {
            var nameDate = ExpressionHelper.GetExpressionText(expressionDate);
            var nameHourse = ExpressionHelper.GetExpressionText(expressionHourse);
            var nameMinute = ExpressionHelper.GetExpressionText(expressionMinute);
            TagBuilder containerDiv = new TagBuilder("div");
            TagBuilder iconDiv = new TagBuilder("div");
            TagBuilder labelForHourse = new TagBuilder("label");
            TagBuilder labelForMinute = new TagBuilder("label");

            TagBuilder script = new TagBuilder("script");
            StringBuilder scriptCode = new StringBuilder();
            scriptCode.Append("$('#" + nameDate.Replace('.', '_').ToString() + "').datepicker({ dateFormat: 'dd.mm.yy', changeMonth: true });");
            scriptCode.Append("$('#containerFor" + nameDate.Replace('.', '_').ToString() + " .icon').bind('click', function () {$('#" + nameDate.Replace('.', '_').ToString() + "').datepicker('show')})");
            script.InnerHtml = scriptCode.ToString();

            iconDiv.AddCssClass("icon");
            iconDiv.InnerHtml = new TagBuilder("div").ToString();
            labelForHourse.AddCssClass("gap");
            labelForMinute.AddCssClass("gap");
            labelForHourse.InnerHtml = "ч";
            labelForMinute.InnerHtml = "мин";
            containerDiv.Attributes["id"] = "containerFor" + nameDate.Replace('.', '_').ToString();
            containerDiv.AddCssClass("inputDateContainer");
            StringBuilder htmlCode = new StringBuilder();
            htmlCode.Append(System.Web.Mvc.Html.InputExtensions.TextBoxFor(html, expressionDate, new { @class = "inputDate", ReadOnly = false }).ToString());
            htmlCode.Append(iconDiv);
            htmlCode.Append(labelForHourse);
            htmlCode.Append(System.Web.Mvc.Html.SelectExtensions.DropDownListFor(html, expressionHourse, new SelectList(KSystem.Function.DropDownItems.HoursList(), nameHourse), new { @class = "inputTime" }).ToString());
            htmlCode.Append(labelForMinute);
            htmlCode.Append(System.Web.Mvc.Html.SelectExtensions.DropDownListFor(html, expressionMinute, new SelectList(KSystem.Function.DropDownItems.HoursList(), nameMinute), new { @class = "inputTime" }).ToString());
            containerDiv.InnerHtml = htmlCode.ToString();
            return MvcHtmlString.Create(String.Concat(containerDiv.ToString(), script.ToString()));
        }

        public static MvcHtmlString Pagination(this HtmlHelper html, string action, PageInfo pageInfo)
        {
            if (pageInfo.TotalPages > 1)
            {
                //начальный индекс отрисовки плиток
                int index = 1;
                TagBuilder script = new TagBuilder("script");
                StringBuilder scriptCode = new StringBuilder();
                scriptCode.Append("$('.paginationPage').bind('click', function() { $(this).addClass('current').siblings().removeClass('current'); $('#forReportBlock').load('Report/" + action + "', { numPage: $(this).html() }) });");
                scriptCode.Append("$('#paginationPageLeft').bind('click', function () {$('#forReportBlock').load('Report/" + action + "', { numPage: $(this).next('.paginationPage').html() }) });");
                scriptCode.Append("$('#paginationPageRight').bind('click', function () {$('#forReportBlock').load('Report/" + action + "', { numPage: $(this).prev('.paginationPage').html() }) });");
                script.InnerHtml = scriptCode.ToString();
                TagBuilder paginationDiv = new TagBuilder("div");
                paginationDiv.AddCssClass("pagination");
                StringBuilder pagContainerString = new StringBuilder();
                TagBuilder pagLeft = new TagBuilder("a");
                pagLeft.AddCssClass("paginationPageArrow");
                pagLeft.Attributes["id"] = "paginationPageLeft";
                pagLeft.InnerHtml = "<img src='/Image/pagination/pagination-arrow-left.png' />";
                if (pageInfo.PageNumber >= pageInfo.VisiblePages)
                {
                    pagLeft.Attributes["style"] = "display:inline-block";
                }
                else
                {
                    pagLeft.Attributes["style"] = "display:none";
                }
                pagContainerString.Append(pagLeft);

                //статус отображения правой стрелочки
                bool pagRightFlag = false;
                //определяем есть ли среди оставшихся плиток переходная если есть то рисуем стрелочку
                for (var i = pageInfo.PageNumber; i <= pageInfo.TotalPages; i++)
                {
                    int transitionPage = 1;
                    if (i < pageInfo.TotalPages - 1)
                        transitionPage = ((i + 1) - pageInfo.VisiblePages) % (pageInfo.VisiblePages - 2);
                    if (transitionPage == 0)
                        pagRightFlag = true;
                }
                if (pageInfo.TotalPages > pageInfo.VisiblePages)
                {
                    //являеться ли страница переходной для пагинации 0 - является;
                    int transitionPage = (pageInfo.PageNumber - pageInfo.VisiblePages) % (pageInfo.VisiblePages - 2);
                    //если переходная
                    if (transitionPage == 0)
                    {
                        //назначаем начальный индекс отрисовки плиток пагинации , берем предыдуший от текущией плитки(так как она является переходной)
                        if (pageInfo.PageNumber < pageInfo.TotalPages)
                            index = pageInfo.PageNumber - 1;
                        else
                        {
                            index = pageInfo.PageNumber - (pageInfo.VisiblePages - 1);
                        }
                    }
                    else
                    {
                        //исключаем первые начальные плитки
                        if (pageInfo.PageNumber > pageInfo.VisiblePages)
                        {
                            index = 1;
                            //Взависимости от кол отображаемых плиток, проходим итерацией
                            for (var i = 0; i < pageInfo.VisiblePages - 3; i++)
                            {
                                //определяем стартовую плитку каждую итерацию отнимаю все большее число от текущей плитки
                                int transitionPageForIterarion = ((pageInfo.PageNumber - (i + 1)) - pageInfo.VisiblePages) % (pageInfo.VisiblePages - 2);
                                //найдя нужную итерацию определяем стартовый индекс
                                if (transitionPageForIterarion == 0)
                                {
                                    index = pageInfo.PageNumber - (i + 2);
                                }
                            }
                        }
                        //иначе стартовый инедкс = 1
                        else
                        {
                            index = 1;
                        }
                    }
                }
                for (var i = index; i <= pageInfo.TotalPages && i < pageInfo.VisiblePages + index; i++)
                {
                    TagBuilder pag = new TagBuilder("a");
                    if (i == pageInfo.PageNumber)
                    {
                        pag.AddCssClass("paginationPage current");
                    }
                    else
                    {
                        pag.AddCssClass("paginationPage");
                    }
                    pag.InnerHtml = i.ToString();
                    pagContainerString.Append(pag);
                }
                TagBuilder pagRight = new TagBuilder("a");
                pagRight.AddCssClass("paginationPageArrow");
                pagRight.Attributes["id"] = "paginationPageRight";
                if (pageInfo.TotalPages > pageInfo.VisiblePages && pagRightFlag)
                {
                    pagRight.Attributes["style"] = "display:inline-block";
                }
                else
                {
                    pagRight.Attributes["style"] = "display:none";
                }
                pagRight.InnerHtml = "<img src='/Image/pagination/pagination-arrow-right.png' />";
                pagContainerString.Append(pagRight);
                paginationDiv.InnerHtml = String.Concat(pagContainerString.ToString(),script.ToString());
                return MvcHtmlString.Create(paginationDiv.ToString());
            }
            else
            {
                return null;
            }
        }
    }
}