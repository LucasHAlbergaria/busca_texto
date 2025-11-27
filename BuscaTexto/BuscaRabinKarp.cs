using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuscaTexto {
    class BuscaRabinKarp {
        const long q = 10014521L;
        const int d = 128;

        public static int RKSearch(String p, String t) {
            long dm = 1, h1 = 0, h2 = 0;
            int i;
            int m = p.Length;
            int n = t.Length;
            if (n < m) // texto MENOR que o padrão
                return -1;
            for (i = 1; i < m; i++)
                dm = (d * dm) % q;
            for (i = 0; i < m; i++) {
                h1 = (h1 * d + p[i]) % q;
                h2 = (h2 * d + t[i]) % q;
            }
            for (i = 0; h1 != h2; i++) {
                if (i >= n - m) // chegou ao final do texto sem encontrar
                    return -1;
                h2 = (h2 + d * q - t[i] * dm) % q;
                h2 = (h2 * d + t[i + m]) % q;
            }
            return i;
        }
        public static List<int> TodasOcorrenciasRK(string p, string t)
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
                int resultado = RKSearch(p, subTexto);

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
