using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tabuleiro
{
    class Piece
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
    }
}
