using System;
using System.Collections.Generic;
using System.Linq;

class Test {

  public static int CharlietheDog(string[] strArr) {

    // code goes here 
    char[,] grid = new char[4, 4];
    (int x, int y) charliePos = (0, 0), homePos = (0, 0);
    List<(int x, int y)> foodPositions = new List<(int x, int y)>();
    for (int i = 0; i < 4; i++)
    {
      for (int j = 0; j < 4; j++)
      {
          grid[i,j] = strArr[i][j];
          if(grid[i,j] == 'C')
          {
              charliePos = (i,j);
          }
          else if(grid[i,j] == 'H'){
            homePos = (i,j);
          }
          else if(grid[i,j] == 'F')
          {
            foodPositions.Add((i,j));
          }
      }
    }

    return ShortestPath(grid, charliePos, homePos, foodPositions);
  }

  public static int ShortestPath(
    char[,] grid, 
    (int x, int y) start,
    (int x, int y) home,
    List<(int x, int y)> foodPositions)
    {
        Queue<(int x, int y, int steps, HashSet<(int x, int y)> collectedFood)> queue = new();
        HashSet<string> visited = new HashSet<string>();

        queue.Enqueue((start.x, start.y, 0, new HashSet<(int x, int y)>()));

        while (queue.Count > 0)
        {
            var(x, y, steps, collectedFood) = queue.Dequeue();

            if(collectedFood.Count == foodPositions.Count && x == home.x && y == home.y)
            {
              return steps;
            }

            string visitedKey = $"{x}, {y}, {string.Join(",", collectedFood.Select(f => $"{f.x}, {f.y}"))}";
            if(visited.Contains(visitedKey))
            {
                continue;
            }

            visited.Add(visitedKey);

            foreach(var food in foodPositions)
            { 
                if(x==food.x && y == food.y && !collectedFood.Contains(food))
                {
                    var newCollectedFood = new HashSet<(int x, int y)>(collectedFood);
                    newCollectedFood.Add(food);
                    collectedFood = newCollectedFood;
                    break;
                }
            }

            if(x== home.x && y== home.y && collectedFood.Count != foodPositions.Count)
            {
                continue;
            }

            int[] directionX = {-1, 1, 0, 0};
            int[] directionY = {0,0,-1,1};

            for(int i = 0; i < 4; i++)
            {
                int newX = x + directionX[i];
                int newY = y + directionY[i];

                if(newX >= 0 && newX < 4 && newY >=0 && newY <4)
                {
                    queue.Enqueue((newX, newY, steps+1, collectedFood));
                }
            }
        }
        return -1;
    }

  // keep this function call here
  static void Main() 
  {

    // Console.WriteLine(CharlietheDog(Console.ReadLine()));
    
  } 

}