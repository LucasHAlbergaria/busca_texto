using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuscaTexto {
    class BuscaBoyerMoore {
        static int[] skip = new int[256];

        public static void initSkip(String p) {
            int j, m = p.Length;
            for (j = 0; j < 256; j++)
                skip[j] = m;
            for (j = 0; j < m; j++)
                skip[p[j]] = m - j - 1;
        }

        public static int BMSearch(String p, String t) {

            int m = p.Length;
            int n = t.Length;
            int i = m - 1;
            int j = m - 1;

            initSkip(p);

            while (i < n)
            {
                while (j >= 0 && t[i] == p[j])
                {
                    i--;
                    j--;
                }

                if (j < 0)
                    return i + 1;

                int skipValue = (t[i] < 256) ? skip[t[i]] : m; // Protege se tiver caractere fora da tabela ASCII
                i += Math.Max(skipValue, m - j);
                j = m - 1;
            }

            return -1;
        }
        public static List<int> BoyerTodasOcorrencias(string p, string t)
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
                int resultado = BMSearch(p, subTexto);

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
