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
            AVLTree<int> tree = new AVLTree<int>(10, null);
            //tree.Add(20);
            //Assert.AreEqual(20, tree.Right.Value);
            Assert.Fail();
        }

        [Test]
        public void FindingRootTree()
        {
            AVLTree<int> tree = new AVLTree<int>(10, null);
            Assert.AreEqual(tree, tree.Head());
        }




    }
}
