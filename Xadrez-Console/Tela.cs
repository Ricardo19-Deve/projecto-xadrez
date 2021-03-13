using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Tela
    {
        public static void imprimirPartida(PartidaDeXadrez partida)
        {
            printBoard(partida.tab);

            Console.WriteLine();
            imprimirPecasCapturadas(partida);

            Console.WriteLine();

            Console.WriteLine("Turno: " + partida.turno);

            Console.WriteLine("Aguardando jogada: " + partida.jogadorAtual);

            if (partida.xeque)
            {
                Console.WriteLine("XEQUE!");
            }

            
        }

        public static void imprimirPecasCapturadas(PartidaDeXadrez partida)
        {
            Console.WriteLine("Peças Capturadas: ");
            Console.Write("Brancas: ");
            imprimirConjunto(partida.pecasCapturadas(Color.White));
            Console.WriteLine();
            Console.Write("Pretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            imprimirConjunto(partida.pecasCapturadas(Color.Black));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void imprimirConjunto(HashSet<Piece> conjunto)
        {
            Console.Write("[");
            foreach(Piece x in conjunto)
            {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }
        public static void printBoard(Tabuleiro tab)
        {
            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.colunas; j++)
                {

                    printPiece(tab.piece(i, j));

                }

                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void printBoard(Tabuleiro tab, bool[,] posicoesPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.Red;

            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.colunas; j++)
                {

                    if (posicoesPossiveis[i, j])
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }

                    printPiece(tab.piece(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }

                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");

            Console.BackgroundColor = fundoOriginal;
        }
        public static PosicaoXadrez lerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);

        }

        public static void printPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {

                if (piece.color == Color.White)
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
                Console.Write(" ");
            }

        }
    }
}
