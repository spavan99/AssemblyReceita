using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace Assembly.Receita.Pages.Receita.Teste
{


    public class teste2Model : PageModel
    {

        public StringBuilder HtmlPage { get; set; }
    
        
        public List<string> Data { get; set; } = new List<string>();
        
        public void OnGet()
        {

            Data.Add("teste 1");
            Data.Add("teste 2");
            Data.Add("teste 3");
            Data.Add("teste 4");
            Data.Add("teste 5");

            HtmlPage = new StringBuilder();

            HtmlPage.Append("<h1 class='alfa'>Minha Pagian teste </h1>");

            // HtmlPage.Append("<form method='post'  asp-page=" asp-page=\"onPost\" >");
            HtmlPage.Append("<form  style=\"padding: 5px\" method=\"post\"  asp-page=\"Receita\\Teste\\teste2\">");

            HtmlPage.Append("<h2 > Teste metodo onpost </h2>");

            HtmlPage.Append("<table>");
            for (int i = 0; i < 5; i++)
            {
                
                HtmlPage.Append("<tr>");
                for (int j = 0; j < 3; j++)
                {
 
                    HtmlPage.Append("<td> <a href='https://www.w3schools.com'> " +
                             Data[i] + " </ a> </td>");


                    //HtmlPage.Append("<td>" +
                    //    Data[i] + "</td>");
                }
                HtmlPage.Append("</tr>");
            }
            HtmlPage.Append("</table>");


            HtmlPage.Append("<button type='submit' class='btn btn-primary' >Aprovar Publicação</button>");
            HtmlPage.Append("<button type='submit' class='btn' >  <a href='https://www.w3schools.com'> teste site </a> </button>");


            HtmlPage.Append("<button type=\"submit\" class=\"btn btn-primary\" name=\"action\" value=\"OnPostAprovar\">Enviar e Aprovar</button>");
            HtmlPage.Append("<button type=\"submit\" name=\"submitButton\" value=\"submitButton\">Enviar Formulário</button>");

            // HtmlPage.Append("<button type='submit' class='btn btn-primary' name='action' value='Aprovar'>Aprovar Publicação</button>");
            // HtmlPage.Append("<input type='submit' name='action' value='Enviar' />");

            HtmlPage.Append("</form>");


            //return Page();



        }

        public void OnPostAprovar()
        {

            Console.WriteLine("apronvar  estou aqui");
            Console.ReadLine();


            // Lógica para ação de aprovação
        }

        [HttpPost]
        public void OnPost() {


            Console.WriteLine("estou aqui");
            Console.ReadLine();
        
        }

        [Route("submitButton")]
        public IActionResult submitButton()
        {
            // Lógica para lidar com a submissão do formulário
            // Por exemplo, processar os dados do formulário e redirecionar para outra página
            return RedirectToPage("/OutraPagina");
        }




    }
}
