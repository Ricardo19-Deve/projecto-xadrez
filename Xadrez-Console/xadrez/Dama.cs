using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace xadrez
{
    class Dama : Piece
    {
        public Dama(Color color, Tabuleiro tab) : base(color, tab)
        {

        }

        public override string ToString()
        {
            return "D";
        }

        private bool podeMover(Posicao pos)
        {
            Piece p = tab.piece(pos);
            return p == null || p.color != color;
        }

        public override bool[,] moviementosPossiveis()
        {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);

            // acima
            pos.definirValores(posicao.linha - 1, posicao.coluna);
            while (tab.validePosition(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.piece(pos) != null && tab.piece(pos).color != color)
                {
                    break;
                }
                pos.linha = pos.linha - 1;
            }

            // direita
            pos.definirValores(posicao.linha, posicao.coluna + 1);
            while (tab.validePosition(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.piece(pos) != null && tab.piece(pos).color != color)
                {
                    break;
                }
                pos.coluna = pos.coluna + 1;
            }

            // abaixo
            pos.definirValores(posicao.linha + 1, posicao.coluna);
            while (tab.validePosition(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.piece(pos) != null && tab.piece(pos).color != color)
                {
                    break;
                }
                pos.linha = pos.linha + 1;
            }

            // esquerda
            pos.definirValores(posicao.linha, posicao.coluna - 1);
            while (tab.validePosition(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.piece(pos) != null && tab.piece(pos).color != color)
                {
                    break;
                }
                pos.coluna = pos.coluna - 1;
            }

            // no
            pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
            while (tab.validePosition(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.piece(pos) != null && tab.piece(pos).color != color)
                {
                    break;
                }
                pos.definirValores(pos.linha - 1, pos.coluna - 1); ;
            }

            // ne
            pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
            while (tab.validePosition(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.piece(pos) != null && tab.piece(pos).color != color)
                {
                    break;
                }
                pos.definirValores(pos.linha - 1, pos.coluna + 1);
            }

            // se
            pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
            while (tab.validePosition(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.piece(pos) != null && tab.piece(pos).color != color)
                {
                    break;
                }
                pos.definirValores(pos.linha + 1, pos.coluna + 1); ;
            }

            // so
            pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
            while (tab.validePosition(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.piece(pos) != null && tab.piece(pos).color != color)
                {
                    break;
                }
                pos.definirValores(pos.linha + 1, pos.coluna - 1); ;
            }

            return mat;
        }
    }
}
