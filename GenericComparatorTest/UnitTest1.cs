﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HUTestUtil;
using System.Collections.Generic;

namespace GenericComparatorTest
{
    [TestClass]
    public class UnitTest1
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

            var result = comparator.Compare(list1, list2);

            Assert.AreEqual(false, result.Result);
            Assert.AreEqual("One collection is null, the other has elements.", result.Message);
        }

        [TestMethod]
        public void CompareTest_WhenBothNull_ThenCorrect()
        {
            var comparator = new CollectionComparator<string>();
            List<string> list1 = null;
            List<string> list2 = null;

            var result = comparator.Compare(list1, list2);

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

            var result = comparator.Compare(list1, list2);
            Assert.AreEqual(true, result.Result);
            Assert.AreEqual("", result.Message);
        }

        [TestMethod]
        public void CompareTest_WhenStringDiffer_ThenFalse()
        {
            var comparator = new CollectionComparator<string>();

            var list1 = new List<string>() { "a", "b", "c" };
            var list2 = new List<string>() { "a", "b" };

            var result = comparator.Compare(list1, list2);
            
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

            var result = comparator.Compare(list1, list2);
            Assert.AreEqual(true, result.Result);
            Assert.AreEqual("", result.Message);
        }

        [TestMethod]
        public void CompareTest_WhenIntDiffer_ThenFalse()
        {
            var comparator = new CollectionComparator<int>();

            var list1 = new List<int>() { 1, 2, 3 };
            var list2 = new List<int>() { 1, 2, 4 };

            var result = comparator.Compare(list1, list2);
            
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

            var result = comparator.Compare(array1, array2);

            Assert.AreEqual(false, result.Result);
            Assert.AreEqual(true, result.Message.ToLower().Contains("values") && result.Message.ToLower().Contains("differ"));
        }

        [TestMethod]
        public void CompareTest_WhenIntArrayEqual_ThenTrue()
        {
            var comparator = new CollectionComparator<int>();

            var array1 = new int[] { 1, 2, 3 };
            var array2 = new int[] { 1, 2, 3 };

            var result = comparator.Compare(array1, array2);

            Assert.AreEqual(true, result.Result);
            Assert.AreEqual("", result.Message);
        }


    }
}
