using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;
using xadrez;




namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {

            Tabuleiro tab = new Tabuleiro(8, 8);

            tab.putPiece(new Tower(Color.Black, tab), new Posicao(0, 0));
            tab.putPiece(new Tower(Color.Black, tab), new Posicao(1, 3));
            tab.putPiece(new King(Color.White, tab), new Posicao(2, 4));

            Tela.printBoard(tab);


            Console.ReadLine();


        }
    }
}
