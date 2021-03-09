using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace xadrez_console
{
    class Tela
    {
        public static void printBoard (Tabuleiro tab)
        {
            for (int i=0; i<tab.linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j=0; j<tab.colunas; j++)
                {
                    if (tab.piece(i, j)==null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        printPiece(tab.piece(i, j));
                        Console.Write("- ");
                    }
                }

                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void printPiece(Piece piece)
        {
            if ( piece.color == Color.White)
            {
                Console.Write(piece);
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(piece);
                Console.ForegroundColor = aux;
            }
        }
    }
}
