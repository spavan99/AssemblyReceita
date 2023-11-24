using Assembly.Database;
using Assembly.Domain;
using Assembly.Receita.Pages.CSShared;
using Assembly.Service;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web.Extensions.Head;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.Metadata;
using System.Text;

namespace Assembly.Receita.Pages.Receita.ReceitaComentario
{
    [Authorize(policy: "Admin")] // Authenticated

    public class ComentarioAprovacaoModel : PageModel
    {

        public readonly IComentariosReceitaService _novoService;

        public readonly IComentariosReceitaRepository _novoRepository;

        public List<DtosComentarioReceita> dadosConsulta { get; set; } = new List<DtosComentarioReceita>();

        public static int filtroComentarios { get; set; }
        public string escolha { get; set; }

        // titulo do cadastro injeta html
        public StringBuilder CabecalhoTitulo { get; set; }
        public string titulo { get; set; } = "Alteraçao Status COMENTARIO da Receita";

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
        public ComentarioAprovacaoModel()
        {

            this._novoRepository = new ComentariosReceitaRepository();              // para IComentariosReceitaService  nomeRepoy
            this._novoService = new ComentariosReceitaService(_novoRepository);
        }


        public void OnGet()
        {

            //**************************************************** alterar os campos para pesquisa e filtro
            // adciona campos para pesquisa  primeiro
            SQLDTOSPesquisa pesq1 = new SQLDTOSPesquisa();
            pesq1.descPesquisa = "Nr Receita";
            pesq1.nCampo = "idreceita";
            pesq1.nType = SQLtypeEnum.type_int;
            pesq1.nValor = DadosPesquisar;
            pesq1.operacao = SQLoperEnum.p_fim;
            PesCampo.Add(pesq1);

            // FIM  **************************************************** alterar os campos para pesquisa e filtro

            if (filtroComentarios == 0)
            { filtroComentarios = 2; }

            dadosConsulta = _novoService.GetById<int>(filtroComentarios, "aprovado");
            //dadosConsulta = novoRep.GetById<int>(0, null);

            // titulo 
            CabecalhoTitulo = new CabTituloCRUD().start(titulo);

            // viwdata alimentar tela carregada // para o formulario de consulta e lista paginas
            ViewData["PesCampo"] = PesCampo;
            ViewData["rotaLista"] = "/Receita/ReceitaComentario/ComentarioAprovacao";
            ViewData["PaginaAtual"] = 1;
            ViewData["NumTotaldePag"] = 1;

        }

        public void OnPost()
        {

        }

        public IActionResult OnPostAprovar(int IdAlterar, int IdReceita, string Comentario, int Avaliacao, int Aprovado)
        {
            // status 1 passa para 2
            // status 2 para para 3
            // status 3 para para 1
            ComentarioReceitaEnum nvalor;
            if (Aprovado == 1)
            {
                nvalor = ComentarioReceitaEnum.Aguardando;
            }
            else if (Aprovado == 2)
            {
                nvalor = ComentarioReceitaEnum.Aprovado;
            }
            else
            { nvalor = ComentarioReceitaEnum.Nao_Aprovado; }

            //cria variave 
            ComentariosReceita nova = new ComentariosReceita();
            nova.Id = IdAlterar;
            nova.IdReceita = IdReceita;
            nova.Comentario = Comentario;
            nova.Avaliacao = Avaliacao;
            nova.Aprovado = nvalor;

            // updatre
            bool susecc = _novoService.Update(nova);

            if (susecc)
            {
                Console.WriteLine("Alterado com sucesso");
            }

            return RedirectToPage("/Receita/ReceitaComentario/ComentarioAprovacao");

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
            }
            return RedirectToPage("/Receita/ReceitaComentario/ComentarioAprovacao");
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
            return Redirect("/Receita/ReceitaComentario/ComentarioAprovacao");
        }
    }


}


