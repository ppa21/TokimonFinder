using System;
using System.Collections.Generic;

namespace TokimonFinder {
    public class GameLogic {
    private int xCoor;
    private int yCoor;
    private char yCoorChar;
    private string[,] gameGrid;
    private int SIZE = 10;

    public void SetGameGrid(string[,] gameGrid) {
        this.gameGrid = gameGrid;
    }

    public void PrintUpdatedGrid() {
        RowIdentity();
        char columnIdentity = 'A';

        for(int i = 0; i < SIZE; i++) {
            Console.Write(columnIdentity + " ");
            columnIdentity++;

            for (int j = 0; j < 10; j++) {
                Console.Write(gameGrid[i, j]);
            }

            Console.WriteLine();
        }
    }

    private void RowIdentity() {
        Console.WriteLine(" ");

        for(int i = 0; i < SIZE; i++) {
            Console.Write("  " + (i + 1));
        }

        Console.WriteLine();
    }

    // lets the user move in the grid
    public void MoveUser(int xPos, char yPos) {
        xCoor = xPos;

        int yVal = yPos - 'a' + 1;
        yVal--;

        yCoor = yVal;
        xCoor--;

        if(xCoor < 0) {
            xCoor = 0;
        }

        gameGrid[yCoor, xCoor] = " @ ";
    }

    // updates the grid with user's movement
    public void UpdateGrid() {
        gameGrid[yCoor, xCoor] = gameGrid[yCoor, xCoor].Replace('@', ' ');
    }

    // if the user's is on the same grid as the tokimon, it is revealed
    public void RevealToki(List<int> oPair) {
        if((oPair[0] != -1 && oPair[1] != -1)) {
            gameGrid[yCoor, xCoor] = " @$";
        }
    }

    public void SetTokiInGrid(int x, int y) {
        gameGrid[x, y] = "  $";
    }

    // if the user's is on the same grid as the fokimon, it is revealed
    public void RevealFoki(List<int> oPair) {
        if((oPair[0] != -1 && oPair[1] != -1)) {
            gameGrid[yCoor, xCoor] = " @X";
        }
    }

    // updates user's coordinates
    public void UpdateCoordinates(String choice) {
        if(choice.Trim().ToLower().Equals("w")) {
            yCoorChar--;
        } else if (choice.Trim().ToLower().Equals("s")) {
            yCoorChar++;
        } else if (choice.Trim().ToLower().Equals("d")) {
            xCoor++;
        } else if (choice.Trim().ToLower().Equals("a")) {
            xCoor--;
        }
    }

    public char GetyCoorChar() {
        return yCoorChar;
    }

    public void SetyCoorChar(char tmpYCor) {
        this.yCoorChar = tmpYCor;
    }

    public int GetxCoor() {
        return xCoor;
    }

    public void SetxCoor(int x) {
        this.xCoor = x;
    }

    public int GetyCoor() {
        return yCoor;
    }

    public void SetyCoor(String choice) {
        if(choice.Equals("w")) {
            yCoor--;
        } else if(choice.Equals("s")) {
            yCoor++;
        }
    }

    public void PrintOptions() {
       Console.WriteLine("\nEnter W, A, S, or D appropriately to move: ");
    }

    public string ValidateInput(string choice) {
        while(choice.Trim().ToLower().Length != 1) {
            PrintOptions();
            choice = Console.ReadLine();
            choice = choice.Trim().ToLower();
        }

        return choice;
    }

    // all the functions from here on are bound check functions.
    // They check if the user tries to go off the grid.
    public string BoundCheckLeftXCoor(string choice) {
        while(xCoor == 1) {
            if(choice.Equals("a")) {
                PrintOptions();
                choice = Console.ReadLine();
                choice = choice.Trim().ToLower();
            } else if(yCoorChar == 'a' && choice.Equals("w")) {
                choice = HighRightBouncCheck(choice);
            } else if(yCoorChar == 'j' && choice.Equals("s")) {
                choice = LowRightBoundCheck(choice);
            } else {
                break;
            }
        }

        return choice;
    }

    public string BoundCheckRightXCoor(string choice) {
        while(xCoor == 10) {
            if(choice.Equals("d")) {
                PrintOptions();
                choice = Console.ReadLine();
                choice = choice.Trim().ToLower();
            } else if(yCoorChar == 'a' && choice.Equals("w")) {
                choice = HighRightBouncCheck(choice);
            } else if(yCoorChar == 'j' && choice.Equals("s")) {
                choice = LowRightBoundCheck(choice);
            } else {
                break;
            }
        }

        return choice;
    }

    private string HighRightBouncCheck(string choice) {
        PrintOptions();
        choice = Console.ReadLine();
        choice = choice.Trim().ToLower();

        return choice;
    }

    private string LowRightBoundCheck(string choice) {
        PrintOptions();
        choice = Console.ReadLine();
        choice = choice.Trim().ToLower();

        return choice;
    }

    public string BoundCheckLowYCoor(string choice) {
        while(yCoorChar == 'a') {
            if(choice.Equals("w")) {
                PrintOptions();
                choice = Console.ReadLine();
                choice = choice.Trim().ToLower();
            } else {
                break;
            }
        }

        return choice;
    }

    public string BoundCheckHighYCoor(string choice) {
        while(yCoorChar == 'j') {
            if(choice.Equals("s")) {
                PrintOptions();
                choice = Console.ReadLine();
                choice = choice.Trim().ToLower();
            } else {
                break;
            }
        }

        return choice;
    }
    }
}