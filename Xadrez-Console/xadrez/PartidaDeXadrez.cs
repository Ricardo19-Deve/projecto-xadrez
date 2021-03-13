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
        public bool xeque { get; private set; }


        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Color.White;
            terminada = false;
            xeque = false;
            pieces = new HashSet<Piece>();
            capture = new HashSet<Piece>();
            colocarPecas();
        }

        public Piece executaMovimento(Posicao origem, Posicao destino)
        {
            Piece p = tab.retirarPeca(origem);
            p.incrementarQtdMovimentos();
            Piece pecaCapturada = tab.retirarPeca(destino);
            tab.putPiece(p, destino);
            if (pecaCapturada != null)
            {
                capture.Add(pecaCapturada);
            }
            return pecaCapturada;
        }

        public void desfazMovimento(Posicao origem, Posicao destino, Piece pecaCapturada)
        {
            Piece p = tab.retirarPeca(destino);
            p.decrementarQtdMovimentos();
            if (pecaCapturada != null)
            {
                tab.putPiece(pecaCapturada, destino);
                capture.Remove(pecaCapturada);
            }
            tab.putPiece(p, origem);
        }
        public void realizaJogada(Posicao origem, Posicao destino)
        {
            Piece pecaCapturada = executaMovimento(origem, destino);

            if (estaEmXeque(jogadorAtual))
            {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new BoardException("Você não pode se colocar em xeque!");
            }
            if (estaEmXeque(adversaria(jogadorAtual)))
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }

            if (testeXequeMate(adversaria(jogadorAtual)))
            {
                terminada = true;
            }
            else
            {

                turno++;

                mudaJogador();
            }
        }

        public void validarPosicaoDeOrigem(Posicao pos)
        {
            if (tab.piece(pos) == null)
            {
                throw new BoardException("Não existe peça na posição de origem escolhida!");
            }
            if (jogadorAtual != tab.piece(pos).color)
            {
                throw new BoardException("A peça de origem não é a sua!");
            }
            if (!tab.piece(pos).existeMovimentosPossiveis())
            {
                throw new BoardException("Não há movimentos possiveis para a peça de origem escolhida!");
            }
        }
        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!tab.piece(origem).movimentoPossivel(destino))
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
            foreach (Piece x in capture)
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
            foreach (Piece x in pieces)
            {
                if (x.color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(color));
            return aux;
        }

        private Color adversaria(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        private Piece king(Color color)
        {
            foreach (Piece x in pecasEmJogo(color))
            {
                if (x is King)
                {
                    return x;
                }
            }
            return null;
        }

        public bool estaEmXeque(Color color)
        {
            Piece R = king(color);
            if (R == null)
            {
                throw new BoardException("Não tem rei da cor " + color + " no tabuleiro!");
            }
            foreach (Piece x in pecasEmJogo(adversaria(color)))
            {
                bool[,] mat = x.moviementosPossiveis();
                if (mat[R.posicao.linha, R.posicao.coluna])
                {
                    return true;
                }
            }

            return false;
        }

        public bool testeXequeMate(Color color)
        {
            if (!estaEmXeque(color))
            {
                return false;
            }
            foreach (Piece x in pecasEmJogo(color))
            {
                bool[,] mat = x.moviementosPossiveis();
                for (int i = 0; i < tab.linhas; i++)
                {
                    for (int j = 0; j < tab.colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i, j);
                            Piece pecaCapturada = executaMovimento(origem, destino);
                            bool testeXeque = estaEmXeque(color);
                            desfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        public void colocarNovaPeca(char coluna, int linha, Piece piece)
        {
            tab.putPiece(piece, new PosicaoXadrez(coluna, linha).toPosicao());
            pieces.Add(piece);
        }
        private void colocarPecas()
        {
            colocarNovaPeca('c', 1, new Tower(Color.White, tab));
            colocarNovaPeca('h', 7, new Tower(Color.White, tab));
            colocarNovaPeca('d', 1, new King(Color.White, tab));
            //colocarNovaPeca('e', 2, new Tower(Color.White, tab));
            //colocarNovaPeca('e', 1, new Tower(Color.White, tab));
            //colocarNovaPeca('d', 1, new King(Color.White, tab));

            //colocarNovaPeca('c', 7, new Tower(Color.Black, tab));
            //colocarNovaPeca('c', 8, new Tower(Color.Black, tab));
            //colocarNovaPeca('d', 7, new Tower(Color.Black, tab));
            //colocarNovaPeca('e', 7, new Tower(Color.Black, tab));
            colocarNovaPeca('b', 8, new Tower(Color.Black, tab));
            colocarNovaPeca('a', 8, new King(Color.Black, tab));
        }
    }
}
