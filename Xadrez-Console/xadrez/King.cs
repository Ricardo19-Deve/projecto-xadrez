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
        public King(Color color, Tabuleiro tab) : base(color, tab)
        {

        }

        public override string ToString()
        {
            return "R";
        }
    }
}
