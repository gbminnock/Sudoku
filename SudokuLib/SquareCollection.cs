using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuLib
{

    // simple collection class
    public class SquareCollection : System.Collections.CollectionBase
    {
        // add
        public void Add(Square square)
        {
            List.Add(square);
        }

        // remove
        public void Remove(int index)
        {
            if (index > Count - 1 || index < 0)
            {
                throw new IndexOutOfRangeException("Index does not exist.", 
                    new Exception("at position: " + index + ". Length of collection is: " + Count));
            }
            else
            {
                List.RemoveAt(index);
            }
        }

        // indexed position
        public Square Item(int index)
        {
            return (Square)List[index];
        }

        // Sort
        public void Sort(string SortExpression, SortOrder Order)
        {
            SquareCollectionComparer comp = new SquareCollectionComparer(SortExpression, Order);
            this.InnerList.Sort(comp);
        }  


    }
}
