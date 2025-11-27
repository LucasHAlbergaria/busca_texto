using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuscaTexto {
    class BuscaForcaBruta {
        public static int forcaBruta(String p, String t) {
            int i, j, aux;
            int m = p.Length;
            int n = t.Length;
            for (i = 0; i < n; i++) {
                aux = i;
                for (j = 0; j < m && aux < n; j++) {
                    if (p[j] != '?' && t[aux] != p[j])
                        break;
                    aux++;
                }
                if (j == m)
                    return i;
            }
            return -1;
        }
        public static List<int> ForcaBrutaTodasOcorrencias(string p, string t)
        {
            List<int> posicoes = new List<int>();
            int inicio = 0;

            while (inicio < t.Length)
            {
                // Cria substring a partir da posição atual 'inicio'
                string subTexto = t.Substring(inicio);

                // Busca a primeira ocorrência do padrão na substring usando seu método original
                int resultado = forcaBruta(p, subTexto);

                // Se não encontrar, sai do loop
                if (resultado == -1)
                    break;

                // Adiciona a posição relativa ao texto original
                posicoes.Add(inicio + resultado);

                // Avança o índice para continuar a busca após a ocorrência encontrada
                inicio += resultado + 1;
            }

            return posicoes;
        }
    }
}
