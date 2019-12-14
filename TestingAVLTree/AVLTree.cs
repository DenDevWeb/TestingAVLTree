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
        /// Левый потомок
        /// </summary>
        AVLTree<T> _left;
        /// <summary>
        /// Правый потомок
        /// </summary>
        AVLTree<T> _right;

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

        /// <summary>
        /// Сравнивает текущий узел по указаному значению, возвращет 1, если значение экземпляра больше переданного значения, 
        /// возвращает -1, когда значение экземпляра меньше переданого значения, 0 - когда они равны.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(T other)
        {
            return Value.CompareTo(other);
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
        /// Левый потомок
        /// </summary>
        public AVLTree<T> Left
        {
            get
            {
                return _left;
            }

            internal set
            {
                _left = value;

                if (_left != null)
                {
                    _left.Parent = this;  // установка указателя на родительский элемент
                }
            }
        }

        /// <summary>
        /// Правый потомок
        /// </summary>
        public AVLTree<T> Right
        {
            get
            {
                return _right;
            }

            internal set
            {
                _right = value;

                if (_right != null)
                {
                    _right.Parent = this; // установка указателя на родительский элемент
                }
            }
        }

        /// Разница между правым и левым поддеревом
        /// <summary>
        /// Разница между правым и левым поддеревом:
        /// больше 1 - перевес справа,
        /// меньше 1 - первес слева,
        /// иначе - сбалансировано
        /// </summary>
        private int BalanceFactor
        {
            get
            {
                return MaxChildHeight(Right) - MaxChildHeight(Left);
            }
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
        /// Рекурсивно находит высоту дерева
        /// </summary>
        /// <param name="node">Корень, с которого считаем высоту</param>
        /// <returns></returns>
        public static int MaxChildHeight(AVLTree<T> node)
        {
            if (node != null)
            {
                return 1 + Math.Max(MaxChildHeight(node.Left), MaxChildHeight(node.Right));
            }

            return 0;
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
            // Вариант 1: Добавление нового узла в дерево. Значение добавлемого узла меньше чем значение текущего узла.      

            if (value.CompareTo(node.Value) < 0)
            {
                //Создание левого узла, если его нет.

                if (node.Left == null)
                {
                    node.Left = new AVLTree<T>(value, node);
                }

                else
                {
                    // Переходим к следующему левому узлу
                    AddTo(node.Left, value);
                }
            }
            // Вариант 2: Добавлемое значение больше или равно текущему значению.

            else
            {
                //Создание правого узла, если его нет.         
                if (node.Right == null)
                {
                    node.Right = new AVLTree<T>(value, node);
                }
                else
                {
                    // Переход к следующему правому узлу.             
                    AddTo(node.Right, value);
                }
            }
            node.Balance();
        }

        public void Balance()
        {
            if (this.BalanceFactor > 1)
            {
                if (Right != null && Right.BalanceFactor < -1)
                {
                    RightLeftRotation();
                }

                else
                {
                    LeftRotation();
                }
            }
        }

        public void LeftRotation()
        {

            // До
            //     12(this)     
            //      \     
            //       15     
            //        \     
            //         25     
            //     
            // После     
            //       15     
            //      / \     
            //     12  25  

            // Сделать правого потомка новым корнем дерева.
            AVLTree<T> newRoot = Right;
            ReplaceRoot(newRoot);

            // Поставить на место правого потомка - левого потомка нового корня.    
            Right = newRoot.Left;
            // Сделать текущий узел - левым потомком нового корня.    
            newRoot.Left = this;
        }

        public void RightLeftRotation()
        {
            Right.RightRotation();
            LeftRotation();
        }

        public void RightRotation()
        {
            // Было
            //     c (this)     
            //    /     
            //   b     
            //  /     
            // a     
            //     
            // Стало    
            //       b     
            //      / \     
            //     a   c  

            // Левый узел текущего элемента становится новым корнем
            AVLTree<T> newRoot = Left;
            ReplaceRoot(newRoot);

            // Перемещение правого потомка нового корня на место левого потомка старого корня
            Left = newRoot.Right;

            // Правым потомком нового корня, становится старый корень.     
            newRoot.Right = this;
        }

        public void ReplaceRoot(AVLTree<T> newRoot)
        {
            if (this.Parent != null)
            {
                if (this.Parent.Left == this)
                {
                    this.Parent.Left = newRoot;
                }
                else if (this.Parent.Right == this)
                {
                    this.Parent.Right = newRoot;
                }
            }
            else
            {
                AVLTree<T> h = Head();
                h = newRoot;
            }

            newRoot.Parent = this.Parent;
            this.Parent = newRoot;
        }
    }
}
