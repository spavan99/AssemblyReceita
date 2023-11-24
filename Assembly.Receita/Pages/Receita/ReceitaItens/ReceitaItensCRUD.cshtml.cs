using Assembly.Database;
using Assembly.Receita.Pages.CSShared;
using Assembly.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Text;

namespace Assembly.Receita.Pages.Receita.ReceitaItens
{
    [Authorize(policy: "Usuario")] // Authenticated
    public class ReceitaItensCRUDModel : PageModel
    {
        //injeçao de depencia
        private readonly IItensReceitaRepository _Repository;
        private readonly IItensReceitaService _Service;

        public ReceitaItensCRUDModel(IItensReceitaRepository Repository, IItensReceitaService Service)
        {
            _Repository = Repository;
            _Service = Service;
        }

        // ******************************** grupo dados alterar
        // Dados filtrado
        public List<DtosItensReceitaFull> dadosFiltro { get; set; } = new List<DtosItensReceitaFull>();
        //public List<dynamic> dadosFiltro { get; set; } = new List<dynamic>();

        //rota lista - atual
        public string rotaLista { get; set; } = "/Receita/ReceitaItens/ReceitaItensCRUD";

        //rota lista - cadastro/update/delete/l
        public string rotaCRUD { get; set; } = "/Receita/ReceitaItens/ReceitaItensCadastrar";

        private string titulo { get; set; } = "Ingredientes";
        // Fim  ******************************** grupo dados alterar


        // titulo do cadastro injeta html
        public StringBuilder CabecalhoTitulo { get; set; }

        // campos pesquisa html
        public List<SQLDTOSPesquisa> PesCampo { get; set; } = new();

        // campos pesquisa html
        public List<SQLDTOSPesquisa> PesFinal { get; set; } = new();

        [BindProperty]
        public string TipoFiltro { get; set; }

        [BindProperty]
        public string DadosFiltroPesquisa { get; set; }

        // ********* varaiveis static para pode funcionar todos o paginas recaregadas do lado do servidor
        // dados  a sere pesquisadp 
        public static string DadosPesquisar { get; set; }

        // filtro pesquisa final zero todos itens  1 conforme selecao
        public static int filtroGeral;
        // item pesquisa selecionaodo
        public static int selecao;

        public List<string> listCamposTAB;
        public List<string> descricaoCamposTAB;
        public int nrReceita { get; set; }
        //**********************************************************************************************

        public async Task OnGet([FromQuery] int? IdOrigem, [FromQuery] string? acaoOrigem)
        {
            //variavel comuicacao outro form
            TempData["MinhaChave"] = IdOrigem.Value;

            //**************************************************** alterar os campos para pesquisa e filtro
            // adciona campos para pesquisa  primeiro  // nao vamos colocar filtro tmos manter para compatviilidade // tirar cshtml
            SQLDTOSPesquisa pesq1 = new SQLDTOSPesquisa();
            pesq1.descPesquisa = "Id";
            pesq1.nCampo = "Id";
            pesq1.nType = SQLtypeEnum.type_int;
            pesq1.nValor = DadosFiltroPesquisa;
            pesq1.operacao = SQLoperEnum.p_nulo;
            PesCampo.Add(pesq1);

            // FIM  **************************************************** alterar os campos para pesquisa e filtro

            if (filtroGeral == 0)
            {
                if (IdOrigem.HasValue)
                {
                    nrReceita = IdOrigem.Value;
                    dadosFiltro = _Service.GetById<int>(IdOrigem.Value, "IdReceita");
                }
            }else
            {   // uso do filtro de pesquisa
                PesFinal.Clear();
                var naux = new SQLDTOSPesquisa();

                naux.nCampo = PesCampo[selecao].nCampo;
                naux.operacao = SQLoperEnum.p_fim;
                naux.nValor = DadosPesquisar;
                naux.descPesquisa = PesCampo[selecao].descPesquisa;
                naux.nType = PesCampo[selecao].nType;
                PesFinal.Add(naux);

                dadosFiltro.Clear();
                dadosFiltro = _Service.GetById(PesFinal);

            }
            // titulo 
            CabecalhoTitulo = new CabTituloCRUD().start(titulo);

            // campos a exibir
            List<string> listCamposTAB = new List<string>();
            List<string> descricaoCamposTAB = new List<string>();

            listCamposTAB.Add("Id");
            descricaoCamposTAB.Add("ID");

            listCamposTAB.Add("IdReceita");
            descricaoCamposTAB.Add("Nr Receita");

            listCamposTAB.Add("Ingredientes");
            descricaoCamposTAB.Add("Ingredientes Receita");

            listCamposTAB.Add("QtdeIngredientes");
            descricaoCamposTAB.Add("Qtde de Ingredientes");

            // casting lista dinamica
            var listaDynamic = dadosFiltro.Cast<dynamic>().ToList();

            // viwdata alimentar tela carregada // para o formulario de consulta e lista paginas
            ViewData["PesCampo"] = PesCampo;
            ViewData["rotaLista"] = rotaLista;
            ViewData["PaginaAtual"] = 1;
            ViewData["NumTotaldePag"] = 1;
            //ViewData["btn1Name"] = "Pesquisar";
            //ViewData["btn2Name"] = "Limpar";

            ViewData["dadosFiltro"] = listaDynamic;
            ViewData["rotaCRUD"] = rotaCRUD;
            ViewData["listCamposTAB"] = listCamposTAB;
            ViewData["descricaoCamposTAB"] = descricaoCamposTAB;
            ViewData["NumItensPorPag"] = 1000;    // sem paginacao

            // campo botal adicional se nao econtrar nada tabela
            ViewData["btnAdicName"] = "Itens Receita";
            ViewData["rotaItens"] = "/Receita/ReceitaItens/ReceitaItensCRUD";
            ViewData["TemBotaoMais"] = 1;

            //TempData["MinhaVariavel"] = "ValorInicial vou teste se funciobna";

            // campo botal adicional se nao econtrar nada tabela
            ViewData["btnAdicNameFinal"] = "Nova Itens Receita";
            ViewData["TemBotaoFinal"] = 1;



        }

        public void OnPost()
        {
            //Console.WriteLine("teste");
            //Console.ReadLine();
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
            return Redirect(rotaLista);
        }


    }
}
