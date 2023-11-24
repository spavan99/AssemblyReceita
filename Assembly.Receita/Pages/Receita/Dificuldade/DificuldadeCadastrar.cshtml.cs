using Assembly.Database;
using Assembly.Receita.Pages.CSShared;
using Assembly.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace Assembly.Receita.Pages.Receita.Dificuldade
{
    [Authorize(policy: "Admin")] // Authenticated

    public class DificuldadeCadastrarModel : PageModel
    {

        //injeçao de depencia
        private readonly IDificuldadeRepository _Repository;
        private readonly IDificuldadeService _Service;

        public DificuldadeCadastrarModel(IDificuldadeRepository Repository, IDificuldadeService Service)
        {
            _Repository = Repository;
            _Service = Service;
        }


        // ******************************** grupo dados alterar

        private string titulo { get; set; } = "Cadastro de Grau de Dificuldade da Receita";

        //public Usuario dadosCRU { get; set; } = new Usuario();

        public int nrColunasCad { get; set; } = 1;   // de 1 a 3 colunas

        [BindProperty]
        public DtosDificuldadeFull novoCadastro { get; set; } = new DtosDificuldadeFull();

        //rota volta - atual
        public static string rotaVolta { get; set; }
        public string rotaLista { get; set; } = "/Receita/Dificuldade/DificuldadeCRUD";
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


        public void OnGet([FromQuery] int? IdOrigem, [FromQuery] string? acaoOrigem)
        {

            //ver como pegar paramentro vcindo do outro formular  
            // origem  imput   Request.Form["suavariavelorigem"]
            // origme  <a> #href  Request.Query["suavaraivelorigem"]

            if (IdOrigem.HasValue && !string.IsNullOrEmpty(acaoOrigem))
            {
                // rota de volta
                rotaVolta = rotaLista;
                // busca usuario ou monta elementos
                var achou = _Service.GetById<int>(IdOrigem.Value, "Id");
                if (achou.Count == 1)
                {
                    // parse para dtosfull  fazer automaitoc
                    novoCadastro.Id = achou[0].Id;
                    novoCadastro.GrauDificuldade = achou[0].GrauDificuldade;
                }

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
            DtosDificuldadeFull obj = new DtosDificuldadeFull();
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
                    {
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
                        else { _msg = "Nao Alterado"; }
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
