using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace xadrez
{
    class Peao : Piece
    {
        private PartidaDeXadrez partida;
        public Peao(Color color, Tabuleiro tab, PartidaDeXadrez partida) : base(color, tab)
        {
            this.partida = partida;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool existeInimigo(Posicao pos)
        {
            Piece p = tab.piece(pos);
            return p != null && p.color != color;
        }
        private bool livre(Posicao pos)
        {
            return tab.piece(pos) == null;
        }

        public override bool[,] moviementosPossiveis()
        {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);

            if (color == Color.White)
            {

                pos.definirValores(posicao.linha - 1, posicao.coluna);
                if (tab.validePosition(pos) && livre(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }

                pos.definirValores(posicao.linha - 2, posicao.coluna);
                if (tab.validePosition(pos) && livre(pos) && qntMovements == 0)
                {
                    mat[pos.linha, pos.coluna] = true;
                }

                pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
                if (tab.validePosition(pos) && existeInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }

                pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
                if (tab.validePosition(pos) && existeInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                // #jogada especial en passant
                if (posicao.linha ==3)
                {
                    Posicao esquerda = new Posicao(posicao.linha, posicao.coluna - 1);
                    if (tab.validePosition(esquerda)&& existeInimigo(esquerda) && tab.piece(esquerda) == partida.vulneravelEnPassanat)
                    {
                        mat[esquerda.linha -1, esquerda.coluna] = true;
                    }
                    Posicao direita = new Posicao(posicao.linha, posicao.coluna + 1);
                    if (tab.validePosition(direita) && existeInimigo(direita) && tab.piece(direita) == partida.vulneravelEnPassanat)
                    {
                        mat[direita.linha-1, direita.coluna] = true;
                    }
                }
            }
            else
            {
                pos.definirValores(posicao.linha + 1, posicao.coluna);
                if (tab.validePosition(pos) && livre(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }

                pos.definirValores(posicao.linha + 2, posicao.coluna);
                if (tab.validePosition(pos) && livre(pos) && qntMovements == 0)
                {
                    mat[pos.linha, pos.coluna] = true;
                }

                pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
                if (tab.validePosition(pos) && existeInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }

                pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
                if (tab.validePosition(pos) && existeInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                // #jogada especial en passant
                if (posicao.linha == 4)
                {
                    Posicao esquerda = new Posicao(posicao.linha, posicao.coluna - 1);
                    if (tab.validePosition(esquerda) && existeInimigo(esquerda) && tab.piece(esquerda) == partida.vulneravelEnPassanat)
                    {
                        mat[esquerda.linha +1, esquerda.coluna] = true;
                    }
                    Posicao direita = new Posicao(posicao.linha, posicao.coluna + 1);
                    if (tab.validePosition(direita) && existeInimigo(direita) && tab.piece(direita) == partida.vulneravelEnPassanat)
                    {
                        mat[direita.linha+1, direita.coluna] = true;
                    }
                }
            }

            return mat;
        }
    }

}

