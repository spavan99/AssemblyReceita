using Assembly.Database;
using Assembly.Receita.Pages.CSShared;
using Assembly.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Reflection;
using Assembly.Domain;
using Microsoft.AspNetCore.Authorization;

namespace Assembly.Receita.Pages.Receita.Usuario
{
    [Authorize(policy: "Usuario")] // Authenticated
    public class UsuarioCadastrarModel : PageModel
    {

        //injeçao de depencia
        private readonly IUserRepository _Repository;
        private readonly IUserService _Service;

        public UsuarioCadastrarModel(IUserRepository Repository, IUserService Service)
        {
            _Repository = Repository;
            _Service = Service;
        }


        // ******************************** grupo dados alterar

        private string titulo { get; set; } = "Cadastro de Usuário";

        //public Usuario dadosCRU { get; set; } = new Usuario();

        public int nrColunasCad { get; set; } = 3;   // de 1 a 3 colunas

        [BindProperty]
        public DtosUsuarioFull novoCadastro { get; set; } = new DtosUsuarioFull();

        //rota volta - atual
        public static string rotaVolta { get; set; }
        public string rotaLista { get; set; } = "/Receita/Usuario/UsuarioCRUD";
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

           
            if (IdOrigem.HasValue &&  ! string.IsNullOrEmpty(acaoOrigem))
            {
                // rota de volta
                rotaVolta = rotaLista;
                // busca usuario ou monta elementos
                var achou = _Service.GetById<int>(IdOrigem.Value, "Id");
                if( achou.Count == 1 )
                {
                    // parse para dtosfull
                    novoCadastro.Id = achou[0].Id;
                    novoCadastro.Name= achou[0].Name;
                    novoCadastro.Email= achou[0].Email;
                    novoCadastro.SobreNome = achou[0].SobreNome;
                    novoCadastro.UserName = achou[0].UserName;
                    novoCadastro.Senha = achou[0].Senha;
                    novoCadastro.Ativo = achou[0].Ativo;
                    novoCadastro.TipoUsuario = achou[0].TipoUsuario;

                }

                acaoBTN = acaoOrigem;
                if ( acaoBTN.Equals("INSERT"))
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
          
            
            }else
            {
                IdOrigem = 0;
                acaoBTN = "INSERT";
                descricaoBTN = "Salvar/Gravar";
                rotaVolta = rotaHome;
            }

            // DTOS arquivo a ser utiliza grud - montar a tela
            DtosUsuarioFull obj = new DtosUsuarioFull();
            DadosViewModel = new ReflectionModel(obj);

            // titulo 
            CabecalhoTitulo = new CabTituloCRUD().start(titulo,  descricaoTela);

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

        public IActionResult OnPostCadastraFormulario() {

            string botaoClicado = Request.Form["BtCadFormulario"];
 
            if (!string.IsNullOrEmpty(botaoClicado))
            {
                if(botaoClicado.Equals("INSERT"))
                {
                    if (novoCadastro is not null)
                    {
                        var ok = _Service.AddFull(novoCadastro);
                    }
                    // pensar como montar erro lista
                }

                if (botaoClicado.Equals("DELETE"))
                {
                    if (novoCadastro is not null)
                    {
                        var ok = _Service.Delete( novoCadastro.Id);
                    }
                    // pensar como montar erro lista
                }

                if (botaoClicado.Equals("UPDATE"))
                {
                    if( novoCadastro is not null)
                    {
                       var ok = _Service.UpdateFull(novoCadastro);
                    }
                    // pensar como montar erro lista
                }

                if (botaoClicado.Equals("VIEW"))
                {
                    // nao faze nada somente mostra o dados
                }
                if (botaoClicado.Equals("VOLTAR"))
                {
                    return RedirectToPage(rotaVolta);
                }


            }
            else {
                Console.WriteLine("botao ano encontrado");
            }

            Console.WriteLine(botaoClicado +  "rota  >>  " + rotaVolta);
            return RedirectToPage(rotaVolta);

        }
    }
}
