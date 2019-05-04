using System;
using System.Collections.Generic;

namespace TokimonFinder {
    public class Fokimon {
    private Random rand;
    private HashSet<int> FokiUniqueCoordinates;
    private HashSet<int> TokiUniqueCoordinates;
    private List<int> Coordinates;
    private List<int> XVal;
    private List<int> YVal;
    private List<int> OrderedPair;
    private int fokiNum;
    private int count;

    public Fokimon(int numFoki) {
        fokiNum = numFoki;
        rand = new Random(110);
        FokiUniqueCoordinates = new HashSet<int>();
        TokiUniqueCoordinates = new HashSet<int>();
    }

    public void SetFokiUniqueCoordinates(HashSet<int> setToki) {
        TokiUniqueCoordinates = setToki;

        AssignCoordinates();

        Coordinates = new List<int>(FokiUniqueCoordinates);
        XVal = new List<int>();
        YVal = new List<int>();
        OrderedPair = new List<int>();

        FokiCoordinateParser();
    }

    // chooses where to place fokimons
    private void AssignCoordinates() {
        while(FokiUniqueCoordinates.Count != fokiNum) {
            Random rand = new Random();
            int coor = rand.Next(1, 110);
            
            if(!TokiUniqueCoordinates.Contains(coor)) {
                FokiUniqueCoordinates.Add(coor);
            }
        }
    }

    private void FokiCoordinateParser() {
        CoordinateParser(Coordinates, rand, XVal, YVal);
    }

    // parses the coordinates into x and y values
    public static void CoordinateParser(List<int> coordinates, Random rand, List<int> xVal, List<int> yVal) {
        for(int i = 0; i < coordinates.Count; i++) {
            int a = coordinates[i];

            if(a < 10) {
                int b = rand.Next(9);

                xVal.Add(a);
                yVal.Add(b);
            } else if(a <= 99) {
                String tmo = Convert.ToString(a);

                xVal.Add(Convert.ToInt32(tmo[0]));
                yVal.Add(Convert.ToInt32(tmo[1]));
            } else {
                String tmo = Convert.ToString(a);
                
                xVal.Add(10);
                yVal.Add(Convert.ToInt32(tmo[2]));
            }
        }
    }

    // returns the ordered pair of the fokimon location
    public List<int> PickFokiSpot(int x, int y) {
        OrderedPair.Clear();

        int oPairSize = Coordinates.Count;
        int xValSize = XVal.Count;

        for(int i = 0; i < oPairSize; i++) {
            if(XVal.Contains(x) && YVal.Contains(y)) {
                for(int j = 0; j < xValSize; j++) {
                    if(XVal[j] == x && YVal[j] == y) {
                        count++;

                        OrderedPair.Add(x);
                        OrderedPair.Add(y);

                        return OrderedPair;
                    }
                }
            }
        }

        OrderedPair.Add(-1);
        OrderedPair.Add(-1);

        return OrderedPair;
    }

    public int GetCount() {
        return count;
    }

    public List<int> GetxVal() {
        return XVal;
    }

    public List<int> GetyVal() {
        return YVal;
    }

    public void KillFoki() {
        if(fokiNum > 0) {
            fokiNum--;

            Random rand = new Random();
            int fokiToBeKilled = rand.Next(fokiNum + 1);

            XVal.Remove(fokiToBeKilled);
            YVal.Remove(fokiToBeKilled);
        } else {
            Console.WriteLine("\nNo Fokimon remaining");
        }
    }
    }
}