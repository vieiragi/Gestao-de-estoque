using System;

namespace BrandsGoods.Entities
{
    /// <summary>
    /// Classe para checar permissoes
    /// </summary>
    static class Permissao
    {
        /// <summary>
        /// Metodo para checar permissoes
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static bool Check(int[] level)
        {
            try
            {
                foreach (int i in level)
                {
                    if (i == Program.groupId) return true;
                }
            }
            catch (ArgumentNullException ex)
            {

                Console.Write("Erro (Nulo): ");
                Console.Write($"{ex}");
            }
            return false;
        }
    }
}
