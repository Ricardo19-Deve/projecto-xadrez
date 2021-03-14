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
        private PartidaDeXadrez partida;
        public King(Color color, Tabuleiro tab, PartidaDeXadrez partida) : base(color, tab)
        {
            this.partida = partida;
        }

        public override string ToString()
        {
            return "R";
        }

        private bool podeMover(Posicao pos)
        {
            Piece p = tab.piece(pos);
            return p == null || p.color != color;
        }

        private bool testeTorreParaRoque(Posicao pos)
        {
            Piece p = tab.piece(pos);
            return p != null && p is Tower && p.color == color && p.qntMovements == 0;
        }

        public override bool[,] moviementosPossiveis()
        {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);

            // acima
            pos.definirValores(posicao.linha - 1, posicao.coluna);
            if (tab.validePosition(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            // nordeste
            pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
            if (tab.validePosition(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            // direita
            pos.definirValores(posicao.linha, posicao.coluna + 1);
            if (tab.validePosition(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            // sudeste
            pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
            if (tab.validePosition(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            // abaixo
            pos.definirValores(posicao.linha + 1, posicao.coluna);
            if (tab.validePosition(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            // sudoeste
            pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
            if (tab.validePosition(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            // esquerda
            pos.definirValores(posicao.linha, posicao.coluna - 1);
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

            //#jogadaespecial Roque
            if (qntMovements == 0 && !partida.xeque)
            {
                //#jogadaespecial roque pequeno
                Posicao posT1 = new Posicao(posicao.linha, posicao.coluna + 3);

                if (testeTorreParaRoque(posT1))
                {
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna + 1);
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna + 2);
                    if (tab.piece(p1) == null && tab.piece(p2) == null)
                    {
                        mat[posicao.linha, posicao.coluna + 2] = true;
                    }

                }
                //#jogadaespecial roque grande
                Posicao posT2 = new Posicao(posicao.linha, posicao.coluna -4);

                if (testeTorreParaRoque(posT2))
                {
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna - 1);
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna -2);
                    Posicao p3 = new Posicao(posicao.linha, posicao.coluna - 3);
                    if (tab.piece(p1) == null && tab.piece(p2) == null && tab.piece(p3)== null)
                    {
                        mat[posicao.linha, posicao.coluna - 2] = true;
                    }

                }

            }
            return mat;
        }
    }
}



