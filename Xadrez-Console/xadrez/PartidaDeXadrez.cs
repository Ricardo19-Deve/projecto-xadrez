using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Color jogadorAtual { get; private set; }
        public bool terminada { get; private set; }

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Color.White;
            colocarPecas();
        }

        public void executaMovimento(Posicao origem, Posicao destino)
        {
            Piece p = tab.retirarPeca(origem);
            p.incrementarQtdMovimentos();
            Piece pecaCapturada = tab.retirarPeca(destino);
            tab.putPiece(p, destino);
        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {
            executaMovimento(origem, destino);
            turno++;

            mudaJogador();
        }

        public void validarPosicaoDeOrigem(Posicao pos)
        {
            if(tab.piece(pos) == null)
            {
                throw new BoardException("Não existe peça na posição de origem escolhida!");
            }
            if(jogadorAtual != tab.piece(pos).color)
            {
                throw new BoardException("A peça de origem não é a sua!");
            }
            if (! tab.piece(pos).existeMovimentosPossiveis())
            {
                throw new BoardException("Não há movimentos possiveis para a peça de origem escolhida!");
            }
        }
        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!tab.piece(origem).podeMoverPara(destino))
            {
                throw new BoardException("Posição de destino inválida!");
            }
        }
        private void mudaJogador()
        {
            if (jogadorAtual == Color.White)
            {
                jogadorAtual = Color.Black;
            }
            else
            {
                jogadorAtual = Color.White;
            }
        }

        private void colocarPecas()
        {
            tab.putPiece(new Tower(Color.White, tab), new PosicaoXadrez('c', 1).toPosicao());
            tab.putPiece(new Tower(Color.White, tab), new PosicaoXadrez('c', 2).toPosicao());
            tab.putPiece(new Tower(Color.White, tab), new PosicaoXadrez('d', 2).toPosicao());
            tab.putPiece(new Tower(Color.White, tab), new PosicaoXadrez('e', 2).toPosicao());
            tab.putPiece(new Tower(Color.White, tab), new PosicaoXadrez('e', 1).toPosicao());
            tab.putPiece(new King(Color.White, tab), new PosicaoXadrez('d', 1).toPosicao());

            tab.putPiece(new Tower(Color.Black, tab), new PosicaoXadrez('c', 7).toPosicao());
            tab.putPiece(new Tower(Color.Black, tab), new PosicaoXadrez('c', 8).toPosicao());
            tab.putPiece(new Tower(Color.Black, tab), new PosicaoXadrez('d', 7).toPosicao());
            tab.putPiece(new Tower(Color.Black, tab), new PosicaoXadrez('e', 7).toPosicao());
            tab.putPiece(new Tower(Color.Black, tab), new PosicaoXadrez('e', 8).toPosicao());
            tab.putPiece(new King(Color.Black, tab), new PosicaoXadrez('d', 8).toPosicao());
        }
    }
}
