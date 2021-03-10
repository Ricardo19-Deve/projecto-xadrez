using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace xadrez
{
    class King : Piece
    {
        public King(Color color, Tabuleiro tab) : base(color, tab)
        {

        }

        public override string ToString()
        {
            return "R";
        }

        private bool podeMover (Posicao pos)
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
            if (tab.validePosition(pos) && podeMover (pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            // nordeste
            pos.definirValores(posicao.linha - 1, posicao.coluna +1);
            if (tab.validePosition(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            // direita
            pos.definirValores(posicao.linha, posicao.coluna +1);
            if (tab.validePosition(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            // sudeste
            pos.definirValores(posicao.linha +1, posicao.coluna+1);
            if (tab.validePosition(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            // abaixo
            pos.definirValores(posicao.linha +1, posicao.coluna);
            if (tab.validePosition(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            // sudoeste
            pos.definirValores(posicao.linha +1, posicao.coluna-1);
            if (tab.validePosition(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            // esquerda
            pos.definirValores(posicao.linha, posicao.coluna-1);
            if (tab.validePosition(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            // noreste
            pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
            if (tab.validePosition(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            return mat;
        }
    }
}



