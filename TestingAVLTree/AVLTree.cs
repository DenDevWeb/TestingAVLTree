using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingAVLTree
{
    class AVLTree<T> : IComparable<T> where T : IComparable
    {
        public AVLTree(T value, AVLTree<T> parent)
        {
        }

        public int CompareTo(T other)
        {
            return 5;
        }
    }
}
