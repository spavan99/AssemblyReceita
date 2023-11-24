using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Assembly.Service
{
    public class ParseShared
    {

        public ParseShared() { }

        //parde classe para dtos // recebe um elemento por vez

 
        public static List<TDestino> ParseListClassDtos<TDestino>(List<dynamic> listaOrigem)
        {
            var listaDestino = new List<TDestino>();

            // Obtenha as propriedades do DTO
            var propriedadesDestino = typeof(TDestino).GetProperties();

            /*
            foreach( var prop in propriedadesDestino)
            {
                Console.WriteLine(prop);
            }
            Console.ReadLine();
            */

            foreach (var itemOrigem in listaOrigem)
            {
                var itemDestino = Activator.CreateInstance<TDestino>();
 
                /*
                foreach( var prop in itemOrigem)
                {
                    Console.WriteLine(prop);
                }
                Console.ReadLine();
                */

                foreach (var propDestino in propriedadesDestino)
                {
                    var propOrigem = ((IDictionary<string, object>)itemOrigem).FirstOrDefault(p => p.Key == propDestino.Name);
                    if (!string.IsNullOrEmpty(propOrigem.Key))
                    {
                        var valorOrigem = propOrigem.Value;
                        propDestino.SetValue(itemDestino, valorOrigem);
                    }
                }

                listaDestino.Add(itemDestino);
            }
            return listaDestino;
        }

        public static TDestino ParseClassDtos<TDestino, TOrigen>(TOrigen objetoOrigem)
        {
            var nretDtos = Activator.CreateInstance<TDestino>();

            var propriedadesDestino = typeof(TDestino).GetProperties();

            foreach (var propDestino in propriedadesDestino)
            {
                var nomePropDestino = propDestino.Name;

                // Use reflexão para obter a propriedade correspondente do objeto de origem
                var propOrigem = typeof(TOrigen).GetProperty(nomePropDestino);

                if (propOrigem != null)
                {
                    var valorOrigem = propOrigem.GetValue(objetoOrigem);

                    // Certifique-se de que o valor de origem não é nulo antes de atribuí-lo
                    if (valorOrigem != null)
                    {
                        propDestino.SetValue(nretDtos, valorOrigem);
                    }
                }
            }

            return nretDtos;
        }


        //public static TDestino ParseClassDtos<TDestino>(dynamic listaOrigem)
        //{
        //    var nretDtos = Activator.CreateInstance<TDestino>();

        //    // Obtenha as propriedades do DTO de destino
        //    var propriedadesDestino = typeof(TDestino).GetProperties();

        //    var origemDict = (IDictionary<string, object>)listaOrigem;

        //    foreach (var propDestino in propriedadesDestino)
        //    {
        //        var nomePropDestino = propDestino.Name;

        //        if (origemDict.ContainsKey(nomePropDestino))
        //        {
        //            var valorOrigem = origemDict[nomePropDestino];

        //            // Certifique-se de que o valor de origem não é nulo antes de atribuí-lo
        //            if (valorOrigem != null)
        //            {
        //                propDestino.SetValue(nretDtos, valorOrigem);
        //            }
        //        }
        //    }

        //    return nretDtos;
        //}


        //public static TDestino ParseClassDtos<TDestino>(dynamic listaOrigem)
        //{
        //    var nretDtos = Activator.CreateInstance<TDestino>();

        //    // Obtenha as propriedades do DTO
        //    var propriedadesDestino = typeof(TDestino).GetProperties();

        //            var g = (IDictionary<string, object>)listaOrigem;
        //        foreach (var propDestino in propriedadesDestino)
        //        {

        //            Console.WriteLine(propDestino.Name);

        //            var propOrigem = ((IDictionary<string, object>)listaOrigem).FirstOrDefault(p => p.Key == propDestino.Name);
        //            if (!string.IsNullOrEmpty(propOrigem.Key))
        //            {
        //                var valorOrigem = propOrigem.Value;
        //                propDestino.SetValue(nretDtos, valorOrigem);
        //            }
        //        }
        //    return nretDtos;
        //}

    }
}