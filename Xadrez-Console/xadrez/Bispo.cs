using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace xadrez
{
    
    
        class Bispo : Piece
        {
            public Bispo(Color color, Tabuleiro tab) : base(color, tab)
            {

            }

            public override string ToString()
            {
                return "B";
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

            // no
            pos.definirValores(posicao.linha-1 , posicao.coluna -1);
            while (tab.validePosition(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.piece(pos) != null && tab.piece(pos).color != color)
                {
                    break;
                }
                pos.definirValores(pos.linha -1, pos.coluna - 1); ;
            }

            // ne
            pos.definirValores(posicao.linha-1, posicao.coluna + 1);
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
            pos.definirValores(posicao.linha + 1, posicao.coluna +1);
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
