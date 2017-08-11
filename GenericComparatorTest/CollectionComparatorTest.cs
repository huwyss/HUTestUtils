using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HUTestUtil;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace GenericComparatorTest
{
    [TestClass]
    public class CollectionComparatorTest
    {
        // --------------------------------------------------------------
        // null or empty
        // --------------------------------------------------------------

        [TestMethod]
        public void CompareTest_WhenOneListNull_ThenCorrect()
        {
            var comparator = new CollectionComparator<string>();
            List<string> list1 = null;
            var list2 = new List<string>() { "a", "b", "c" };

            var result = comparator.IsEqual(list1, list2);

            Assert.AreEqual(false, result.Result);
            Assert.AreEqual("One collection is null, the other has elements.", result.Message);
        }

        [TestMethod]
        public void CompareTest_WhenBothNull_ThenCorrect()
        {
            var comparator = new CollectionComparator<string>();
            List<string> list1 = null;
            List<string> list2 = null;

            var result = comparator.IsEqual(list1, list2);

            Assert.AreEqual(true, result.Result);
            Assert.AreEqual("Both collections are null.", result.Message);
        }

        // --------------------------------------------------------------
        // list of string
        // --------------------------------------------------------------

        [TestMethod]
        public void CompareTest_WhenStringEqual_ThenTrue()
        {
            var comparator = new CollectionComparator<string>();

            var list1 = new List<string>() { "a", "b", "c" };
            var list2 = new List<string>() { "a", "b", "c" };

            var result = comparator.IsEqual(list1, list2);
            Assert.AreEqual(true, result.Result);
            Assert.AreEqual("", result.Message);
        }

        [TestMethod]
        public void CompareTest_WhenStringDiffer_ThenFalse()
        {
            var comparator = new CollectionComparator<string>();

            var list1 = new List<string>() { "a", "b", "c" };
            var list2 = new List<string>() { "a", "b" };

            var result = comparator.IsEqual(list1, list2);

            Assert.AreEqual(false, result.Result);
            Assert.AreEqual(true, result.Message.ToLower().Contains("sizes differ"));
        }

        // --------------------------------------------------------------
        // list of int
        // --------------------------------------------------------------

        [TestMethod]
        public void CompareTest_WhenIntEqual_ThenTrue()
        {
            var comparator = new CollectionComparator<int>();

            var list1 = new List<int>() { 1, 2, 3 };
            var list2 = new List<int>() { 1, 2, 3 };

            var result = comparator.IsEqual(list1, list2);
            Assert.AreEqual(true, result.Result);
            Assert.AreEqual("", result.Message);
        }

        [TestMethod]
        public void CompareTest_WhenIntDiffer_ThenFalse()
        {
            var comparator = new CollectionComparator<int>();

            var list1 = new List<int>() { 1, 2, 3 };
            var list2 = new List<int>() { 1, 2, 4 };

            var result = comparator.IsEqual(list1, list2);

            Assert.AreEqual(false, result.Result);
            Assert.AreEqual(true, result.Message.ToLower().Contains("values") && result.Message.ToLower().Contains("differ"));
        }

        // --------------------------------------------------------------
        // array of int
        // --------------------------------------------------------------

        [TestMethod]
        public void CompareTest_WhenIntArrayDiffer_ThenFalse()
        {
            var comparator = new CollectionComparator<int>();

            var array1 = new int[] { 1, 2, 3 };
            var array2 = new int[] { 1, 2, 4 };

            var result = comparator.IsEqual(array1, array2);

            Assert.AreEqual(false, result.Result);
            Assert.AreEqual(true, result.Message.ToLower().Contains("values") && result.Message.ToLower().Contains("differ"));
        }

        [TestMethod]
        public void CompareTest_WhenIntArrayEqual_ThenTrue()
        {
            var comparator = new CollectionComparator<int>();

            var array1 = new int[] { 1, 2, 3 };
            var array2 = new int[] { 1, 2, 3 };

            var result = comparator.IsEqual(array1, array2);

            Assert.AreEqual(true, result.Result);
            Assert.AreEqual("", result.Message);
        }

        // --------------------------------------------------------------
        // array of double
        // --------------------------------------------------------------

        [TestMethod]
        public void CompareTest_WhenDoubleArrayDiffer_ThenFalse()
        {
            var comparator = new CollectionComparator<double>();

            var array1 = new double[] { 1, 2, 3 };
            var array2 = new double[] { 1, 2, 4 };

            var result = comparator.IsEqual(array1, array2);

            Assert.AreEqual(false, result.Result);
            Assert.AreEqual(true, result.Message.ToLower().Contains("values") && result.Message.ToLower().Contains("differ"));
        }

        [TestMethod]
        public void CompareTest_WhenDoubleArrayEqual_ThenTrue()
        {
            var comparator = new CollectionComparator<double>();

            var array1 = new double[] { 1, 2, 3 };
            var array2 = new double[] { 1, 2, 3 };

            var result = comparator.IsEqual(array1, array2);

            Assert.AreEqual(true, result.Result);
            Assert.AreEqual("", result.Message);
        }

        [TestMethod]
        public void CompareTest_WhenDoubleArrayAlmostEqual_ThenTrue()
        {
            var comparator = new CollectionComparator<double>();

            var array1 = new double[] { 1.000001, 2, 3 };
            var array2 = new double[] { 1, 2, 3 };

            var result = comparator.IsEqual(array1, array2);

            Assert.AreEqual(true, result.Result);
            Assert.AreEqual("", result.Message);
        }

        [TestMethod]
        public void CompareTest_WhenDoubleArrayAlmostEqualSetPrecision_ThenTrue()
        {
            var comparator = new CollectionComparator<double>(precision: 0.0000001);

            var array1 = new double[] { 1.000001, 2, 3 };
            var array2 = new double[] { 1, 2, 3 };

            var result = comparator.IsEqual(array1, array2);

            Assert.AreEqual(false, result.Result);
            Assert.AreEqual(true, result.Message.Contains("differ"));
        }

        [TestMethod]
        public void CompareTest_WhenDoubleArrayAlmostEqualSetPrecision_ThenFalse()
        {
            var comparator = new CollectionComparator<double>(precision: 0.000001);

            var array1 = new double[] { 1.000001, 2, 3 };
            var array2 = new double[] { 1, 2, 3 };

            var result = comparator.IsEqual(array1, array2);

            Assert.AreEqual(true, result.Result);
            Assert.AreEqual("", result.Message);
        }

        // --------------------------------------------------------------
        // array of float
        // --------------------------------------------------------------

        [TestMethod]
        public void CompareTest_WhenFloatArrayDiffer_ThenFalse()
        {
            var comparator = new CollectionComparator<double>();

            var array1 = new double[] { 1, 2, 3 };
            var array2 = new double[] { 1, 2, 3.0001 };

            var result = comparator.IsEqual(array1, array2);

            Assert.AreEqual(false, result.Result);
            Assert.AreEqual(true, result.Message.ToLower().Contains("values") && result.Message.ToLower().Contains("differ"));
        }

        [TestMethod]
        public void CompareTest_WhenFloatArrayEqual_ThenTrue()
        {
            var comparator = new CollectionComparator<double>();

            var array1 = new double[] { 1, 2, 3 };
            var array2 = new double[] { 1, 2, 3 };

            var result = comparator.IsEqual(array1, array2);

            Assert.AreEqual(true, result.Result);
            Assert.AreEqual("", result.Message);
        }

        // --------------------------------------------------------------
        // list of objects
        // --------------------------------------------------------------

        public class Person
        {
            public string Name { get; set; }
            public int Id { get; set; }

            public override bool Equals(object obj)
            {
                Person otherPerson = obj as Person;
                if (otherPerson == null)
                {
                    return false;
                }

                bool equal = true;
                equal &= Name.Equals(otherPerson.Name);
                equal &= Id.Equals(otherPerson.Id);
                return equal;
            }

            public override string ToString()
            {
                string text = "Name=" + Name + ", Id=" + Id.ToString();
                return text;
            }
        }

        [TestMethod]
        public void WhenListOfPersonsEqual_ThenTrue()
        {
            var comparator = new CollectionComparator<Person>();

            List<Person> persons = new List<Person>();
            persons.Add(new Person() { Name = "Hans", Id = 1 });
            persons.Add(new Person() { Name = "Peter", Id = 2 });

            List<Person> persons2 = new List<Person>();
            persons2.Add(new Person() { Name = "Hans", Id = 1 });
            persons2.Add(new Person() { Name = "Peter", Id = 2 });

            var result = comparator.IsEqual(persons, persons2);

            Assert.AreEqual(true, result.Result);
            Assert.AreEqual("", result.Message);
        }

        [TestMethod]
        public void WhenListOfPersonsDiffer_ThenFalse()
        {
            var comparator = new CollectionComparator<Person>();

            List<Person> persons = new List<Person>();
            persons.Add(new Person() { Name = "Hans", Id = 1 });
            persons.Add(new Person() { Name = "Peter", Id = 2 });

            List<Person> persons2 = new List<Person>();
            persons2.Add(new Person() { Name = "Hans", Id = 3 });
            persons2.Add(new Person() { Name = "Peter", Id = 2 });

            var result = comparator.IsEqual(persons, persons2);

            Assert.AreEqual(false, result.Result);
            Assert.IsTrue(result.Message.Contains("Values at index 0 differ"));

        }
    }
}
