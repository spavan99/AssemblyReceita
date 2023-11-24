using Assembly.Database;
using Assembly.Domain;
using Assembly.Receita.Pages.CSShared;
using Assembly.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace Assembly.Receita.Pages.Receita.Receita
{
    [Authorize(policy: "Usuario")] // Authenticated
    public class ReceitaCRUDModel : PageModel
    {
        //injeçao de depencia
        private readonly IReceitaRepository _Repository;
        private readonly IReceitaService _Service;

        public ReceitaCRUDModel(IReceitaRepository Repository, IReceitaService Service)
        {
            _Repository = Repository;
            _Service = Service;
        }

        // ******************************** grupo dados alterar
        // Dados filtrado
        public List<DtosReceitaFull> dadosFiltro { get; set; } = new List<DtosReceitaFull>();

        //rota lista - atual
        public string rotaLista { get; set; } = "/Receita/Receita/ReceitaCRUD";

        //rota lista - cadastro/update/delete/l
        public string rotaCRUD { get; set; } = "/Receita/Receita/ReceitaCadastrar";

        private string titulo { get; set; } = "Cadastro de Receitas do Site";
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

        public int UserLogado { get; set; }

        //**********************************************************************************************

        public async Task OnGet()
        {
            // pegar usuaior logado
            UserLogado = int.Parse(User.FindFirst("id").Value);

            //pegar role
            var role = User.FindFirst("role").Value;
            bool filtroUser = false;
            if(role.ToUpper().Equals(TipoUsuarioEnum.Master.ToString().ToUpper()) ||
               role.ToUpper().Equals(TipoUsuarioEnum.Admin.ToString().ToUpper()) )
            { filtroUser = true;} 


            // passar mais o campo usuario e receita rempensar  // colocar somente no usuario total
            //**************************************************** alterar os campos para pesquisa e filtro
            // adciona campos para pesquisa  primeiro
            SQLDTOSPesquisa pesq1 = new SQLDTOSPesquisa();
            pesq1.descPesquisa = "Nr Receita";
            pesq1.nCampo = "Id";
            pesq1.nType = SQLtypeEnum.type_int;
            pesq1.nValor = DadosFiltroPesquisa;
            pesq1.operacao = SQLoperEnum.p_fim;
            PesCampo.Add(pesq1);

            // adciona campos para pesquisa  segundo
            SQLDTOSPesquisa pesq2 = new SQLDTOSPesquisa();
            pesq2.descPesquisa = "Titulo";
            pesq2.nCampo = "Titulo";
            pesq2.nType = SQLtypeEnum.type_string;
            pesq2.nValor = DadosFiltroPesquisa;
            pesq2.operacao = SQLoperEnum.p_fim;
            PesCampo.Add(pesq2);

            // FIM  **************************************************** alterar os campos para pesquisa e filtro

            if (filtroGeral == 0)
            {   // filtro para usaurio geral e individual
                if (filtroUser)
                { dadosFiltro = _Service.GetAll();
                }else
                { dadosFiltro = _Service.GetById<int>(UserLogado, "IdUser");
                }

            }else
            {
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
            descricaoCamposTAB.Add("Nr Receita");

            listCamposTAB.Add("IdUser");
            descricaoCamposTAB.Add("Id Usuario");

            listCamposTAB.Add("Titulo");
            descricaoCamposTAB.Add("Titulo da Receita");

            listCamposTAB.Add("Descricao");
            descricaoCamposTAB.Add("Descricao Receita");

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

            //--->  criado dois botoes adicionais na tabela
            // campo adicional na tabela - buton para uma açoo especifica // TemItensMais == 0 ou nulo nao tem
            ViewData["btnAdicName"] = "Itens Receita";
            ViewData["rotaItens"] = "/Receita/ReceitaItens/ReceitaItensCRUD";
            ViewData["TemItensMais"] = 1;

            // campo mais botal - buton para uma açoo especifica // TemItensMais == 0 ou nulo nao tem
            ViewData["btnAdicName2"] = "Fotos";
            //ViewData["rotaItens2"] = "/Receita/ReceitaItens/ReceitaItensCRUD";
            ViewData["rotaItens2"] = "/Receita/Fotos/FotosReceita";
            ViewData["TemItensMais2"] = 1;
            //---------------------------------------------------------------------------------

            // campo botal adicional se nao econtrar nada tabela
            ViewData["btnAdicNameFinal"] = "Nova Receita";
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
