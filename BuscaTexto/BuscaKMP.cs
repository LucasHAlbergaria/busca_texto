using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuscaTexto
{
    class BuscaKMP
    {
        static int[] next = new int[1000];

        public static void initNext(string p)
        {
            int i = 0, j = -1, m = p.Length;
            next[0] = -1;
            while (i < m)
            {
                while (j >= 0 && p[i] != p[j])
                    j = next[j];
                i++;
                j++;
                next[i] = j;
            }
        }

        public static int KMPSearch(string p, string t)
        {
            int i = 0, j = 0, m = p.Length, n = t.Length;
            initNext(p);
            while (j < m && i < n)
            {
                while (j >= 0 && t[i] != p[j])
                    j = next[j];
                i++;
                j++;
            }
            if (j == m)
                return i - m;
            else
                return -1;
        }

        public static List<int> KMPTodasOcorrencias(string p, string t)
        {
            // Lista para armazenar as posições de todas as ocorrências do padrão no texto
            List<int> posicoes = new List<int>();
            // Índice para controlar a posição atual no texto onde a busca começará
            int inicio = 0;

            while (inicio < t.Length)
            {
                // Criar uma substring do texto a partir da posição 'inicio'
                string subTexto = t.Substring(inicio);
                // Procurar o padrão 'p' dentro da substring usando KMPSearch
                int resultado = KMPSearch(p, subTexto);

                if (resultado == -1)
                    break;
                // Adiciona a posição da ocorrência no texto original
                // 'inicio' é a posição de início da substring no texto original
                // 'resultado' é a posição da ocorrência dentro da substring
                posicoes.Add(inicio + resultado);
                inicio += resultado + 1; // Avança para depois da ocorrência encontrada
            }

            return posicoes;
        }

    }
}
