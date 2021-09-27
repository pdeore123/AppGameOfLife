using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public struct Neighbours
    {
        private int row;
        private int col;

        public int GetX { get { return row; } set { row = value; } }
        public int GetY { get { return col; } set { col = value; } }

        public Neighbours(int row, int col)
        {
            this.row = row;
            this.col = col;
        }

    }

    public struct Cell
    {
        private bool isLive;
        public bool State { get { return isLive; } set { isLive = value; } }
        public Neighbours[] neighbours;
    }

    public class Helper
    {
        internal Cell[,] CreateGrid(int size = 25)
        {
            Cell[,] generation = new Cell[size, size];

            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    if (row > 0 && row < size - 1 && col > 0 && col < size - 1)
                    {
                        generation[row, col].neighbours = new Neighbours[]
                        {
                            new Neighbours(row,col-1),
                            new Neighbours(row,col+1),
                            new Neighbours(row-1,col-1),
                            new Neighbours(row-1,col+1),
                            new Neighbours(row+1,col-1),
                            new Neighbours(row+1,col+1),
                            new Neighbours(row-1,col),
                            new Neighbours(row+1,col)
                        };
                    }
                }
            }

            for (int col = 0; col < size; col++)
            {
                int row = 0;
                if (col == 0)
                {
                    generation[row, col].neighbours = new Neighbours[]
                    {
                        new Neighbours(row,col+1),
                        new Neighbours(row+1,col),
                        new Neighbours(row+1,col+1)
                    };
                }
                else if (col == size - 1)
                {
                    generation[row, col].neighbours = new Neighbours[]
                    {
                        new Neighbours(row,col-1),
                        new Neighbours(row+1, col-1),
                        new Neighbours(row +1 , col)
                    };
                }
                else if (col > 0 && col < size - 1)
                {
                    generation[row, col].neighbours = new Neighbours[]
                    {
                        new Neighbours(row,col-1),
                        new Neighbours(row,col+1),
                        new Neighbours(row+1,col-1),
                        new Neighbours(row+1,col),
                        new Neighbours(row+1,col+1)
                    };
                }

                row = size - 1;
                if (col == 0)
                {
                    generation[row, col].neighbours = new Neighbours[]
                    {
                        new Neighbours(row-1,col),
                        new Neighbours(row,col+1),
                        new Neighbours(row-1,col+1)

                    };
                }
                else if (col == size - 1)
                {
                    generation[row, col].neighbours = new Neighbours[]
                    {
                        new Neighbours(row,col -1),
                        new Neighbours(row-1,col-1),
                        new Neighbours(row-1,col)
                    };
                }
                else if (col > 0 && col < size - 1)
                {
                    generation[row, col].neighbours = new Neighbours[]
                    {
                        new Neighbours(row,col-1),
                        new Neighbours(row,col+1),
                        new Neighbours(row-1,col-1),
                        new Neighbours(row-1,col),
                        new Neighbours(row-1,col+1)
                    };
                }
            }

            for (int row = 1; row < size - 1; row++)
            {
                int col = 0;

                generation[row, col].neighbours = new Neighbours[]
                {
                    new Neighbours(row -1,col),
                    new Neighbours(row+1,col),
                    new Neighbours(row,col+1),
                    new Neighbours(row-1,col+1),
                    new Neighbours(row+1,col+1)
                };

                col = size - 1;
                generation[row, col].neighbours = new Neighbours[]
                {
                    new Neighbours(row,col-1),
                    new Neighbours(row-1,col-1),
                    new Neighbours(row-1,col),
                    new Neighbours(row+1,col-1),
                    new Neighbours(row+1,col)
                };
            }

            return generation;
        }

        internal Cell[,] Play(Cell[,] grid)
        {
            var newGeneration = (Cell[,])grid.Clone();
            for (int row = 0; row < grid.Length / 25 -1; row++)
            {
                for (int col = 0; col < grid.Length / 25 -1; col++)
                {
                    Cell cell = grid[row, col];
                    bool isLive = UpdateGeneration(cell.State,cell.neighbours,grid);
                    newGeneration[row, col].State = isLive;
                }
            }
            return newGeneration;
        }

        internal Cell[,] ApplyGlider(Cell[,] grid)
        {
            grid[3, 2].State = true;
            grid[4, 2].State = true;
            grid[5, 2].State = true;
            grid[5, 3].State = true;
            grid[4, 4].State = true;

            return grid;
        }

        internal bool UpdateGeneration(bool currState, Neighbours[] neighbours, Cell[,] currGeneration)
        {
            int count = 0;
            foreach (var n in neighbours)
            {
                if (currGeneration[n.GetX, n.GetY].State) count++;
            }

            if(currState)
            {
                if (count < 2)
                    currState = false;
                if (count > 3)
                    currState = false;
                if (count == 2 || count == 3)
                    currState = true;
            }
            else
            {
                if (count == 3)
                    currState = true;
            }

            return currState;
        }
    }
}
