using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingAVLTree
{
    [TestFixture]
    class TestAVLTree
    {
        [Test]
        public void CreateAVLTreeGivenItsValueAndParent()
        {
            AVLTree<int> tree = new AVLTree<int>(10, null); 
        }

        [Test]
        public void InsertingNodeIntoAVLTree()
        {
            AVLTree<int> tree = new AVLTree<int>(12, null);
            tree.Add(15);
            tree.Add(25);
            if(!(tree.Head().Value == 15 && tree.Head().Right.Value == 25 && tree.Head().Left.Value == 12))
            {
                Assert.Fail();
            }
        }

        [Test]
        public void FindingRootTree()
        {
            AVLTree<int> tree = new AVLTree<int>(10, null);
            tree.Left = new AVLTree<int>(8, tree);
            tree.Left.Right = new AVLTree<int>(9, tree.Left);
            Assert.AreEqual(tree, tree.Head());
        }

        [Test]
        public void ToBalanceTheTreeFromTheCurrentNode()
        {
            //    10              8    
            //    /              / \    
            //   8     --->     7  10   
            //  / \                /     
            // 7   9              9       
            AVLTree<int> treeR = new AVLTree<int>(10, null);
            treeR.Left = new AVLTree<int>(8, treeR);
            treeR.Left.Left = new AVLTree<int>(7, treeR.Left);
            treeR.Left.Right = new AVLTree<int>(9, treeR.Left);
            treeR.Balance();
            bool checkHighR = (treeR.Head().Value == 8) && (treeR.Head().Left.Value == 7) && (treeR.Head().Right.Value == 10) && (treeR.Head().Right.Left.Value == 9);

            //    10             12    
            //     \             / \    
            //     12  --->    10  13   
            //     / \          \        
            //    11 13         11        
            AVLTree<int> treeL = new AVLTree<int>(10, null);
            treeL.Right = new AVLTree<int>(12, treeL);
            treeL.Right.Left = new AVLTree<int>(11, treeL.Right);
            treeL.Right.Right = new AVLTree<int>(13, treeL.Right);
            treeL.Balance();
            bool checkHighL = (treeL.Head().Value == 12) && (treeL.Head().Right.Value == 13) && (treeL.Head().Left.Value == 10) && (treeL.Head().Left.Right.Value == 11);

            //    10                   
            //     \              11    
            //     12  --->       / \    
            //     /            10  12   
            //    11                      
            AVLTree<int> treeRL = new AVLTree<int>(10, null);
            treeRL.Right = new AVLTree<int>(12, treeRL);
            treeRL.Right.Left = new AVLTree<int>(11, treeRL.Right);
            treeRL.Balance();
            bool checkHighRL = (treeRL.Head().Value == 11) && (treeRL.Head().Right.Value == 12) && (treeRL.Head().Left.Value == 10);

            //    10                   
            //    /               9     
            //   8     --->      / \    
            //    \             8   10   
            //     9                      
            AVLTree<int> treeLR = new AVLTree<int>(10, null);
            treeLR.Left = new AVLTree<int>(8, treeLR);
            treeLR.Left.Right = new AVLTree<int>(9, treeLR.Left);
            treeLR.Balance();
            bool checkHighLR = (treeLR.Head().Value == 9) && (treeLR.Head().Right.Value == 10) && (treeLR.Head().Left.Value == 8);

            if (!(checkHighR && checkHighL && checkHighRL && checkHighLR))
            {
                Assert.Fail();
            }
        }

        [Test]
        public void FindingTheHeightOfATree()
        {
            AVLTree<int> tree = new AVLTree<int>(10, null);
            tree.Left = new AVLTree<int>(8, tree);
            tree.Right = new AVLTree<int>(11, tree);
            tree.Left.Left = new AVLTree<int>(7, tree.Left);
            tree.Left.Right = new AVLTree<int>(9, tree.Left);
            Assert.AreEqual(3, AVLTree<int>.MaxChildHeight(tree));
        }

        [Test]
        public void ComparingTheValuesOfTwoNodes()
        {
            AVLTree<int> nodeGreater = new AVLTree<int>(12, null);
            AVLTree<int> nodeLess = new AVLTree<int>(10, null);
            AVLTree<int> nodeEq = new AVLTree<int>(10, null);
            if (!(nodeGreater.CompareTo(nodeLess.Value) == 1 && nodeLess.CompareTo(nodeGreater.Value) == -1 && nodeLess.CompareTo(nodeEq.Value) == 0))
            {
                Assert.Fail();
            }
        }

        [Test]
        public void ReplacementOfTheRoot()
        {
            AVLTree<int> tree = new AVLTree<int>(12, null);
            tree.Right = new AVLTree<int>(15, tree);
            
            tree.ReplaceRoot(tree.Right);

            if(!(tree.Value == 12 && tree.Right.Value == 15 && tree.Right.Parent == null && tree.Parent == tree.Right && tree.Head() == tree.Right))
            {
                Assert.Fail();
            }
        }

        [Test]
        public void LeftRotationTree()
        {
            //    10             12    
            //     \             / \    
            //     12  --->    10  13   
            //     / \          \        
            //    11 13         11        
            AVLTree<int> treeL = new AVLTree<int>(10, null);
            treeL.Right = new AVLTree<int>(12, treeL);
            treeL.Right.Left = new AVLTree<int>(11, treeL.Right);
            treeL.Right.Right = new AVLTree<int>(13, treeL.Right);
            treeL.LeftRotation();
            bool checkHighL = (treeL.Head().Value == 12) && (treeL.Head().Right.Value == 13) && (treeL.Head().Left.Value == 10) && (treeL.Head().Left.Right.Value == 11);
            if(!checkHighL)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void RightLeftRotationTree()
        {
            //    10                   
            //     \              11    
            //     12  --->       / \    
            //     /            10  12   
            //    11                      
            AVLTree<int> treeRL = new AVLTree<int>(10, null);
            treeRL.Right = new AVLTree<int>(12, treeRL);
            treeRL.Right.Left = new AVLTree<int>(11, treeRL.Right);
            treeRL.RightLeftRotation();
            bool checkHighRL = (treeRL.Head().Value == 11) && (treeRL.Head().Right.Value == 12) && (treeRL.Head().Left.Value == 10);
            if (!checkHighRL)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void RightRotationTree()
        {
            //    10              8    
            //    /              / \    
            //   8     --->     7  10   
            //  / \                /     
            // 7   9              9       
            AVLTree<int> treeR = new AVLTree<int>(10, null);
            treeR.Left = new AVLTree<int>(8, treeR);
            treeR.Left.Left = new AVLTree<int>(7, treeR.Left);
            treeR.Left.Right = new AVLTree<int>(9, treeR.Left);
            treeR.RightRotation();
            bool checkHighR = (treeR.Head().Value == 8) && (treeR.Head().Left.Value == 7) && (treeR.Head().Right.Value == 10) && (treeR.Head().Right.Left.Value == 9);
            if (!checkHighR)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void LeftRightRotationTree()
        {
            //    10                   
            //    /               9     
            //   8     --->      / \    
            //    \             8   10   
            //     9                      
            AVLTree<int> treeLR = new AVLTree<int>(10, null);
            treeLR.Left = new AVLTree<int>(8, treeLR);
            treeLR.Left.Right = new AVLTree<int>(9, treeLR.Left);
            treeLR.LeftRightRotation();
            bool checkHighLR = (treeLR.Head().Value == 9) && (treeLR.Head().Right.Value == 10) && (treeLR.Head().Left.Value == 8);
            if(!checkHighLR)
            {
                Assert.Fail();
            }
        }
    }
}
