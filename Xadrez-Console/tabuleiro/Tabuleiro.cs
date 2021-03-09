using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tabuleiro
{
    class Tabuleiro
    {
        public int linhas { get; set; }
        public int colunas { get; set; }
        private Piece[,] pieces;

        public Tabuleiro(int linhas, int colunas)
        {
            this.linhas = linhas;
            this.colunas = colunas;
            pieces = new Piece[linhas, colunas];
        }

        public Piece piece(int linha, int coluna)
        {
            return pieces[linha, coluna];
        }

        public Piece piece (Posicao pos)
        {
            return pieces[pos.linha, pos.coluna];
        }

        public bool existePeca(Posicao pos)
        {
            validatePosition(pos);
            return piece(pos) != null;
        }
        public void putPiece(Piece p, Posicao pos)
        {
            if (existePeca(pos))
            {
                throw new BoardException("Já existe uma peça nessa posição");
            }
            pieces[pos.linha, pos.coluna] = p;
            p.posicao = pos;
        }

        public bool validePosition(Posicao pos)
        {
            if (pos.linha<0 || pos.linha>=linhas || pos.coluna<0 || pos.coluna >= colunas)
            {
                return false;
            }
            return true;
        }

        public void validatePosition(Posicao pos)
        {
            if (! validePosition(pos))
            {
                throw new BoardException("Posição inválida");
            }
        }
    }
}
