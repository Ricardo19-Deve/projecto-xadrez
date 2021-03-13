using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tabuleiro
{
     abstract class Piece
    {
        public Posicao posicao { get; set; }
        public Color color { get; protected set; }
        public int qntMovements { get; protected set; }
        public Tabuleiro tab { get; protected set; }

        public Piece( Color color, Tabuleiro tab)
        {
            this.posicao = null;
            this.color = color;
            this.tab = tab;
            this.qntMovements = 0;
        }

        public void incrementarQtdMovimentos()
        {
            qntMovements++;
        }

        public void decrementarQtdMovimentos()
        {
            qntMovements--;
        }

        public bool existeMovimentosPossiveis()
        {
            bool[,] mat = moviementosPossiveis();
            for(int i =0; i<tab.linhas; i++)
            {
                for( int j = 0; j<tab.colunas; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                } 
            }
            return false;
        }
        public bool podeMoverPara(Posicao pos)
        {
            return moviementosPossiveis()[pos.linha, pos.coluna];
        }
        public abstract bool[,] moviementosPossiveis();
        

        
    }
}
