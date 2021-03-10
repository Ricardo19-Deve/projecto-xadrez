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
        private HashSet<Piece> pieces;
        private HashSet<Piece> capture;
        

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Color.White;
            pieces = new HashSet<Piece>();
            capture = new HashSet<Piece>();
            colocarPecas();
        }

        public void executaMovimento(Posicao origem, Posicao destino)
        {
            Piece p = tab.retirarPeca(origem);
            p.incrementarQtdMovimentos();
            Piece pecaCapturada = tab.retirarPeca(destino);
            tab.putPiece(p, destino);
            if (pecaCapturada != null)
            {
                capture.Add(pecaCapturada);
            }
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

        public HashSet<Piece> pecasCapturadas(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach(Piece x in capture)
            {
                if (x.color == color)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }
        
        public HashSet<Piece> pecasEmJogo(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in capture)
            {
                if (x.color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(color));
            return aux;
        }
        public void colocarNovaPeca(char coluna, int linha, Piece piece)
        {
            tab.putPiece(piece, new PosicaoXadrez(coluna, linha).toPosicao());
            pieces.Add(piece);
        }
        private void colocarPecas()
        {
            colocarNovaPeca('c', 1, new Tower(Color.White, tab));
            colocarNovaPeca('c', 2, new Tower(Color.White, tab));
            colocarNovaPeca('d', 2, new Tower(Color.White, tab));
            colocarNovaPeca('e', 2, new Tower(Color.White, tab));
            colocarNovaPeca('e', 1, new Tower(Color.White, tab));
            colocarNovaPeca('d', 1, new King(Color.White, tab));

            colocarNovaPeca('c',7,new Tower(Color.Black, tab));
            colocarNovaPeca('c',8,new Tower(Color.Black, tab));
            colocarNovaPeca('d',7,new Tower(Color.Black, tab));
            colocarNovaPeca('e',7,new Tower(Color.Black, tab));
            colocarNovaPeca('e',8,new Tower(Color.Black, tab));
            colocarNovaPeca('d',8,new King(Color.Black, tab));
        }
    }
}
