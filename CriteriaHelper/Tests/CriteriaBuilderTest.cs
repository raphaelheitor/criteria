using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NHibernate;

namespace CriteriaHelper.Tests
{
    [TestFixture]
    internal class CriteriaBuilderTest
    {
        [Test]
        public void ShouldReturnCorrectDetachedCriteriaGivenWhereClauseEqualPassingParameter()
        {
            CriteriaBuilder<MockClass> c = new CriteriaBuilder<MockClass>().Eq(m => m.Id, 1);

            string test = c.GetDetachedCriteria().ToString();
            Assert.True(test == "DetachableCriteria(this_.Id = 1)", "Current value: " + test);
        }
        [Test]
        public void ShouldReturnCorrectDetachedCriteriaGivenWhereClauseEqualPassingParameterNull()
        {
            CriteriaBuilder<MockClass> c = new CriteriaBuilder<MockClass>().EqAllowingNull(m => m.Id, null);

            string test = c.GetDetachedCriteria().ToString();
            Assert.True(test == "DetachableCriteria()", "Current value: " + test);
        }
        [Test]
        public void ShouldReturnCorrectDetachedCriteriaGivenWhereClauseLikePassingParameter()
        {
            CriteriaBuilder<MockClass> c = new CriteriaBuilder<MockClass>().Like(m => m.StringField, "test");

            string test = c.GetDetachedCriteria().ToString();
            Assert.True(test == "DetachableCriteria(this_.StringField like %test%)", "Current value: " + test);
        }
        [Test]
        public void ShouldReturnCorrectDetachedCriteriaGivenWhereClauseLikePassingParameterNull()
        {
            CriteriaBuilder<MockClass> c = new CriteriaBuilder<MockClass>().LikeAllowingNull(m => m.StringField, null);

            string test = c.GetDetachedCriteria().ToString();
            Assert.True(test == "DetachableCriteria()", "Current value: " + test);
        }
        [Test]
        public void ShouldReturnCorrectDetachedCriteriaGivenWhereClauseLikeAndEqualPassingParameter()
        {
            CriteriaBuilder<MockClass> c = new CriteriaBuilder<MockClass>().Like(m => m.StringField, "test")
                                                                   .Eq(m => m.Id, 1);

            string test = c.GetDetachedCriteria().ToString();
            Assert.True(test == "DetachableCriteria(this_.StringField like %test% and this_.Id = 1)", "Current value: " + test);
        }
        [Test]
        public void ShouldReturnCorrectDetachedCriteriaGivenWhereClauseBetween()
        {
            CriteriaBuilder<MockClass> c = new CriteriaBuilder<MockClass>().Between(m => m.DecimalField, 1.3, 100.5);

            string test = c.GetDetachedCriteria().ToString();
            Assert.True(test == "DetachableCriteria(this_.DecimalField between 1,3 and 100,5)", "Current value: " + test);
        }
        [Test]
        public void ShouldReturnCorrectDetachedCriteriaGivenWhereClauseGreaterThanPassingParameter()
        {
            CriteriaBuilder<MockClass> c = new CriteriaBuilder<MockClass>().GreaterThan(m => m.Id, 2);

            string test = c.GetDetachedCriteria().ToString();
            Assert.True(test == "DetachableCriteria(this_.Id > 2)", "Current value: " + test);
        }
        [Test]
        public void ShouldReturnCorrectDetachedCriteriaGivenWhereClauseGreaterThanOrEqualPassingParameter()
        {
            CriteriaBuilder<MockClass> c = new CriteriaBuilder<MockClass>().GreaterThanOrEqual(m => m.Id, 2);

            string test = c.GetDetachedCriteria().ToString();
            Assert.True(test == "DetachableCriteria(this_.Id >= 2)", "Current value: " + test);
        }
        [Test]
        public void ShouldReturnCorrectDetachedCriteriaGivenWhereClauseLessThanPassingParameter()
        {
            CriteriaBuilder<MockClass> c = new CriteriaBuilder<MockClass>().LessThan(m => m.Id, 2);

            string test = c.GetDetachedCriteria().ToString();
            Assert.True(test == "DetachableCriteria(this_.Id < 2)", "Current value: " + test);
        }
        [Test]
        public void ShouldReturnCorrectDetachedCriteriaGivenWhereClauseLessThanOrEqualPassingParameter()
        {
            CriteriaBuilder<MockClass> c = new CriteriaBuilder<MockClass>().LessThanOrEqual(m => m.Id, 2);

            string test = c.GetDetachedCriteria().ToString();
            Assert.True(test == "DetachableCriteria(this_.Id <= 2)", "Current value: " + test);
        }
        
        [Test]
        public void ShouldReturnCorrectDetachedCriteriaGivenWhereClauseGreaterThanOrEqualPassingParameterNull()
        {
            CriteriaBuilder<MockClass> c = new CriteriaBuilder<MockClass>().GreaterThanOrEqualAllowingNull(m => m.Id, null);

            string test = c.GetDetachedCriteria().ToString();
            Assert.True(test == "DetachableCriteria()", "Current value: " + test);
        }
        [Test]
        public void ShouldReturnCorrectDetachedCriteriaGivenWhereClauseLessThanOrEqualPassingParameterNull()
        {
            CriteriaBuilder<MockClass> c = new CriteriaBuilder<MockClass>().LessThanOrEqualAllowingNull(m => m.Id, null);

            string test = c.GetDetachedCriteria().ToString();
            Assert.True(test == "DetachableCriteria()", "Current value: " + test);
        }
        [Test]
        public void ShouldReturnCorrectDetachedCriteriaGivenWhereClouseEqPassingParameterDatePart()
        {
            CriteriaBuilder<MockClass> c = new CriteriaBuilder<MockClass>().EqDatePartAllowingNull(m => m.DateTimeField, 2013, DatePart.Year);
            string test = c.GetDetachedCriteria().ToString();
            Assert.True(test == "DetachableCriteria(NHibernate.Criterion.SqlFunctionProjection = 2013)", "Current value: " + test);
        }
        [Test]
        public void ShouldReturnCorrectDetachedCriteriaGivenWhereClouseOrWithEquals()
        {
            CriteriaBuilder<MockClass> c = new CriteriaBuilder<MockClass>().OrEq(m => m.Id, 1, t => t.Id, 2);
            string test = c.GetDetachedCriteria().ToString();
            Assert.True(test == "DetachableCriteria((this_.Id = 1 or this_.Id = 2))", "Current value: " + test);
        }
        [Test]
        public void ShouldReturnCorrectDetachedCriteriaGivenWhereClouseOrWithEqualsAndClouseLike()
        {
            CriteriaBuilder<MockClass> c = new CriteriaBuilder<MockClass>().OrEq(m => m.Id, 1, t => t.Id, 2).Like(m => m.StringField, "test");
            string test = c.GetDetachedCriteria().ToString();
            Assert.True(test == "DetachableCriteria((this_.Id = 1 or this_.Id = 2) and this_.StringField like %test%)", "Current value: " + test);
        }
        [Test]
        public void ShouldReturnCorrectDetachedCriteriaGivenWhereClouseOrWithLike()
        {
            CriteriaBuilder<MockClass> c = new CriteriaBuilder<MockClass>().OrLike(m => m.StringField, "tests", t => t.StringField, "test");
            string test = c.GetDetachedCriteria().ToString();
            Assert.True(test == "DetachableCriteria((this_.StringField like %tests% or this_.StringField like %test%))", "Current value: " + test);
        }
        [Test]
        public void ShouldReturnCorrectDetachedCriteriaGivenWhereClouseOrWithEqualsAndLike()
        {
            CriteriaBuilder<MockClass> c = new CriteriaBuilder<MockClass>().OrEqLike(m => m.Id, 1, t => t.StringField, "test");
            string test = c.GetDetachedCriteria().ToString();
            Assert.True(test == "DetachableCriteria((this_.Id = 1 or this_.StringField like %test%))", "Current value: " + test);
        }
        [Test]
        public void ShouldReturnCorrectDetachedCriteriaGivenJoinRelationshipWithEqualAllowingNull()
        {
            CriteriaBuilder<MockClass> c = new CriteriaBuilder<MockClass>().EqJoinAllowingNull("mock", m => m.mock.StringField, "test");
            string test = c.GetDetachedCriteria().ToString();
            Assert.True(test == "DetachableCriteria(mock.StringField = test)", "Current value: " + test);
        }
    }
    //Mock
    public class MockClass
    {
        public int Id { get; set; }
        public string StringField { get; set; }
        public DateTime DateTimeField { get; set; }
        public decimal DecimalField { get; set; }
        public MockClass2 mock { get; set; }
    }
    public class MockClass2
    {
        public int Id { get; set; }
        public string StringField { get; set; }
        public DateTime DateTimeField { get; set; }
        public decimal DecimalField { get; set; }
    }
}
