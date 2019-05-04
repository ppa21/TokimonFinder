using System;
using System.Collections.Generic;

namespace TokimonFinder {
    public class Tokimon {
    private Random rand;
    private HashSet<int> UniqueCoordinates;
    private List<int> Coordinates;
    private List<int> XVal;
    private List<int> YVal;
    private List<int> OrderedPair;
    private int tokiNum;
    private int count;

    public Tokimon(int numToki) {
        tokiNum = numToki;
        rand = new Random(110);
        UniqueCoordinates = new HashSet<int>();

        AssignCoordinates();

        Coordinates = new List<int>(UniqueCoordinates);
        XVal = new List<int>();
        YVal = new List<int>();
        OrderedPair = new List<int>();

        TokiCoordinateParser();
    }

    // assigns randoms spots to tokimons in the grid
    private void AssignCoordinates() {
        while(UniqueCoordinates.Count != tokiNum) {
            Random rand = new Random();
            int coor = rand.Next(1, 110);

            UniqueCoordinates.Add(coor);
        }
    }

    // parses the coordinates into x and y values
    private void TokiCoordinateParser() {
        Fokimon.CoordinateParser(Coordinates, rand, XVal, YVal);
    }

    // returns the ordered pair of the fokimon location
    public List<int> PickTokiSpot(int x, int y) {
        OrderedPair.Clear();

        for(int i = 0; i < Coordinates.Count; i++) {

            if(XVal.Contains(x) && YVal.Contains(y)) {
                for(int j = 0; j < XVal.Count; j++) {
                    if(XVal[j] == x && YVal[j] == y) {
                        OrderedPair.Add(x);
                        OrderedPair.Add(y);

                        count++;

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

    public HashSet<int> GetUniqueCoordinates() {
        return UniqueCoordinates;
    }

    public List<int> GetXToki() {
        return XVal;
    }

    public List<int> GetYToki() {
        return YVal;
    }
    }
}