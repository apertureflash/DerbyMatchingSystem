using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using CockDerbyMatching_BL;
using CockDerbyMatching_DAL;

namespace CockDerbyMatchingSystem.Classes
{
    /// <summary>
    /// A source of Northwind Customer objects
    /// </summary>
    public class DerbyEntriesDataProvider
    {
        //private CustomersTableAdapter adapter;
        //private 

        public DerbyEntriesDataProvider()
        {
            //NorthwindDataSet dataset = NorthWindDataProvider.NorthwindDataSet;

            //adapter = new CustomersTableAdapter();
            //adapter.Fill(NorthWindDataProvider.NorthwindDataSet.Customers);

            //dataset.Customers.CustomersRowChanged += new NorthwindDataSet.CustomersRowChangeEventHandler(CustomersRowModified);
            //dataset.Customers.CustomersRowDeleted += new NorthwindDataSet.CustomersRowChangeEventHandler(CustomersRowModified);
        }

        /*
        void CustomersRowModified(object sender, NorthwindDataSet.CustomersRowChangeEvent e)
        {
            adapter.Update(NorthWindDataProvider.NorthwindDataSet.Customers);
        }
        */

        public ObservableCollection<Entry> GetDerbyEntries(Int32 derbyID)
        {
            Entries dEntries = new Entries();

            return dEntries.GetDerbyEntries(derbyID);
        }

        public ObservableCollection<Entry> GetNoFightEntries(Int32 entryID)   
        {
            Entries dEntries = new Entries();

            return dEntries.GetNoFightEntries(entryID);
        }

    }

    public class EntryRoostersDataProvider
    {
        //public OrdersTableAdapter adapter;

        public EntryRoostersDataProvider()
        {
            //NorthwindDataSet dataset = NorthWindDataProvider.NorthwindDataSet;

            //adapter = new OrdersTableAdapter();
            //adapter.Fill(dataset.Orders);

            AddRowChangeHandlers();
        }

        private void AddRowChangeHandlers()
        {
            //NorthwindDataSet dataset = NorthWindDataProvider.NorthwindDataSet;
            //dataset.Orders.OrdersRowChanged += new NorthwindDataSet.OrdersRowChangeEventHandler(OrderRowModified);
            //dataset.Orders.OrdersRowDeleted += new NorthwindDataSet.OrdersRowChangeEventHandler(OrderRowModified);
        }

        /*
        void OrderRowModified(object sender, NorthwindDataSet.OrdersRowChangeEvent e)
        {
            //adapter.Update(NorthWindDataProvider.NorthwindDataSet.Orders);
        }
        */

        /*
        public Roosters GetEntryRoosters()
        {
            //return NorthWindDataProvider.NorthwindDataSet.Orders.DefaultView;
        }
        */

        /// <summary>
        /// Obtains all the orders for the given customer.
        /// </summary>
        public ObservableCollection<Rooster> GetRoostersByDerbyEntry(Int32 entryID)
        {
            //ObservableCollection<Rooster> entryRoosters = null;

            //if (entryID != null || entryID >= 0)
            //{
            //    Roosters eRoosters = new Roosters();

            //    entryRoosters = eRoosters.GetEntryRoosters((Int32)entryID);
            //}
            Roosters eRoosters = new Roosters();

            return eRoosters.GetEntryRoosters(entryID);
        }

        public ObservableCollection<UnMatched> GetUnMatchedRoosterEntries()
        {
            ObservableCollection<UnMatched> unMatched = null;

            Roosters eRoosters = new Roosters();

            unMatched = eRoosters.GetUnMatchedRoosterEntries();
            return unMatched;
        }

    }
}
