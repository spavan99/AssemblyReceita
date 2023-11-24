using Assembly.Database;
using Assembly.Receita.Pages.CSShared;
using Assembly.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace Assembly.Receita.Pages.Receita.ReceitaItens
{
    [Authorize(policy: "Usuario")] // Authenticated
    public class ReceitaItensCadastrarModel : PageModel
    {

        //injeçao de depencia
        private readonly IItensReceitaRepository _Repository;
        private readonly IItensReceitaService _Service;
        public ReceitaItensCadastrarModel(IItensReceitaRepository Repository, IItensReceitaService Service)
        {
            _Repository = Repository;
            _Service = Service;
        }

        // ******************************** grupo dados alterar
        private string titulo { get; set; } = "Ingredientes";
        public int nrColunasCad { get; set; } = 2;   // de 1 a 3 colunas

        [BindProperty]
        public DtosItensReceitaFull novoCadastro { get; set; } = new DtosItensReceitaFull();

        //rota volta - atual
        public static string rotaVolta { get; set; } = "/Receita/ReceitaItens/ReceitaItensCRUD";
        public string rotaLista { get; set; } = "/Receita/ReceitaItens/ReceitaItensCRUD";
        public string rotaHome { get; set; } = "/Index";

        public int IdReceitaOrigem { get; set; }
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

            if (IdOrigem.HasValue && !string.IsNullOrEmpty(acaoOrigem))
            {
                if(IdOrigem.Value == 0)
                {
                    IdReceitaOrigem = (int)TempData["MinhaChave"];
                }
                else
                {
                    IdReceitaOrigem = IdOrigem.Value;
                }
                TempData["MinhaChavePost"] = IdReceitaOrigem;

                // busca usuario ou monta elementos
                var achou = _Service.GetById<int>(IdOrigem.Value, "Id");
                if (achou.Count == 1)
                {
                    // parse para dtosfull  fazer automaitoc
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

                    IdReceitaOrigem = novoCadastro.IdReceita;
                    // Armazena IdReceitaOrigem no TempData
                    TempData["IdReceitaOrigem"] = novoCadastro.IdReceita;
                    TempData["OldReceitaOrigem"] = novoCadastro.IdReceita;
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
                //IdOrigem = 0;
                acaoBTN = "INSERT";
                descricaoBTN = "Salvar/Gravar";
                rotaVolta = rotaHome;
            }

            // DTOS arquivo a ser utiliza grud - montar a tela
            DtosItensReceitaFull obj = new DtosItensReceitaFull();
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
            
            int IdReceitaOrigemLocal = 0;
            if (TempData["MinhaChavePost"] is int MinhaChavePost)
            {   // Move o valor para uma variável local
                IdReceitaOrigemLocal = MinhaChavePost;
            }

            if (!string.IsNullOrEmpty(botaoClicado))
            {
                if (botaoClicado.Equals("INSERT"))
                {
                    if (novoCadastro is not null)
                    {
                        // ver como pegar
                        novoCadastro.IdReceita = IdReceitaOrigemLocal;
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
                    return RedirectToPage(rotaVolta, new { IdOrigem = IdReceitaOrigemLocal });
                }
            }
            else
            { _msg = "Erro geral!!!"; }

            TempData["My9Mensagem"] = _msg;
            return RedirectToPage(rotaVolta, new { IdOrigem = IdReceitaOrigemLocal });
            //return RedirectToPage(rotaVolta);

        }

    }
}
