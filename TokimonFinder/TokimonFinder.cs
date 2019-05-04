using System;
using System.Collections.Generic;
using System.Text;

namespace TokimonFinder {
    internal class TokimonFinder {
        
    private static int numOfToki;
    private static int numOfFoki;
    private static char yCoordinate = (char) 0;
    private static int xCoordinate;

    public static void Main(string[] args) {
        if(args.Length > 2) {
            Console.WriteLine("Wrong number of arguments");
            Environment.Exit(-1);
        }

        if(args.Length == 0) {
            numOfToki = 10;
            numOfFoki = 5;
        } else if(args.Length == 1) {
            ExtractTokimon(args);
            numOfFoki = 5;
        } else {
            ExtractTokimon(args);
            ExtractFokimon(args);
        }

        Tokimon toki = new Tokimon(numOfToki);
        Fokimon foki = new Fokimon(numOfFoki);
        foki.SetFokiUniqueCoordinates(toki.GetUniqueCoordinates());         // sets fokimon in the grid

        GameInterface game = new GameInterface();

        game.PrintGrid();

        bool xValSet = true;

        string choice = "";

        // gets user's initial coordinate
        while(xValSet) {
            Console.WriteLine("\nPick a coordinate where you want to start: ");
            choice = Console.ReadLine();
            choice = choice.Trim().ToLower();

            yCoordinate = choice[0];

            if(choice.Length > 1 && char.IsDigit(choice[1])) {
                xCoordinate = Convert.ToInt32(choice[1]) - 48;
                xValSet = false;
            }
        }

        // evaluate user input
        while(choice.Length <= 1 || yCoordinate < 'a' || yCoordinate > 'j' || xCoordinate < 1 || xCoordinate > 10) {
            Console.WriteLine("\nPlease enter a valid coordinate: ");
            choice = Console.ReadLine();
            choice = choice.Trim().ToLower();

            yCoordinate = choice[0];

            if(choice.Length > 1 && char.IsDigit(choice[1])) {
                xCoordinate = Convert.ToInt32(choice[1]) - 48;
            }
        }

        GameLogic logic = new GameLogic();
        logic.SetGameGrid(game.GetGrid());

        List<int> TokiOrderedPair;
        List<int> FokiOrderedPair;

        List<string> InputList = new List<string>(new string[] {"w", "a", "s", "d"});

        while(true) {
            Console.WriteLine();

            // gets x and y values for tokimon and fokimon
            TokiOrderedPair = toki.PickTokiSpot(logic.GetxCoor(), logic.GetyCoor());
            FokiOrderedPair = foki.PickFokiSpot(logic.GetxCoor(), logic.GetyCoor());

            if(xCoordinate < 0) {
                xCoordinate = 0;
            }

            logic.MoveUser(xCoordinate, yCoordinate);           // lets the user move in the grid
            logic.RevealToki(TokiOrderedPair);                  // checks to see if the user is on the same grid as toki
            logic.RevealFoki(FokiOrderedPair);                  // checks to see if the user is on the same grid as foki

            logic.PrintUpdatedGrid();

            // if all tokimons are revealed
            if(numOfToki == toki.GetCount()) {
                Console.WriteLine("\nYou Win!\n");
                return;
            }

            // if one fokimon is revealed
            if(foki.GetCount() == 1) {
                Console.WriteLine("\nYou Lose!\n");
                return;
            }

            bool printCheck = true;

            // eval user's choice
            while(printCheck) {
                logic.PrintOptions();
                choice = Console.ReadLine();
                choice = choice.Trim().ToLower();

                if(!InputList.Contains(choice)) {
                    continue;
                }

                printCheck = false;
            }

            choice = logic.ValidateInput(choice);         // validate user's move

            logic.UpdateGrid();

            logic.SetxCoor(xCoordinate);
            logic.SetyCoorChar(yCoordinate);

                // check bound on user's move choice
            choice = logic.BoundCheckLeftXCoor(choice);
            choice = logic.BoundCheckRightXCoor(choice);
            choice = logic.BoundCheckLowYCoor(choice);
            choice = logic.BoundCheckHighYCoor(choice);

            logic.UpdateCoordinates(choice);
            logic.SetyCoor(choice);

            // update user's coordinate
            xCoordinate = logic.GetxCoor();
            yCoordinate = logic.GetyCoorChar();
        }
    }

    // extract tokimon args
    private static void ExtractTokimon(string[] args){
        StringBuilder tokiArg = new StringBuilder();
        string tokiNum;
        string tmp = args[0];

        for(int i = 0; i < tmp.Length; i++) {
            if(char.IsDigit(tmp[i])) {
                tokiArg.Append(tmp[i]);
                if(tmp[i - 1] == '-') {
                    Console.WriteLine("Enter a positive number of Tokimon.");
                    Environment.Exit(-1);
                }
            }
        }

        tokiNum = tokiArg.ToString();

        if(tokiNum.Equals("")) {
            tokiNum = "10";
        }

        numOfToki = Int32.Parse(tokiNum);

        if(numOfToki < 5) {
            Console.WriteLine("Number of Tokimon has to be greater than or equal to 5.");
            Environment.Exit(-1);
        }

        if(numOfToki > 100) {
            Console.WriteLine("More than 100 Tokimons. Please fix your input.");
            Environment.Exit(-1);
        }
    }

    // extract fokimon arguments
    private static void ExtractFokimon(string[] args) {
        StringBuilder fokiArg = new StringBuilder();
        string fokiNum;
        string tmp = args[1];
        
        for(int i = 0; i < tmp.Length; i++) {
            if(char.IsDigit(tmp[i])) {
                fokiArg.Append(tmp[i]);

                if(tmp[i - 1] == '-') {
                    Console.WriteLine("Enter a positive number of Fokimon.");
                    Environment.Exit(-1);
                }
            }
        }

        fokiNum = fokiArg.ToString();

        if(fokiNum.Equals("")) {
            fokiNum = "5";
        }

        numOfFoki = Int32.Parse(fokiNum);

        if(numOfFoki < 5) {
            Console.WriteLine("Number of Fokimon has to be greater than or equal to 5.");
            Environment.Exit(-1);
        }

        int tmpToki = numOfToki;
        int tmpFoki = numOfFoki;

        if((tmpToki + tmpFoki) > 100) {
            Console.WriteLine("Total of Tokimon and Fokimon is greater than 100. Please fix your input.");
            Environment.Exit(-1);
        }
    }
    }
}