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
            //try
            //{
            //    Tabuleiro tab = new Tabuleiro(8, 8);

            //    tab.putPiece(new Tower(Color.Black, tab), new Posicao(0, 0));
            //    tab.putPiece(new Tower(Color.Black, tab), new Posicao(1, 9));
            //    tab.putPiece(new King(Color.White, tab), new Posicao(0, 2));

            //    Tela.printBoard(tab);
            //}
            //catch (BoardException e)
            //{
            //    Console.WriteLine(e.Message);
            //}

            PosicaoXadrez pos = new PosicaoXadrez('c', 7);

            Console.WriteLine(pos);

            Console.WriteLine(pos.toPosicao());
            Console.ReadLine();


        }
    }
}
