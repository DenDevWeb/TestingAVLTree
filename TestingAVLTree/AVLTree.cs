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

        /// <summary>
        /// Удалить узел
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Remove(T value)
        {
            AVLTree<T> current;
            current = Find(value); // находим узел с удаляемым значением

            if (current == null) // узел не найден
            {
                return false;
            }

            AVLTree<T> treeToBalance = current.Parent; // баланс дерева относительно узла родителя
            Count--;                                       // уменьшение колиества узлов

            // Вариант 1: Если удаляемый узел не имеет правого потомка      

            if (current.Right == null) // если нет правого потомка
            {
                if (current.Parent == null) // удаляемый узел является корнем
                {
                    AVLTree<T> h = Head();
                    h = current.Left;    // на место корня перемещаем левого потомка

                    if (h != null)
                    {
                        h.Parent = null; // убераем ссылку на родителя  
                    }
                }
                else // удаляемый узел не является корнем
                {
                    int result = current.Parent.CompareTo(current.Value);

                    if (result > 0)
                    {
                        // Если значение родительского узла больше значения удаляемого,
                        // сделать левого потомка удаляемого узла, левым потомком родителя.  

                        current.Parent.Left = current.Left;
                    }
                    else if (result < 0)
                    {

                        // Если значение родительского узла меньше чем удаляемого,                 
                        // сделать левого потомка удаляемого узла - правым потомком родительского узла.                 

                        current.Parent.Right = current.Left;
                    }
                }
            }

            // Вариант 2: Если правый потомок удаляемого узла не имеет левого потомка, тогда правый потомок удаляемого узла
            // становится потомком родительского узла.      

            else if (current.Right.Left == null) // если у правого потомка нет левого потомка
            {
                current.Right.Left = current.Left;

                if (current.Parent == null) // текущий элемент является корнем
                {
                    AVLTree<T> h = Head();
                    h = current.Right;

                    if (h != null)
                    {
                        h.Parent = null;
                    }
                }
                else
                {
                    int result = current.Parent.CompareTo(current.Value);
                    if (result > 0)
                    {
                        // Если значение узла родителя больше чем значение удаляемого узла,                 
                        // сделать правого потомка удаляемого узла, левым потомком его родителя.                 

                        current.Parent.Left = current.Right;
                    }

                    else if (result < 0)
                    {
                        // Если значение родительского узла меньше значения удаляемого,                 
                        // сделать правого потомка удаляемого узла - правым потомком родителя.                 

                        current.Parent.Right = current.Right;
                    }
                }
            }

            // Вариант 3: Если правый потомок удаляемого узла имеет левого потомка,      
            // заместить удаляемый узел, крайним левым потомком правого потомка.     
            else
            {
                // Нахожление крайнего левого узла для правого потомка удаляемого узла.       

                AVLTree<T> leftmost = current.Right.Left;

                while (leftmost.Left != null)
                {
                    leftmost = leftmost.Left;
                }

                // Родительское правое поддерево становится родительским левым поддеревом.         

                leftmost.Parent.Left = leftmost.Right;

                // Присвоить крайнему левому узлу, ссылки на правого и левого потомка удаляемого узла.         
                leftmost.Left = current.Left;
                leftmost.Right = current.Right;

                if (current.Parent == null)
                {
                    AVLTree<T> h = Head();
                    h = leftmost;

                    if (h != null)
                    {
                        h.Parent = null;
                    }
                }
                else
                {
                    int result = current.Parent.CompareTo(current.Value);

                    if (result > 0)
                    {
                        // Если значение родительского узла больше значения удаляемого,                 
                        // сделать крайнего левого потомка левым потомком родителя удаляемого узла.                 

                        current.Parent.Left = leftmost;
                    }
                    else if (result < 0)
                    {
                        // Если значение родительского узла, меньше чем значение удаляемого,                 
                        // сделать крайнего левого потомка, правым потомком родителя удаляемого узла.                 

                        current.Parent.Right = leftmost;
                    }
                }
            }

            if (treeToBalance != null)
            {
                treeToBalance.Balance();
            }

            else
            {
                AVLTree<T> h = Head();
                if (h != null)
                {
                    h.Balance();
                }
            }

            return true;

        }

        /// <summary> 
        /// Находит и возвращает первый узел который содержит искомое значение.
        /// Если значение не найдено, возвращает null. 
        /// Так же возвращает родительский узел.
        /// </summary> /// 
        /// <param name="value">Значение поиска</param> 
        /// <param name="parent">Родительский элемент для найденного значения/// </param> 
        /// <returns> Найденный узел (или ноль) /// </returns> 

        public AVLTree<T> Find(T value)
        {

            AVLTree<T> current = this.Head(); // помещаем текущий элемент в корень дерева

            // Пока текщий узел на пустой 
            while (current != null)
            {
                int result = current.CompareTo(value); // сравнение значения текущего элемента с искомым значением

                if (result > 0)
                {
                    // Если значение меньшне текущего - переход влево 
                    current = current.Left;
                }
                else if (result < 0)
                {
                    // Если значение больше текщего - переход вправо             
                    current = current.Right;
                }
                else
                {
                    // Элемент найден      
                    break;
                }
            }
            return current;
        }

        public void Balance()
        {
            if (this.BalanceFactor > 1)
            {
                if (Right != null && Right.BalanceFactor < 0)
                {
                    RightLeftRotation();
                }

                else
                {
                    LeftRotation();
                }
            }
            else if (this.BalanceFactor < -1)
            {
                if (Left != null && Left.BalanceFactor > 0)
                {
                    LeftRightRotation();
                }
                else
                {
                    RightRotation();
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

        public void LeftRightRotation()
        {
            Left.LeftRotation();
            RightRotation();
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
