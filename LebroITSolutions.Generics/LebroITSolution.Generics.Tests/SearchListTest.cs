using System.Globalization;
using System.Threading;
using LebroITSolution.Generics.Searches;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using LebroITSolution.Generics.Tests.TestObjects;

namespace LebroITSolution.Generics.Tests
{


    /// <summary>
    ///This is a test class for HelpSearchTest and is intended
    ///to contain all HelpSearchTest Unit Tests
    ///</summary>
    [TestClass]
    public class SearchListTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        private HelpItems _helpItems;
        private HelpItems _helpItemsPartialTests;
        private string[] _propertyNames;
        private const string PropertyName = "HelpDescription";


        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        [TestInitialize]
        public void MyTestInitialize()
        {
            _helpItems = CreateHelpItems();
            _helpItemsPartialTests = CreateHelpItemsForPartial();
            _propertyNames = new[] { "HelpDescription", "HelpInSource", "HelpTopic", "HelpTopicId", "HelpVersie" };
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup]
        public void MyTestCleanup()
        {
            _helpItems = null;
            _helpItemsPartialTests = null;
            _propertyNames = null;
        }

        #endregion

        #region Constructor Tests

        /// <summary>
        /// Testing the param.. constructor
        /// </summary>
        [TestMethod]
        public void HelpSearchConstructorTest()
        {
            var searchCollection = new List<HelpItem>(_helpItems);
            var target = new SearchList<HelpItem>(searchCollection);
            Assert.IsNotNull(target.SearchCollection);
            Assert.IsInstanceOfType(target.SearchCollection, typeof(List<HelpItem>));
            Assert.IsTrue(target.SearchCollection.Count == 5);
        }

        /// <summary>
        /// Testing de standard constructor
        /// </summary>
        [TestMethod]
        public void HelpSearchConstructorTestNoParam()
        {
            var searchCollection = new List<HelpItem>(_helpItems);
            var target = new SearchList<HelpItem> { SearchCollection = searchCollection };
            Assert.IsNotNull(target.SearchCollection);
            Assert.IsInstanceOfType(target.SearchCollection, typeof(List<HelpItem>));
            Assert.IsTrue(target.SearchCollection.Count == 5);
        }

        #endregion

        #region Search the collection by 1 searchterm

        /// <summary>
        ///A happy flow test for the SearchCollection1SearchTerm
        ///</summary>
        [TestMethod]
        public void SearchCollection1SearchTermTest()
        {
            //Happy flow
            var searchCollection = new List<HelpItem>(_helpItems);
            var target = new SearchList<HelpItem>(searchCollection);
            const string searchTerm = "Help omschrijving 4";
            var expected = searchCollection[3];
            var actual = target.SearchCollection1SearchTerm(searchTerm, PropertyName);
            Assert.AreEqual(expected, actual[0]);
        }

        #region Multiple Properties testing

        /// <summary>
        ///A happy flow test for the SearchCollection1SearchTerm
        ///</summary>
        [TestMethod]
        public void SearchCollection1SearchTermMultiplePropertiesTest()
        {
            //Happy flow
            var searchCollection = new List<HelpItem>(_helpItems);
            var target = new SearchList<HelpItem>(searchCollection);
            const string searchTerm = "Help omschrijving 4";
            var expected = searchCollection[3];
            var actual = target.SearchCollection1SearchTermAllProperties(searchTerm, _propertyNames);
            Assert.AreEqual(expected, actual[0]);
        }

        /// <summary>
        ///A happy flow test for the SearchCollection1SearchTerm
        ///</summary>
        [TestMethod]
        public void SearchCollection1SearchTermMultipleProperties1EmptyTest()
        {
            //Happy flow
            var searchCollection = new List<HelpItem>(_helpItems);
            var target = new SearchList<HelpItem>(searchCollection);
            const string searchTerm = "Help omschrijving 4";
            var expected = searchCollection[3];
            _propertyNames[4] = string.Empty;
            var actual = target.SearchCollection1SearchTermAllProperties(searchTerm, _propertyNames);
            Assert.AreEqual(expected, actual[0]);
        }

        /// <summary>
        ///A happy flow test for the SearchCollection1SearchTerm
        ///</summary>
        [TestMethod]
        public void SearchCollection1SearchTermMultiplePropertiesDuplicateResultTest()
        {
            //Happy flow
            var searchCollection = new List<HelpItem>(_helpItems);
            searchCollection.Insert(2, searchCollection[3]);
            var target = new SearchList<HelpItem>(searchCollection);
            const string searchTerm = "Help omschrijving 4";
            var expected = searchCollection.Find(s => s.HelpDescription == searchTerm);
            _propertyNames[4] = string.Empty;
            var actual = target.SearchCollection1SearchTermAllProperties(searchTerm, _propertyNames);
            Assert.IsTrue(actual.Contains(expected));
        }
        #endregion

        #endregion

        #region Search the collection by 1 partial searchterm

        /// <summary>
        ///A happy flow test for the SearchCollection1SearchTerm
        ///</summary>
        [TestMethod]
        public void SearchCollection1PartialSearchTermTest()
        {
            //Happy flow
            var searchCollection = new List<HelpItem>(_helpItemsPartialTests);
            var target = new SearchList<HelpItem>(searchCollection);
            const string searchTerm = "speci";
            const int expected = 2;
            var actual = target.SearchCollection1PartialSearchTermAllProperties(searchTerm, _propertyNames);
            Assert.AreEqual(expected, actual.Count);
            Assert.AreEqual(searchCollection[0], actual[0]);
            Assert.AreEqual(searchCollection[1], actual[1]);
        }

        /// <summary>
        ///A happy flow test for the SearchCollection1SearchTerm
        ///</summary>
        [TestMethod]
        public void SearchCollection1PartialStartSearchTermTest()
        {
            //Happy flow
            var searchCollection = new List<HelpItem>(_helpItemsPartialTests);
            var target = new SearchList<HelpItem>(searchCollection);
            const string searchTerm = "He";
            const int expected = 5;
            var actual = target.SearchCollection1PartialStartSearchTermAllProperties(searchTerm, _propertyNames);
            Assert.AreEqual(expected, actual.Count);
            Assert.AreEqual(searchCollection[0], actual[3]);
            Assert.AreEqual(searchCollection[1], actual[4]);
        }

        #endregion

        #region Null arguments testing


        /// <summary>
        ///A test for SearchCollection
        ///</summary>
        [TestMethod]
        public void SearchCollectionIsNullTest()
        {
            var target = new SearchList<HelpItem> { SearchCollection = null };
            const string searchTerm = "Help omschrijving 4";
            var actual = target.SearchCollection1SearchTerm(searchTerm, PropertyName);
            Assert.AreEqual(actual, null);
            actual = target.SearchCollection1SearchTermAllProperties(searchTerm, _propertyNames);
            Assert.AreEqual(actual, null);
            const string partialSearchTerm = "speci";
            actual = target.SearchCollection1PartialSearchTermAllProperties(partialSearchTerm, _propertyNames);
            Assert.AreEqual(actual, null);
            actual = target.SearchCollection1PartialStartSearchTermAllProperties(partialSearchTerm, _propertyNames);
            Assert.AreEqual(actual, null);

        }

        #endregion

        #region Eexception testing

        /// <summary>
        /// Test wat happens if the property is not of the type string.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void InvalidCastOnPropertyNameTest()
        {
            //Using a badobject
            var badCollection = CreateBadObjects();
            var badTarget = new SearchList<BadObject>(badCollection);
            const string searchTerm = "maakt niet uit";
            const string propertyName = "ID";
            var badActual = badTarget.SearchCollection1SearchTerm(searchTerm, propertyName);
            Assert.IsNull(badActual);
        }

        /// <summary>
        /// Test wat happens if the property is not of the type string.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void InvalidCastOnPropertyNamesTest()
        {
            //Using a badobject
            var badCollection = CreateBadObjects();
            var badTarget = new SearchList<BadObject>(badCollection);
            const string searchTerm = "maakt niet uit";
            var propertyNames = new[] { "ID", "Name" };
            var badActual = badTarget.SearchCollection1SearchTermAllProperties(searchTerm, propertyNames);
            Assert.IsNull(badActual);
        }

        /// <summary>
        /// Test wat happens if the property is not of the type string.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void InvalidCastOnPropertyNamesPartialTest()
        {
            //Using a badobject
            var badCollection = CreateBadObjects();
            var badTarget = new SearchList<BadObject>(badCollection);
            const string searchTerm = "maakt niet uit";
            var propertyNames = new[] { "ID", "Name" };
            var badActual = badTarget.SearchCollection1PartialSearchTermAllProperties(searchTerm, propertyNames);
            Assert.IsNull(badActual);
        }

        /// <summary>
        /// Test wat happens if the property is not of the type string.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void InvalidCastOnPropertyNamesPartialStartTest()
        {
            //Using a badobject
            var badCollection = CreateBadObjects();
            var badTarget = new SearchList<BadObject>(badCollection);
            const string searchTerm = "maakt niet uit";
            var propertyNames = new[] { "ID", "Name" };
            var badActual = badTarget.SearchCollection1PartialStartSearchTermAllProperties(searchTerm, propertyNames);
            Assert.IsNull(badActual);
        }

        /// <summary>
        ///Propertyname is null test
        ///</summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PropertyNameIsNullTest()
        {
            var target = new SearchList<HelpItem> { SearchCollection = new List<HelpItem>(_helpItems) };
            const string searchTerm = "HelpDescription";
            var actual = target.SearchCollection1SearchTerm(searchTerm, null);
            Assert.AreEqual(actual, 0);
        }

        /// <summary>
        /// Propertienames are null test.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PropertyNamesAreNullTest()
        {
            var target = new SearchList<HelpItem> { SearchCollection = new List<HelpItem>(_helpItems) };
            const string searchTerm = "HelpDescription";
            var actual = target.SearchCollection1SearchTermAllProperties(searchTerm, null);
            Assert.AreEqual(actual, 0);
        }

        /// <summary>
        /// Partials searchterm with the propertynames are null test.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PartialPropertyNamesAreNullTest()
        {
            var target = new SearchList<HelpItem> { SearchCollection = new List<HelpItem>(_helpItems) };
            const string searchTerm = "HelpDescription";
            var actual = target.SearchCollection1PartialSearchTermAllProperties(searchTerm, null);
            Assert.AreEqual(actual, 0);
        }

        /// <summary>
        /// Partials searchterm with the propertynames are null test.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PartialStartPropertyNamesAreNullTest()
        {
            var target = new SearchList<HelpItem> { SearchCollection = new List<HelpItem>(_helpItems) };
            const string searchTerm = "HelpDescription";
            var actual = target.SearchCollection1PartialStartSearchTermAllProperties(searchTerm, null);
            Assert.AreEqual(actual, 0);
        }

        /// <summary>
        ///A test for a empty searchterm on 1 searchterm & 1 property
        ///</summary>
        [TestMethod]
        public void SearchTermIsNullTest()
        {
            try
            {
                //test 1 searchterm 1 propertyname
                var target = new SearchList<HelpItem> { SearchCollection = new List<HelpItem>(_helpItems) };
                const string propertyName = "HelpDescription";
                target.SearchCollection1SearchTerm(null, propertyName);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.GetType(), typeof(ArgumentNullException));
                Assert.AreEqual(ex.Message,
                               Thread.CurrentThread.CurrentCulture.Name == "nl-NL"
                                   ? "Search term mag niet null zijn\r\nParameternaam: searchTerm"
                                   : "Search term mag niet null zijn\r\nParameter name: searchTerm");
            }
        }

        /// <summary>
        ///A test for a empty searchterm on 1 searchterm multiple properties
        /// </summary>
        [TestMethod]
        public void SearchTermMultiPropsIsNullTest()
        {
            try
            {
                //test 1 searchterm multiple propertynames
                var target = new SearchList<HelpItem> { SearchCollection = new List<HelpItem>(_helpItems) };
                _propertyNames = new[] { "HelpDescription", "HelpInSource", "HelpTopic", "HelpTopicId", "HelpVersie" };
                target.SearchCollection1SearchTermAllProperties(null, _propertyNames);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.GetType(), typeof(ArgumentNullException));
                Assert.AreEqual(ex.Message,
                               Thread.CurrentThread.CurrentCulture.Name == "nl-NL"
                                   ? "Search term mag niet null zijn\r\nParameternaam: searchTerm"
                                   : "Search term mag niet null zijn\r\nParameter name: searchTerm");
            }
        }

        /// <summary>
        ///A test for a empty searchterm on 1 partial searchterm multiple properties
        /// </summary>
        [TestMethod]
        public void PartialSearchTermIsnullTest()
        {
            try
            {
                //test 1 partial searchterm multiple propertynames
                var target = new SearchList<HelpItem> { SearchCollection = new List<HelpItem>(_helpItemsPartialTests) };
                target.SearchCollection1PartialSearchTermAllProperties(null, _propertyNames);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.GetType(), typeof(ArgumentNullException));
                Assert.AreEqual(ex.Message,
                                Thread.CurrentThread.CurrentCulture.Name == "nl-NL"
                                    ? "Search term mag niet null zijn\r\nParameternaam: searchTerm"
                                    : "Search term mag niet null zijn\r\nParameter name: searchTerm");
            }
        }

        /// <summary>
        ///A test for a empty searchterm on 1 partial searchterm multiple properties
        /// </summary>
        [TestMethod]
        public void PartialStartSearchTermIsnullTest()
        {
            try
            {
                //test 1 partial searchterm multiple propertynames
                var target = new SearchList<HelpItem> { SearchCollection = new List<HelpItem>(_helpItemsPartialTests) };
                target.SearchCollection1PartialStartSearchTermAllProperties(null, _propertyNames);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.GetType(), typeof(ArgumentNullException));
                Assert.AreEqual(ex.Message,
                               Thread.CurrentThread.CurrentCulture.Name == "nl-NL"
                                   ? "Search term mag niet null zijn\r\nParameternaam: searchTerm"
                                   : "Search term mag niet null zijn\r\nParameter name: searchTerm");
            }
        }
        #endregion

        #region Create HelpItems

        private HelpItems CreateHelpItems()
        {
            var items = new HelpItems();
            for (var i = 1; i < 6; i++)
            {
                items.Add(new HelpItem
                              {
                                  HelpDescription = string.Format("Help omschrijving {0}", i),
                                  HelpInSource = string.Format("Help in source {0}", i),
                                  HelpTopic = string.Format("Help topic {0}", i),
                                  HelpTopicId = i.ToString(),
                                  HelpVersie = string.Format("versie {0}", i)
                              });
            }
            return items;
        }

        private HelpItems CreateHelpItemsForPartial()
        {
            var items = new HelpItems();
            for (var i = 1; i < 6; i++)
            {
                items.Add(new HelpItem
                {
                    HelpDescription = string.Format("Help omschrijving {0}", i),
                    HelpInSource = string.Format("Help in source {0}", i),
                    HelpTopic = string.Format("Help topic {0}", i),
                    HelpTopicId = i.ToString(),
                    HelpVersie = string.Format("versie {0}", i)
                });
                if (i == 1 || i == 2) items[i - 1].HelpDescription = "Specifiek";

            }
            return items;
        }
        #endregion



        #region Bad objects

        private List<BadObject> CreateBadObjects()
        {
            return new List<BadObject>
                    {
                        new BadObject
                            {
                                ID = 1,
                                Name = "1e slechte object"
                            },
                        new BadObject
                            {
                                ID = 2,
                                Name = "2e slechte object"
                            }
                    };
        }

        #endregion
    }

    internal class BadObject
    {
        public string Name { get; set; }
        public int ID { get; set; }
    }
}
