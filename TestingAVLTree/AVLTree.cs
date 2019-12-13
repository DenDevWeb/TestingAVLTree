using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingAVLTree
{
    class AVLTree<T> : IComparable<T> where T : IComparable
    {
        /// <summary>
        /// Количество узлов дерева
        /// </summary>
        public int Count
        {
            get;
            private set;
        }

        /// <summary>
        /// Значение текущего узла
        /// </summary>
        public T Value
        {
            get;
            private set;
        }

        public AVLTree(T value, AVLTree<T> parent)
        {
            Value = value;
            Parent = parent;
        }

        public int CompareTo(T other)
        {
            return 5;
        }

        /// <summary>
        /// Указатель на родительский узел
        /// </summary>
        public AVLTree<T> Parent
        {
            get;
            internal set;
        }

        /// <summary>
        /// Нахождение корня дерева
        /// </summary>
        /// <returns>Корень дерева</returns>
        public AVLTree<T> Head()
        {
            AVLTree<T> current = this;
            while (current.Parent != null)
            {
                current = current.Parent;
            }

            return current;
        }

        /// <summary>
        /// Метод добавлет новый узел
        /// </summary>
        /// <param name="value">Добавляемое начение</param>
        public void Add(T value)
        {
            AddTo(Head(), value);

            Count++;
        }

        /// <summary>
        /// Алгоритм рекурсивного добавления нового узла в дерево.
        /// </summary>
        /// <param name="node">Узел</param>
        /// <param name="value">Значение</param>
        private void AddTo(AVLTree<T> node, T value)
        {
        }
    }
}
