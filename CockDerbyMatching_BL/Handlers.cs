using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data.Linq;
using System.Text;
using CockDerbyMatching_DAL;


namespace CockDerbyMatching_BL
{
    public class Handlers
    {
        public Handler GetHandler(Int32 handlerID)
        {
            Handler var;

            DerbyMatchingDataContext dmDB = new DerbyMatchingDataContext();

            var = (from hand in dmDB.Handlers where hand.HandlerID == handlerID select hand).FirstOrDefault();

            return var;
        }

        public ObservableCollection<Handler> GetDerbyHandlers(Int32 derbyID)
        {
            DerbyMatchingDataContext dmDB = new DerbyMatchingDataContext();

            dmDB.DeferredLoadingEnabled = false;

            ObservableCollection<Handler> myHandlers = null;

            var query = (from handler in dmDB.Handlers
                         join entry in dmDB.Entries on handler.HandlerID equals entry.HandlerID
                         where entry.DerbyID == derbyID
                         orderby handler.HandlerName
                         select handler);

            if (query.Count() > 0)
            {
                myHandlers = new ObservableCollection<Handler>(query);
            }
            else
            {
                myHandlers = null;
            }
            return myHandlers;
        }

        public Int32 AddHandler(Handler newHandler)
        {
            DerbyMatchingDataContext dmDB = new DerbyMatchingDataContext();

            newHandler.DateCreated = DateTime.Now;
            dmDB.Handlers.InsertOnSubmit(newHandler);
            dmDB.SubmitChanges();

            return newHandler.HandlerID;
        }

        public void UpdateHandlerInfo(Handler currentHandler)
        {

            using (DerbyMatchingDataContext dmDB = new DerbyMatchingDataContext())
            {
                var handler = (from a in dmDB.Handlers
                               where a.HandlerID == currentHandler.HandlerID
                               select a).Single();

                handler.HandlerName = currentHandler.HandlerName;
                handler.LastUpdated = DateTime.Now;
                handler.UpdatedBy = "Administrator";
                dmDB.SubmitChanges();
            }
        }
    }
}
