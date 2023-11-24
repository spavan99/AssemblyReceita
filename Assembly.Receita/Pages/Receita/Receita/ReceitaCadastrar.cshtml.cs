using Assembly.Database;
using Assembly.Receita.Pages.CSShared;
using Assembly.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace Assembly.Receita.Pages.Receita.Receita
{
    [Authorize(policy: "Usuario")] // Authenticated
    public class ReceitaCadastrarModel : PageModel
    {
        //injeçao de depencia
        private readonly IReceitaRepository _Repository;
        private readonly IReceitaService _Service;

        public ReceitaCadastrarModel(IReceitaRepository Repository, IReceitaService Service)
        {
            _Repository = Repository;
            _Service = Service;
        }


        // ******************************** grupo dados alterar

        private string titulo { get; set; } = "Cadastro de Receita do Site";

        //public Usuario dadosCRU { get; set; } = new Usuario();

        public int nrColunasCad { get; set; } = 3;   // de 1 a 3 colunas

        [BindProperty]
        public DtosReceitaFull novoCadastro { get; set; } = new DtosReceitaFull();

        //rota volta - atual
        public static string rotaVolta { get; set; }
        public string rotaLista { get; set; } = "/Receita/Receita/ReceitaCRUD";
        public string rotaHome { get; set; } = "/Index";


        // fim   ***********************************************

        // titulos 
        public StringBuilder CabecalhoTitulo { get; set; }
        public List<DtosCadastroForm> dadosForm { get; set; } = new();
        public string acaoBTN { get; set; } = "";
        public string descricaoBTN { get; set; } = "";
        private string descricaoTela { get; set; } = "Novo Cadastro";

        // campo reflection dados // pegar configuraçoes tela 
        public ReflectionModel DadosViewModel { get; set; }

        // campo select categoria
        public List<DtosCategoriaFull> selIdCategoria;
        // campo select grau dificuldade
        public List<DtosDificuldadeFull> selIdDificuldade;

        public void OnGet([FromQuery] int? IdOrigem, [FromQuery] string? acaoOrigem)
        {

            if (IdOrigem.HasValue && !string.IsNullOrEmpty(acaoOrigem))
            {
                // rota de volta
                rotaVolta = rotaLista;
                // busca usuario ou monta elementos
                var achou = _Service.GetById<int>(IdOrigem.Value, "Id");
                if (achou.Count == 1)
                {
                    var propriedadesOrigem = achou[0].GetType().GetProperties();
                    var propriedadesDestino = novoCadastro.GetType().GetProperties();
                    foreach (var propriedadeDestino in propriedadesDestino)
                    {
                        var propriedadeOrigem = propriedadesOrigem.FirstOrDefault(p => p.Name == propriedadeDestino.Name);
                        if (propriedadeOrigem != null && propriedadeOrigem.PropertyType == propriedadeDestino.PropertyType)
                        {
                            propriedadeDestino.SetValue(novoCadastro, propriedadeOrigem.GetValue(achou[0]));
                        }
                    }
                }

                //bsucar dados categoria
                ICategoriaService lstcat = new CategoriaService(new CategoriaRepository());
                selIdCategoria = lstcat.GetAll();

                //bsucar dados grau dificuldade
                IDificuldadeService lstdif = new DificuldadeService(new DificuldadeRepository());
                selIdDificuldade = lstdif.GetAll();

                acaoBTN = acaoOrigem;
                if (acaoBTN.Equals("INSERT"))
                {
                    descricaoBTN = "Salvar / Gravar";
                }
                else if (acaoBTN.Equals("UPDATE"))
                {
                    descricaoBTN = "Salvar Alteraçoes";
                    descricaoTela = "Alterar Cadastro";
                }
                else if (acaoBTN.Equals("DELETE"))
                {
                    descricaoBTN = "Confirma Cancelar / Apagar";
                    descricaoTela = "Apagar/Deletar o Cadastro";
                }
                else if (acaoBTN.Equals("VIEW"))
                {
                    descricaoBTN = "Dados da Verificação";
                    descricaoTela = "Visualizar o Cadastro";
                }
            }
            else
            {
                IdOrigem = 0;
                acaoBTN = "INSERT";
                descricaoBTN = "Salvar/Gravar";
                rotaVolta = rotaHome;
            }

            // DTOS arquivo a ser utiliza grud - montar a tela
            DtosReceitaFull obj = new DtosReceitaFull();
            DadosViewModel = new ReflectionModel(obj);

            // titulo 
            CabecalhoTitulo = new CabTituloCRUD().start(titulo, descricaoTela);

            // viewData
            ViewData["nrColunasCad"] = nrColunasCad;
            ViewData["DadosViewModel"] = DadosViewModel;
            ViewData["voltaBTN"] = "Volta";
            ViewData["acaoBTN"] = acaoBTN;
            ViewData["descricaoBTN"] = descricaoBTN;
            // parse dinamica
            // casting lista dinamica
            //var cadDynamic = (dynamic)novoCadastro;
            ViewData["novoCadastro"] = novoCadastro;

            ViewData["selIdCategoria"] = selIdCategoria;

        }
        public void OnPost() { }
        public IActionResult OnPostCadastraFormulario()
        {
            string _msg = "";
            string botaoClicado = Request.Form["BtCadFormulario"];

            if (!string.IsNullOrEmpty(botaoClicado))
            {
                if (botaoClicado.Equals("INSERT"))
                {
                    if (novoCadastro is not null)
                    {   // fotoreceita em colocar "" pois nao permite gravar null
                        novoCadastro.fotoreceita = "";
                        // obter numero usuario
                        int userLog = int.Parse(User.FindFirst("id").Value);
                        novoCadastro.IdUser= userLog;
                        var ok = _Service.AddFull(novoCadastro);
                        _msg = ok;
                    }

                }
                else if (botaoClicado.Equals("DELETE"))
                {
                    if (novoCadastro is not null)
                    {
                        var ok = _Service.Delete(novoCadastro.Id);
                        if (ok)
                        {
                            _msg = "Deletado/Cancelado com sucesso";
                        }
                        else { _msg = "Não Deletado/Cancelado"; }

                    }
                }
                else if (botaoClicado.Equals("UPDATE"))
                {
                    if (novoCadastro is not null)
                    {
                        var ok = _Service.UpdateFull(novoCadastro);
                        if (ok)
                        {
                            _msg = "Alterado com sucesso";
                        }
                        else { _msg = "Nao Alterado / Erro Altercao / Categoria Errada / Dificuldade Errada / ETC"; }
                    }
                }
                else if (botaoClicado.Equals("VIEW"))
                {
                    _msg = "";
                }
                else if (botaoClicado.Equals("VOLTAR"))
                {
                    _msg = "";
                    return RedirectToPage(rotaVolta);
                }
            }
            else
            { _msg = "Erro geral!!!"; }

            TempData["My9Mensagem"] = _msg;
            return RedirectToPage(rotaVolta);

        }


    }
}
