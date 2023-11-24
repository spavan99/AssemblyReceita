using Assembly.Database;
using Assembly.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.Service
{
    public class ReceitaService : IReceitaService
    {

        private readonly IReceitaRepository _Repository;

        public ReceitaService(IReceitaRepository pRepository)
        {
            this._Repository = pRepository;

        }

        public Receita Add(Receita obj)
        {
            obj = _Repository.Add(obj);
            if (obj is not null)
            {
                return obj;
            }
            else
            {
                return null;
            }
        }

        public string AddFull(DtosReceitaFull obj)
        {
            // checampo estao padrao
            if(obj is not null)
            {
                obj.Status = ReceitaStatusEnum.Aguardando;
                //obj.IdUser = 2;   // mover o usuario ativo 
            }

            if (obj.Titulo == null || obj.Descricao == null || obj.IdCategoria == 0 || obj.IdDificuldade == 0 )
            {
                return "Cadastro nao realizado - Campo nulo ou zerados";
            }
            else if (string.IsNullOrWhiteSpace(obj.Titulo))
            {
                return "Cadastro nao realizado - Campo inválido, deve conter pelo menos 3 caracteres";
            }
            else
            {
                // ver categoria exixte
                ICategoriaService categoria = new CategoriaService( new CategoriaRepository() );
                var nres = categoria.GetById<int>( obj.IdCategoria, "Id" );
                if( nres.Count == 0)
                {
                    return "Cadastro nao realizado - Categoria não existe";
                }

                // ver grau dificuldade exixte
                IDificuldadeService dificuldade = new DificuldadeService(new DificuldadeRepository());
                var nres2 = categoria.GetById<int>(obj.IdDificuldade, "Id");
                if (nres2.Count == 0)
                {
                    return "Cadastro nao realizado - Grau de Dificuldade não existe";
                }
                // conversao full para usuario
                Receita cadastrar = ParseShared.ParseClassDtos<Receita, DtosReceitaFull>(obj);
                var nret = Add(cadastrar);
                if (nret != null)
                {
                    return "Cadastro com Sucesso";

                };
                return "Nao Cadastrado";
            }
            return "Dados nulo";

        }

        public bool Delete(int id)
        {
            if (true)
            {
                return (_Repository.Delete(id));
            }
            return false;
        }

        public bool Delete(Receita obj)
        {
            if (true)
            {
                return (_Repository.Delete(obj.Id));
            }
            return false;
        }

        public List<DtosReceitaFull> GetAll()
        {
            // antes de enviar te fazer dtos
            List<dynamic> nLstAchou = (_Repository.GetAll<int>(null, 0));

            //parse dtos
            List<DtosReceitaFull> nlista;
            nlista = ParseShared.ParseListClassDtos<DtosReceitaFull>(nLstAchou);

            return nlista;
        }

        public List<DtosReceitaFull> GetById<Tvr>(Tvr id, string ncampo = null)
        {
            // antes de enviar te fazer dtos
            List<dynamic> nLstAchou = (_Repository.GetAll<Tvr>(ncampo, id));

            //parse dtos
            List<DtosReceitaFull> nlista;
            nlista = ParseShared.ParseListClassDtos<DtosReceitaFull>(nLstAchou);

            return nlista;
        }

        public List<DtosReceitaFull> GetById(List<SQLDTOSPesquisa> nChave)
        {
            // where multiplos
            List<dynamic> nLstAchou = (_Repository.GetAll(nChave));

            //parse dtos
            List<DtosReceitaFull> nlista;
            nlista = ParseShared.ParseListClassDtos<DtosReceitaFull>(nLstAchou);

            return nlista;
        }

        public bool Update(Receita obj)
        {
            if (true)
            {
                return (_Repository.Update(obj));
            }
            return false;
        }

        public bool Update(Receita obj, int id)
        {
            // checar id tem base  
            if (true)
            {
                // equipara o obejto recebido ao id a alterar e efetua a alteracao
                obj.Id = id;
                return (_Repository.Update(obj));
            }
            return false; ;
        }

        public bool UpdateFull(DtosReceitaFull obj)
        {
            // conversao full para usuario

            // ver categoria exixte
            ICategoriaService categoria = new CategoriaService(new CategoriaRepository());
            var nres = categoria.GetById<int>(obj.IdCategoria, "Id");
            if (nres.Count == 0)
            {
                return false;
            }

            // ver grau dificuldade exixte
            IDificuldadeService dificuldade = new DificuldadeService(new DificuldadeRepository());
            var nres2 = categoria.GetById<int>(obj.IdDificuldade, "Id");
            if (nres2.Count == 0)
            {
                return false;
            }

            Receita alterado = ParseShared.ParseClassDtos<Receita, DtosReceitaFull>(obj);
            return Update(alterado);

        }

        // especifica desta rotina
        public List<DtosReceitaVitrine> ReceitaVitrine()
        {
            //join com tabela de fotos / so a principal tem rever rotina princiapl nao testada

            // antes de enviar te fazer dtos
            List<dynamic> nLstAchou = (_Repository.GetAll<int>("Status", 4));

            //parse dtos
            List<DtosReceitaVitrine> nlista;
            nlista = ParseShared.ParseListClassDtos<DtosReceitaVitrine>(nLstAchou);

            return nlista;
        }


    }
}
