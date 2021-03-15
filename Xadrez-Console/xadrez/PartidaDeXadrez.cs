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
        public Piece vulneravelEnPassanat { get; private set; }


        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Color.White;
            terminada = false;
            xeque = false;
            vulneravelEnPassanat = null;
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
            //#jogadaespecial Roque pequeno
            if ( p is King && destino.coluna== origem.coluna +2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1);
                Piece T = tab.retirarPeca(origemT);
                T.incrementarQtdMovimentos();
                tab.putPiece(T, destinoT);

            }
            //#jogadaespecial Roque grande
            if (p is King && destino.coluna == origem.coluna -2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1);
                Piece T = tab.retirarPeca(origemT);
                T.incrementarQtdMovimentos();
                tab.putPiece(T, destinoT);

            }

            // # jogada especial en passant
            if ( p is Peao)
            {
                if (origem.coluna != destino.coluna && pecaCapturada == null)
                {
                    Posicao posP;
                    if (p.color == Color.White)
                    {
                        posP = new Posicao(destino.linha + 1, destino.coluna);
                    }
                    else
                    {
                        posP = new Posicao(destino.linha - 1, destino.coluna);
                    }
                    pecaCapturada = tab.retirarPeca(posP);
                    capture.Add(pecaCapturada);
                }
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

            //#jogadaespecial Roque pequeno
            if (p is King && destino.coluna == origem.coluna + 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1);
                Piece T = tab.retirarPeca(destinoT);
                T.decrementarQtdMovimentos();
                tab.putPiece(T, origemT);

            }
            //#jogadaespecial Roque grande
            if (p is King && destino.coluna == origem.coluna - 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1);
                Piece T = tab.retirarPeca(destinoT);
                T.decrementarQtdMovimentos();
                tab.putPiece(T, origemT);

            }
            // #jogada especial en passant
            if(p is Peao)
            {
                if (origem.coluna != destino.coluna && pecaCapturada == vulneravelEnPassanat)
                {
                    Piece peao = tab.retirarPeca(destino);
                    Posicao posP;
                    if(p.color == Color.White)
                    {
                        posP = new Posicao(3, destino.coluna);
                    }
                    else
                    {
                        posP = new Posicao(4, destino.coluna);
                    }
                    tab.putPiece(peao, posP);
                }
            }
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

            Piece p = tab.piece(destino);

            //#jogada especial en passant
            if ( p is Peao && (destino.linha == origem.linha -2 || destino.linha == origem.linha +2))
            {
                vulneravelEnPassanat = p;
            }
            else
            {
                vulneravelEnPassanat = null;
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
            colocarNovaPeca('a', 2, new Peao(Color.White, tab, this));
            colocarNovaPeca('b', 2, new Peao(Color.White, tab, this));
            colocarNovaPeca('c', 2, new Peao(Color.White, tab, this));
            colocarNovaPeca('d', 2, new Peao(Color.White, tab, this));
            colocarNovaPeca('e', 2, new Peao(Color.White, tab, this));
            colocarNovaPeca('f', 2, new Peao(Color.White, tab, this));
            colocarNovaPeca('g', 2, new Peao(Color.White, tab, this));
            colocarNovaPeca('h', 2, new Peao(Color.White, tab, this));
            colocarNovaPeca('a', 1, new Tower(Color.White, tab));
            colocarNovaPeca('b', 1, new Cavalo(Color.White, tab));
            colocarNovaPeca('c', 1, new Bispo(Color.White, tab));
            colocarNovaPeca('d', 1, new Dama(Color.White, tab));
            colocarNovaPeca('e', 1, new King(Color.White, tab, this));
            colocarNovaPeca('f', 1, new Bispo(Color.White, tab));
            colocarNovaPeca('g', 1, new Cavalo(Color.White, tab));
            colocarNovaPeca('h', 1, new Tower(Color.White, tab));


            colocarNovaPeca('a', 7, new Peao(Color.Black, tab,this));
            colocarNovaPeca('b', 7, new Peao(Color.Black, tab, this));
            colocarNovaPeca('c', 7, new Peao(Color.Black, tab, this));
            colocarNovaPeca('d', 7, new Peao(Color.Black, tab, this));
            colocarNovaPeca('e', 7, new Peao(Color.Black, tab, this));
            colocarNovaPeca('f', 7, new Peao(Color.Black, tab, this));
            colocarNovaPeca('g', 7, new Peao(Color.Black, tab, this));
            colocarNovaPeca('h', 7, new Peao(Color.Black, tab, this));
            colocarNovaPeca('a', 8, new Tower(Color.Black, tab));
            colocarNovaPeca('b', 8, new Cavalo(Color.Black, tab));
            colocarNovaPeca('c', 8, new Bispo(Color.Black, tab));
            colocarNovaPeca('d', 8, new Dama(Color.Black, tab));
            colocarNovaPeca('e', 8, new King(Color.Black, tab, this));
            colocarNovaPeca('f', 8, new Bispo(Color.Black, tab));
            colocarNovaPeca('g', 8, new Cavalo(Color.Black, tab));
            colocarNovaPeca('h', 8, new Tower(Color.Black, tab));

        }
    }
}
