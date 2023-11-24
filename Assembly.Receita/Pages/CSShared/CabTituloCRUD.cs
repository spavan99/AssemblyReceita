using System.Text;

namespace Assembly.Receita.Pages.CSShared
{
    public class CabTituloCRUD
    {
        public StringBuilder HtmlPage { get; set; } = new StringBuilder();


        public StringBuilder start(string nTitulo, string subTitulo = null)
        {
      
            if( subTitulo is null)
            {
                // HtmlPage.Append("<div class=\"container-fluid mx-1 px-1\">");
                HtmlPage.Append("<div class=\"container-fluid row col-md-12 mx-0 my-0 px-1 py-2 borda-principal Listagem-Titulo\">");
                HtmlPage.Append("<h3 id = \"pad-listagem-1\"  name=\"titulo-cadatro\" style=\"text-align: center\">" + nTitulo + "</h3>");
                HtmlPage.Append("</div>");
                //HtmlPage.Append("</div>");
            }
            else
            {
                // mehporar o layout
                HtmlPage.Append("<div class=\"container-fluid row mx-0 my-0 px-1 py-2 borda-principal Listagem-Titulo\">");

                //HtmlPage.Append("<div class=\"col-md-7 mx-0 my-0 px-0 py-0 borda-principal Listagem-Titulo\">");
                HtmlPage.Append("<div class=\"col\">");

                HtmlPage.Append("<h3 id = \"pad-listagem-1\"  name=\"titulo-cadatro\" style=\"text-align: center\">" + nTitulo + "</h3>");
                HtmlPage.Append("</div>");

                //HtmlPage.Append("<div class=\"col-md-4 mx-0 my-0 px-0 py-0 borda-principal Listagem-Titulo\">");
                HtmlPage.Append("<div class=\"col\">");

                HtmlPage.Append("<h4 id = \"pad-listagem-1\"  name=\"titulo-cadatro\" style=\"text-align: right\">" + subTitulo + "</h4>");
                HtmlPage.Append("</div>");

                HtmlPage.Append("</div>");

            }

            return HtmlPage;
        }


    }
}
