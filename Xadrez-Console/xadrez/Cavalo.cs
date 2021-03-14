using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace xadrez
{
    class Cavalo : Piece
    {
        public Cavalo(Color color, Tabuleiro tab) : base(color, tab)
        {

        }

        public override string ToString()
        {
            return "C";
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

            
            pos.definirValores(posicao.linha - 1, posicao.coluna -2);
            if (tab.validePosition(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            
            pos.definirValores(posicao.linha - 2, posicao.coluna - 1);
            if (tab.validePosition(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            
            pos.definirValores(posicao.linha -2, posicao.coluna + 1);
            if (tab.validePosition(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            
            pos.definirValores(posicao.linha -1, posicao.coluna + 2);
            if (tab.validePosition(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            
            pos.definirValores(posicao.linha + 1, posicao.coluna +2);
            if (tab.validePosition(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            
            pos.definirValores(posicao.linha + 2, posicao.coluna + 1);
            if (tab.validePosition(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            
            pos.definirValores(posicao.linha+2, posicao.coluna - 1);
            if (tab.validePosition(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            // noreste
            pos.definirValores(posicao.linha +1, posicao.coluna - 2);
            if (tab.validePosition(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            return mat;
        }
    }
}
