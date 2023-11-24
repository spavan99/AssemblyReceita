using Assembly.Database;
using Assembly.Domain;
using Assembly.Service;

internal class Program
{
    private static void Main(string[] args)
    {

        cadastra();

        //cadastrareceita();

       // cadastracomentario();


        //deletaid();
        //deletauser();

        //alterarSomenteObjeto();

        //pesquisa();

        //LoginUser();

    }


    public static void LoginUser()
    {
        UserService novorepository = new UserService(new UserRepository());

        bool logado = novorepository.Login("spavan", "123trter");

        if (logado)
        {
            Console.WriteLine("Logado com sucessso");
        }
        else
        {
            Console.WriteLine("Nao logado usuario e senha invalidos");

        }
        Console.ReadLine();

    }


    public static void pesquisa()
    {

        UserService novorepository = new UserService(new UserRepository());
//        var resut = novorepository.GetAll();

       var resut = novorepository.GetById<string>( "Daut", "Name");

 //       var resut = novorepository.GetById<string>("Silvio", "Name");

//        var resut = novorepository.GetById<int>( 2, "Id");

        for ( int i = 0; i < resut.Count; i++ )
        {
            //Console.Write(resut[i].Id + "  >>  ");
            Console.WriteLine(resut[i].Name);
            Console.WriteLine("email  " + resut[i].Email);
        }
        Console.ReadLine();


    }


    public static void deletauser()
    {

        // incluir usuario texte        
        Usuario novo = new Usuario();
        novo.Id = 12;

        UserService novorepository = new UserService(new UserRepository());
        var resut = novorepository.Delete(novo);

        if (resut)
        {
            Console.WriteLine("Usuario deletado");
        }
        else
        {
            Console.WriteLine("Usuairo nao deletado ou nao encontrado");
        }


    }

    public static void deletaid()
    {

        UserService novorepository = new UserService(new UserRepository());
        var resut = novorepository.Delete(28);

        if (resut)
        {
            Console.WriteLine("Usuario deletado");
        }
        else
        {
            Console.WriteLine("Usuairo nao deletado ou nao encontrado");
        }


    }

    public static void cadastra()
    {


        // incluir usuario texte        
        Usuario novo = new Usuario();

        novo.Name = "Danila";
        novo.SobreNome = "Duarte";
        novo.UserName = "daniela";
        novo.Email = "daniela@gmail.com";
        novo.Senha = "1234@teste";
        novo.Ativo = AtivoEnum.Ativo;
        novo.TipoUsuario = TipoUsuarioEnum.Usuario;

        UserService novorepository = new UserService(new UserRepository());
        var resut = novorepository.Add(novo);

        if (resut is not null)
        {
            Console.WriteLine("Usuario cadasrado  id: " + resut.Id);
        }
        else
        {
            Console.WriteLine("Usuario nao cadstrado");
        }

    }



    public static void cadastrareceita()
    {


        // incluir usuario texte        
        Receita novo = new Receita();

        novo.Descricao = "test teste tesereceita padrap";
        novo.IdDificuldade = 1;
        novo.ServePessoas = 5;
        novo.IdCategoria = 1;
        novo.Preparo = " jfjfd fjsdjfs fhfjfjhjfuhjvamo preapara do modo aafdjaf sjf flk dfljfjsdf df lsjfsjdf ";
        novo.IdUser = 9;
        novo.Tempo = TempoPreparoEnum.Baixo;
        novo.TipoPrato = TipoPratoEnum.Carnivoro;
        novo.Status = ReceitaStatusEnum.Publicada;
        novo.Titulo = "pato assado kkkkkkkkkkkkkkk";

        ReceitaService novorepository = new ReceitaService(new ReceitaRepository() );
        var resut = novorepository.Add(novo);

        if (resut is not null)
        {
            Console.WriteLine("receita  cadadasrado  id: " + resut.Id);
        }
        else
        {
            Console.WriteLine("receita nao cadstrado");
        }
    }



    public static void cadastracomentario()
    {


        // incluir usuario texte        
        ComentariosReceita novo = new ComentariosReceita();

        novo.IdReceita = 4;
        novo.Comentario = "ok kkkkmarqviloho jlkj ff ifj flkjf fljlifj f";
        novo.Avaliacao = 4;
        novo.Aprovado = ComentarioReceitaEnum.Aguardando;

        ComentariosReceitaService novorepository = new ComentariosReceitaService(new ComentariosReceitaRepository() );
        var resut = novorepository.Add(novo);

      //  ReceitaService novorepository = new ReceitaService(new ReceitaRepository());
      //  var resut = novorepository.Add(novo);

        if (resut is not null)
        {
            Console.WriteLine("receita  cadadasrado  id: " + resut.Id);
        }
        else
        {
            Console.WriteLine("receita nao cadstrado");
        }
    }





    public static void alterarSomenteObjeto()
    {


        // incluir usuario texte        
        Usuario novo = new Usuario();

        novo.Id= 11;
        novo.Name = "alteracao  ";
        novo.SobreNome = "alteracao Pavan";
        novo.UserName = "altercao";
        novo.Email = "alteracao";
        novo.Senha = "altercao  1234@teste";
        novo.Ativo = AtivoEnum.Inativo;
        novo.TipoUsuario = TipoUsuarioEnum.Usuario;

        UserService novorepository = new UserService(new UserRepository());
        //var resut = novorepository.Update(novo, 6);
        var resut = novorepository.Update(novo);


        if (resut)
        {
            Console.WriteLine("Usuario altrado  id: " );
        }
        else
        {
            Console.WriteLine("Usuario nao encontrado ou nao alterado");
        }

    }


}