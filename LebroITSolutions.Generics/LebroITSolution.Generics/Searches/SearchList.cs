using System;
using System.Collections.Generic;
using System.Linq;

namespace LebroITSolution.Generics.Searches
{
    /// <summary>
    /// A Generic class to search objects on propteries values
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SearchList<T> where T : class
    {
        #region Private members

        private List<T> _searchCollection;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the search collection.
        /// </summary>
        /// <value>
        /// The search collection.
        /// </value>
        public List<T> SearchCollection
        {
            get { return _searchCollection; }
            set { _searchCollection = value; }
        }

        #endregion
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchList&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="searchCollection">The search collection.</param>
        public SearchList(List<T> searchCollection)
        {
            _searchCollection = searchCollection;
        }

        public SearchList(){}

        #endregion

        #region Generic Search Methodes

        #region Generic Search by 1 complete searchterm


        /// <summary>
        /// Uses this method to Search the collection with 1 searchterm and 1 propertyname.
        /// </summary>
        /// <param name="searchTerm">The search term.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public List<T> SearchCollection1SearchTerm(string searchTerm, string propertyName)
        {
            if (Equals(propertyName, null))
            {
                throw new ArgumentNullException("propertyName", "Property naam mag niet null zijn");
            }
            if (Equals(searchTerm, null))
            {
                throw new ArgumentNullException("searchTerm", "Search term mag niet null zijn");
            }

            if(Equals(_searchCollection, null)) return null;
            try
            {
                //Lambda expression with case insensitive search
                var result =
                    _searchCollection.FindAll(
                        s => ((string)s.GetType().GetProperty(propertyName).GetValue(s, null)).ToLower() == searchTerm.ToLower());
                return result;
            }
            catch (InvalidCastException icEx)
            {
                throw new InvalidCastException("De opgegeven property naam is niet van het type string!", icEx);
            }
        }

        /// <summary>
        /// Use this method to Search the collection with 1 searchterm and several propertynames.
        /// </summary>
        /// <param name="searchTerm">The search term.</param>
        /// <param name="propertyNames">The property names.</param>
        /// <returns></returns>
        public List<T> SearchCollection1SearchTermAllProperties(string searchTerm, string[] propertyNames)
        {
            if (Equals(propertyNames, null))
            {
                throw new ArgumentNullException("propertyNames", "Property naam mag niet null zijn");
            }
            if (Equals(searchTerm, null))
            {
                throw new ArgumentNullException("searchTerm", "Search term mag niet null zijn");
            }
            
            if (Equals(_searchCollection, null)) return null;
            try
            {
                var result = new List<T>();
                foreach (var propertyName in propertyNames.Where(propertyName => !string.IsNullOrEmpty(propertyName)))
                {
                    //Lambda expression with case insensitive search
                    result.AddRange( _searchCollection.FindAll(
                        s => ((string)s.GetType().GetProperty(propertyName).GetValue(s, null)).ToLower() == searchTerm.ToLower()));
                }
                //Remove duplicates
                return result.Distinct().ToList();
            }
            catch (InvalidCastException icEx)
            {
                throw new InvalidCastException("De opgegeven property naam is niet van het type string!", icEx);
            }
        }

        #endregion

        #region Generic Search by 1 partial searchterm

        /// <summary>
        /// Use this method to Search the collection with 1 partial searchterm and several propertynames.
        /// </summary>
        /// <param name="searchTerm">The search term.</param>
        /// <param name="propertyNames">The property names.</param>
        /// <returns></returns>
        public List<T> SearchCollection1PartialSearchTermAllProperties(string searchTerm, string[] propertyNames)
        {
            if (Equals(propertyNames, null))
            {
                throw new ArgumentNullException("propertyNames", "Property naam mag niet null zijn");
            }
            if (Equals(searchTerm, null))
            {
                throw new ArgumentNullException("searchTerm", "Search term mag niet null zijn");
            }

            if (Equals(_searchCollection, null)) return null;
            try
            {
                var result = new List<T>();
                foreach (var propertyName in propertyNames.Where(propertyName => !string.IsNullOrEmpty(propertyName)))
                {
                    result.AddRange(_searchCollection.FindAll(
                        s => ((string)s.GetType().GetProperty(propertyName).GetValue(s, null)).ToLower().Contains(searchTerm.ToLower())));
                }
                
                return result.Distinct().ToList();
            }
            catch (InvalidCastException icEx)
            {
                throw new InvalidCastException("De opgegeven property naam is niet van het type string!", icEx);
            }
        }


        /// <summary>
        /// Use this method to Search the collection with 1 partial searchterm and several propertynames.
        /// </summary>
        /// <param name="searchTerm">The search term.</param>
        /// <param name="propertyNames">The property names.</param>
        /// <returns></returns>
        public List<T> SearchCollection1PartialStartSearchTermAllProperties(string searchTerm, string[] propertyNames)
        {
            if (Equals(propertyNames, null))
            {
                throw new ArgumentNullException("propertyNames", "Property naam mag niet null zijn");
            }
            if (Equals(searchTerm, null))
            {
                throw new ArgumentNullException("searchTerm", "Search term mag niet null zijn");
            }

            if (Equals(_searchCollection, null)) return null;
            try
            {
                var result = new List<T>();
                foreach (var propertyName in propertyNames.Where(propertyName => !string.IsNullOrEmpty(propertyName)))
                {
                    result.AddRange(_searchCollection.FindAll(
                        s => ((string)s.GetType().GetProperty(propertyName).GetValue(s, null)).ToLower().StartsWith(searchTerm.ToLower())));
                }

                return result.Distinct().ToList();
            }
            catch (InvalidCastException icEx)
            {
                throw new InvalidCastException("De opgegeven property naam is niet van het type string!", icEx);
            }
        }
        #endregion

        #endregion

        #region Helper Methodes

        

        #endregion
    }
}
