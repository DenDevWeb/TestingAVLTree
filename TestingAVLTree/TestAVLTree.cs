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
    }
}
