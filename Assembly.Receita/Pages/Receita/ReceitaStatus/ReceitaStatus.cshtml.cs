using Assembly.Database;
using Assembly.Domain;
using Assembly.Receita.Pages.CSShared;
using Assembly.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace Assembly.Receita.Pages.Receita.ReceitaStatus
{
    [Authorize(policy: "Admin")] // Authenticated

    public class ReceitaStatusModel : PageModel
    {

        private readonly IReceitaRepository _Repository;
        private readonly IReceitaService _Service;
        public ReceitaStatusModel(IReceitaRepository Repository, IReceitaService Service)
        {
            _Repository = Repository;
            _Service = Service;
        }

        public List<DtosReceitaFull> dadosConsulta { get; set; } = new List<DtosReceitaFull>();
        public DtosReceitaFull novoCadastro { get; set; } = new DtosReceitaFull();

        public static int filtroComentarios { get; set; }
        public string escolha { get; set; }

        // titulo do cadastro injeta html
        public StringBuilder CabecalhoTitulo { get; set; }
        public string titulo { get; set; } = "Alteraçao Status da Receita";

        // campos pesquisa html
        public List<SQLDTOSPesquisa> PesCampo { get; set; } = new();

        // campos pesquisa html
        public List<SQLDTOSPesquisa> PesFinal { get; set; } = new();

        [BindProperty]
        public string TipoFiltro { get; set; }

        // ********* varaiveis static para pode funcionar todos o paginas recaregadas do lado do servidor
        // dados  a sere pesquisadp 
        public static string DadosPesquisar { get; set; }

        [BindProperty]
        public string DadosFiltroPesquisa { get; set; }

        // filtro pesquisa final zero todos itens  1 conforme selecao
        public static int filtroGeral;
        // item pesquisa selecionaodo
        public static int selecao;

        public string rotaPagina = "/Receita/ReceitaStatus/ReceitaStatus";
        public void OnGet()
        {

            //**************************************************** alterar os campos para pesquisa e filtro
            // adciona campos para pesquisa  primeiro
            SQLDTOSPesquisa pesq1 = new SQLDTOSPesquisa();
            pesq1.descPesquisa = "Nr Receita";
            pesq1.nCampo = "id";
            pesq1.nType = SQLtypeEnum.type_int;
            pesq1.nValor = DadosPesquisar;
            pesq1.operacao = SQLoperEnum.p_fim;
            PesCampo.Add(pesq1);

            // FIM  **************************************************** alterar os campos para pesquisa e filtro

            if (filtroComentarios == 0)
            { filtroComentarios = 4; }

            dadosConsulta = _Service.GetById<int>(filtroComentarios, "status");
            //dadosConsulta = novoRep.GetById<int>(0, null);

            // titulo 
            CabecalhoTitulo = new CabTituloCRUD().start(titulo);

            // viwdata alimentar tela carregada // para o formulario de consulta e lista paginas
            ViewData["PesCampo"] = PesCampo;
            ViewData["rotaLista"] = rotaPagina;
            ViewData["PaginaAtual"] = 1;
            ViewData["NumTotaldePag"] = 1;

        }

        public void OnPost()
        {

        }

        public IActionResult OnPostAprovar(int IdAlterar, ReceitaStatusEnum  Status)
        {
            // status 1 passa para 2
            // status 2 para para 3
            // status 3 para para 4
            // status 4 para 1
            ReceitaStatusEnum nvalor;
            
            if (Status == ReceitaStatusEnum.Nao_Aprovada)
            {
                nvalor = ReceitaStatusEnum.Aguardando;
            }
            else if (Status == ReceitaStatusEnum.Aguardando)
            {
                nvalor = ReceitaStatusEnum.Aprovada;

            }else if (Status == ReceitaStatusEnum.Aprovada)
            {
                nvalor = ReceitaStatusEnum.Publicada;
            }
            else
            {
                nvalor = ReceitaStatusEnum.Nao_Aprovada;
            }
            // pegar a receita atual
            var achou = _Service.GetById<int>(IdAlterar, "id");
            if( achou.Count == 1)
            {
                //DtosReceitaFull novoCadastro = new DtosReceitaFull();
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
                // altera status
                novoCadastro.Status = nvalor;
                // colcoar porque cadastro tinha null campos erro criacao dados pode tirar nao necessario
                if( novoCadastro.fotoreceita is null)
                {
                    novoCadastro.fotoreceita = "";
                }
                //grava receita
                _Service.UpdateFull(novoCadastro);

            }
            return RedirectToPage(rotaPagina);

        }

        public IActionResult OnPostMudarSelecao(string escolha)
        {

            if (escolha == "OK1")
            {
                filtroComentarios = 1;
            }
            else if (escolha == "OK2")
            {
                filtroComentarios = 2;
            }
            else if (escolha == "OK3")
            {
                filtroComentarios = 3;
            }else if( escolha == "OK4")
            {
                filtroComentarios = 4;
            }
            return RedirectToPage(rotaPagina);
        }



        public IActionResult OnPostPesquisaPadrao(string TipoFiltro)
        {
            string botaoClicado = Request.Form["btn9PesquisarFiltro"];

            if (!string.IsNullOrEmpty(botaoClicado))
            {
                if (DadosFiltroPesquisa is not null)
                {
                    filtroGeral = 1;
                    selecao = int.Parse(TipoFiltro);
                    DadosPesquisar = DadosFiltroPesquisa;
                }
            }
            else
            {
                botaoClicado = Request.Form["btn9LimparFiltro"];
                if (!string.IsNullOrEmpty(botaoClicado))
                {
                    filtroGeral = 0;
                }
            }
            return Redirect(rotaPagina);
        }

    }
}
