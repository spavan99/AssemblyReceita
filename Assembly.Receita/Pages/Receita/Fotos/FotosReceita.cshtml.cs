using Assembly.Database;
using Assembly.Receita.Pages.CSShared;
using Assembly.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace Assembly.Receita.Pages.Receita.Fotos
{
    [Authorize(policy: "Usuario")] // Authenticated

    public class FotosReceitaModel : PageModel
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IReceitaRepository _Repository;
        private readonly IReceitaService _Service;

        public FotosReceitaModel(IWebHostEnvironment webHostEnvironment, IReceitaRepository Repository, IReceitaService Service)
        {
            _webHostEnvironment = webHostEnvironment;
            _Repository = Repository;
            _Service = Service;
        }

        // titulos
        private string titulo { get; set; } = "Fotos da Receita - << INCLUIR >>";

        // titulo do cadastro injeta html
        public StringBuilder CabecalhoTitulo { get; set; }
        public int receitaAtiva { get; set; }


        [BindProperty]
        public IFormFile ImageFile { get; set; }
        //string targetPath = "../FotosReceita/";
        //string targetPath = "/receitasfotos/";


        public void OnGet([FromQuery] int? IdOrigem, [FromQuery] string? acaoOrigem)
        {

            receitaAtiva = IdOrigem.Value;
            // titulo 
            CabecalhoTitulo = new CabTituloCRUD().start(titulo);
            TempData["ValorDaReceita"] = receitaAtiva;


        }

        public async Task<ActionResult> OnPostAsync()
        {
            string nomeArquivoGerado = "";
            try
            {
                object fileObj = Request.Form.Files[0];
                string fileName = ((FormFile)fileObj).FileName;
                string fileExtention = Path.GetExtension(fileName);
                string newFileName = Guid.NewGuid().ToString() + fileExtention;
                //string fileDestination = Path.Combine(_webHostEnvironment.ContentRootPath + "/FotosReceita", newFileName);
                string fileDestination = Path.Combine( _webHostEnvironment.WebRootPath, "receitasfotos", newFileName);


                if (ImageFile != null && ImageFile.Length > 0)
                {
                    using (Stream stream = new FileStream(fileDestination, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream); // Save file in folders

                        // Obtém o nome do arquivo gerado
                        FileInfo fileInfo = new FileInfo(fileDestination);
                        nomeArquivoGerado = fileInfo.Name;
                    }

                    if (TempData["ValorDaReceita"] is int valorReceita)
                    {
                        int receitaAtual = valorReceita;
                        var dadosReceita = _Service.GetById<int>(receitaAtual, "Id");

                        // converte para fazer update parse
                        DtosReceitaFull novoCadastro = new DtosReceitaFull();
                        var propriedadesOrigem = dadosReceita[0].GetType().GetProperties();
                        var propriedadesDestino = novoCadastro.GetType().GetProperties();
                        foreach (var propriedadeDestino in propriedadesDestino)
                        {
                            var propriedadeOrigem = propriedadesOrigem.FirstOrDefault(p => p.Name == propriedadeDestino.Name);
                            if (propriedadeOrigem != null && propriedadeOrigem.PropertyType == propriedadeDestino.PropertyType)
                            {
                                propriedadeDestino.SetValue(novoCadastro, propriedadeOrigem.GetValue(dadosReceita[0]));
                            }
                        }
                        novoCadastro.fotoreceita = nomeArquivoGerado;
                        //update
                        _Service.UpdateFull(novoCadastro);

                    }
                }

            }
            catch (Exception ex)
            {
            }
            return new RedirectToPageResult("/Receita/Receita/ReceitaCRUD");
        }

    }
}
