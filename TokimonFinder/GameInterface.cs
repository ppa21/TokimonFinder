using System;

namespace TokimonFinder {
    public class GameInterface {
        private static int NumRow = 10;
        private static int NumCol = 10;
        private string[,] grid = new String[NumRow, NumCol];
        private int Size = 10;

        public GameInterface() {
            for(int i = 0; i < NumRow; i++) {
                for (int j = 0; j < NumCol; j++) {
                    grid[i, j] = " ~ ";
                }
            }
        }

        public string[,]  GetGrid(){
            return grid;
        }

        public void PrintGrid() {
            Console.WriteLine("\nGame has started \n");

            RowIdentity();
            char columnIdentity = 'A';
            
            for(int i = 0; i < Size; i++) {
                Console.Write(columnIdentity + " ");
                columnIdentity++;

                for (int j = 0; j < 10; j++) {
                    Console.Write(grid[i, j]);
                }

                Console.WriteLine();
            }
        }

        private void RowIdentity() {
            Console.Write(" ");

            for(int i = 0; i < NumRow; i++) {
                Console.Write("  " + (i + 1));
            }

            Console.WriteLine();
        }
    }
}