using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using CriteriaHelper.Support;

namespace CriteriaHelper.Tests
{
    [TestFixture]
    internal class ReflectionHelperTest
    {
        [Test]
        public void TestPropertyAsString()
        {
            ReflectionHelper<Test1> cs = new ReflectionHelper<Test1>();

            Assert.IsTrue(cs.PropertyAsString(t => t.Test2.Test1) == "t.Test2.Test1");

            Assert.IsTrue(cs.PropertyAsString(t => t.Test2) == "t.Test2");

            Assert.IsTrue(cs.PropertyAsString(t => t.GetType()) == "t.GetType()");
        }
        [Test]
        public void TestPropertyAsStringWithAlias()
        {
            ReflectionHelper<Test1> cs = new ReflectionHelper<Test1>();

            Assert.IsTrue(cs.PropertyAsString(t => t.GetType(), "this_") == "this_.GetType()", "Teste de Property as String passando Alias");

            Assert.IsTrue(cs.PropertyAsString(t => t.Test2.Test1.id, "this_") == "this_.Test2.Test1.id", cs.PropertyAsString(t => t.Test2.Test1.id, "this_"));
        }
    }

    class Test1
    {
        public int  id {get; set;}
        public Test2 Test2 { get; set; }
    }

    class Test2
    {
        public Test1 Test1 { get; set; }
    }
}
