using Assembly.Database;
using Assembly.Domain;
using Assembly.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;

namespace Assembly.Receita.Pages.Receita.Site
{
    //[BindProperties]
    public class ReceitaDetalheSiteModel : PageModel
    {
        // recebe os paramentos
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Titulo { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Descricao { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Preparo { get; set; }
        [BindProperty(SupportsGet = true)]
        public int Tempo { get; set; }
        [BindProperty(SupportsGet = true)]
        public int TipoPrato { get; set; }
        [BindProperty(SupportsGet = true)]
        public int IdCategoria { get; set; }
        [BindProperty(SupportsGet = true)]
        public int IdDificultade { get; set; }
        [BindProperty(SupportsGet = true)]
        public int ServePessoas { get; set; }
        [BindProperty(SupportsGet = true)]
        public int fotoreceita { get; set; }
        [BindProperty(SupportsGet = true)]

        public string ComentarioReceita { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ChaveReceita { get; set; }

        //obtem os ingredientees
        public List<DtosItensReceitaFull> itensVitrine { get; set; } = new List<DtosItensReceitaFull>();

        // LIST STRING 
        public List<displayItens> classReceita { get; set; } = new List<displayItens>();


        //LIST COMENTARIO
        public List<DtosComentarioReceita> comentariosRec { get; set; } = new List<DtosComentarioReceita>();

        [BindProperty]
        public string RadioClassificacao { get; set; }

        //
        //[BindProperty]
        //public myComentario myComentario { get; set; }

        public string fotoReceita { get; set; }

        public void OnGet()
        {
            //Id = Request.Query["Id"];
            //obten dados da receita

            // OBTEM ITEENS DA RECEITA 
            int chave = int.Parse(Id);
            IItensReceitaService obitens = new ItensReceitaService(new ItensReceitaRepository());
            itensVitrine = obitens.GetById<int>(chave, "IdReceita");

            //  filtro sql pegar comentario e receita especifica e somente publicado  
            // monta filtro 
            List<SQLDTOSPesquisa> npesqRec = new List<SQLDTOSPesquisa>();
            // campo um  chave 1
            SQLDTOSPesquisa campo1 = new SQLDTOSPesquisa();
            campo1.nCampo = "IdReceita";
            campo1.nValor = Id;    /// sempre string
            campo1.nType = SQLtypeEnum.type_int;
            campo1.operacao = SQLoperEnum.p_and;
            // campo um  chave 2
            SQLDTOSPesquisa campo2 = new SQLDTOSPesquisa();
            campo2.nCampo = "aprovado";
            campo2.nValor = "3";    /// sempre string
            campo2.nType = SQLtypeEnum.type_int;
            campo2.operacao = SQLoperEnum.p_fim;
            npesqRec.Add(campo1);
            npesqRec.Add(campo2);

            // obtem comentario da receita
            IComentariosReceitaService comenti = new ComentariosReceitaService(new ComentariosReceitaRepository());
            comentariosRec = comenti.GetById(npesqRec);

            //adicona elmento a classificacao da receita  ok
            //avaliacao  nao tem
            //novo.item = "Avaliacao";
            //novo.qtde = Tempo.ToString();  
            //novo.mostra = 2;
            //classReceita.Add(novo);

            //Categoria  --> buscar na tabela
            classReceita.Add(
                  new displayItens
                  {
                      item = "Categoria",
                      qtde = IdCategoria.ToString(),
                      mostra = 2
                  }
            );
            //tempo de preparoip  // fazendo como entrelas
            classReceita.Add(
                  new displayItens
                  {
                      item = "Tempo Preparo",
                      qtde = Tempo.ToString(),
                      mostra = 1
                  }
            );


            //tempo de dificuladae  --> buscar tabela 
            classReceita.Add(
                  new displayItens
                  {
                      item = "Dificuldade do prato",
                      qtde = IdDificultade.ToString(),
                      mostra = 2
                  }
            );

            //tipo prato  --> buscar enum 
            //DiaDaSemana dia = DiaDaSemana.Quarta;
            //string descricao = dia.GetDescription(); /obter description do enum
            //DiaDaSemana dia = (DiaDaSemana)valorNumerico;
            //string nomeEnum = Enum.GetName(typeof(DiaDaSemana), dia);
            TipoPratoEnum estado = (TipoPratoEnum)TipoPrato;
            string descricaoEnum = Enum.GetName(typeof(TipoPratoEnum), estado);
            classReceita.Add(
                  new displayItens
                  {
                      item = "Tipo Prato",
                      qtde = descricaoEnum,
                      mostra = 2
                  }
            );

            //serve pessoas 
            classReceita.Add(
                  new displayItens
                  {
                      item = "Serve Qtde Pessoas",
                      qtde = ServePessoas.ToString(),
                      mostra = (ServePessoas > 10 ? 2 : 1),
                      loop = 10
                  }
            );

        }

        public void OnPost()
        {

        }
        
        public IActionResult OnPostGravarDadosComentario()
        //public void OnPostGravarDadosComentario()
        {

            //checar boto
            int nravaliacao = 1;
            if( RadioClassificacao == "OK2")
            {
                nravaliacao = 2; 
            }else if( RadioClassificacao == "OK3")
            {
                nravaliacao = 3;
            }else if( RadioClassificacao == "OK4")
            {
                nravaliacao = 4;
            }else if( RadioClassificacao == "OK5")
            {
                nravaliacao = 5;
            }

            ComentariosReceita novaRec = new ComentariosReceita();
            novaRec.Comentario = ComentarioReceita;
            novaRec.IdReceita = int.Parse(ChaveReceita);
            novaRec.Avaliacao = nravaliacao;
            novaRec.Aprovado = ComentarioReceitaEnum.Aguardando;

            ComentariosReceitaService novorepository = new ComentariosReceitaService(new ComentariosReceitaRepository());
            var resut = novorepository.Add(novaRec);

            Console.WriteLine(resut);

            //return RedirectToPage();
            // return Page();
            return RedirectToPage("/Index");
        }
    }

}
public class displayItens
{
    public string item {get; set;}
    public string qtde { get; set; }
    public int mostra { get; set; }
        // 1 mostra dizer // mostra estrelas
    public int loop { get; set; } = 5;
}

