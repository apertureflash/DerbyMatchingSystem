using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data.Linq;
using System.Text;
using CockDerbyMatching_DAL;


namespace CockDerbyMatching_BL
{

    public class Entries
    {

        public ObservableCollection<Entry> GetDerbyEntries(Int32 derbyID)
        {
            var dmDB = new DerbyMatchingDataContext();

            //dmDB.DeferredLoadingEnabled = false;

            ObservableCollection<Entry> myEntries = null;

            var query = (from entry in dmDB.Entries 
                         where entry.DerbyID == derbyID 
                         orderby entry.EntryName 
                         select entry);

            if (query.Count() > 0)
            {
                myEntries = new ObservableCollection<Entry>(query);
            }
            else
            {
                myEntries = null;
            }
            return myEntries;
        }

        public ObservableCollection<Entry> GetNoFightEntries(Int32 entryID)
        {
            var dmDB = new DerbyMatchingDataContext();

            ObservableCollection<Entry> entryNoFights = null;

            var query =   ((from nf in dmDB.NoFights
                              join en1 in dmDB.Entries on nf.EntryID1 equals en1.EntryID
                              where nf.EntryID2 == entryID
                              select en1
                             ).Union
                             (from nf in dmDB.NoFights
                              join en2 in dmDB.Entries on nf.EntryID2 equals en2.EntryID
                              where nf.EntryID1 == entryID
                              select en2 
                             ));

            entryNoFights = new ObservableCollection<Entry>(query);

            return entryNoFights;
        }

        public ObservableCollection<Entry> GetAvailableFights(Int32 entryID)
        {

            var dmDB = new DerbyMatchingDataContext();

            ObservableCollection<Entry> myEntries = null;

            var res = (
                       from entries in dmDB.Entries
                       where entries.EntryID != entryID
                         && !(
                               (
                                from nofights in dmDB.NoFights
                                where nofights.EntryID2 == entryID
                                select nofights.EntryID1
                               ).Union
                               (
                                from nofights in dmDB.NoFights
                                where nofights.EntryID1 == entryID
                                select nofights.EntryID2
                               )
                              ).Contains(entries.EntryID)
                       orderby entries.EntryName
                       select entries
                      );

            myEntries = new ObservableCollection<Entry>(res);
            return myEntries;
        }

        public Entry GetEntry(Int32 entryID)
        {
            var dmDB = new DerbyMatchingDataContext();


            Entry var = (from ent in dmDB.Entries where ent.EntryID == entryID select ent).FirstOrDefault();

            return var;
        }

        public void UpdateEntryInfo(Entry currentEntry)
        {

            using (var dmDB = new DerbyMatchingDataContext())
            {
                var entry = (from a in dmDB.Entries
                             where a.EntryID == currentEntry.EntryID
                             select a).Single();

                entry.EntryName = currentEntry.EntryName;
                entry.LastUpdated = DateTime.Now;
                entry.UpdatedBy = "Administrator";
                dmDB.SubmitChanges();
            }


            //DerbyMatchingDataContext dmDB = new DerbyMatchingDataContext();
            //Entry getEntry = currentEntry;

            //dmDB.Entries.Attach(getEntry);
            //currentEntry.LastUpdated = DateTime.Now;
            //dmDB.Refresh(System.Data.Linq.RefreshMode.KeepCurrentValues, currentEntry);

            //dmDB.SubmitChanges();
        }

        public Int32 AddDerbyEntry(Entry newEntry)
        {
            var dmDB = new DerbyMatchingDataContext();

            newEntry.DateCreated = DateTime.Now;
            dmDB.Entries.InsertOnSubmit(newEntry);
            dmDB.SubmitChanges();

            return newEntry.EntryID;
        }

        public void DeleteEntry(Int32 entryID)
        {
            var dmDB = new DerbyMatchingDataContext();

            dmDB.usp_DeleteEntry(entryID);

        }

        public void SetNoFightEntries(ObservableCollection<Entry> noFightEntries, Int32 entryID)
        {
            var dmDB = new DerbyMatchingDataContext();

            //dmDB.Connection.BeginTransaction();

            dmDB.NoFights.DeleteAllOnSubmit(dmDB.NoFights.Where(f => f.EntryID1 == entryID));
            dmDB.NoFights.DeleteAllOnSubmit(dmDB.NoFights.Where(f => f.EntryID2 == entryID));

            foreach (var ent in noFightEntries)
            {
                var nfEntry = new NoFight
                {
                    EntryID1 = entryID,
                    EntryID2 = ent.EntryID,
                    CreatedBy = "Administrator",
                    DateCreated = DateTime.Now
                };

                dmDB.NoFights.InsertOnSubmit(nfEntry);
            }

            dmDB.SubmitChanges();

            //dmDB.Transaction.Commit();
        }
    }

    public class MaxNumber
    {
        private readonly Int32 _Column1;

        public MaxNumber(Int32 AColumn1)
        {
            _Column1 = AColumn1 + 1;
        }
        public Int32 NewEntryNumber 
        { 
            get { 
                return _Column1; 
            } 
        }
    }

    public class MaxEntryNumber
    {
        private readonly Int32 _maxNumber;

        public MaxEntryNumber()
        {
            var dmDB = new DerbyMatchingDataContext();
            var query =
                from entries in
                    (from entries in dmDB.Entries
                     select new
                     {
                         entries.EntryNumber,
                         Dummy = "x"
                     })
                group entries by new { entries.Dummy } into g
                select new
                {
                    NewEntryNumber = g.Max(p => p.EntryNumber)
                };

            /*
            foreach (var r in query)
                Add(new MaxNumber(
                    r.NewEntryNumber));
            */

            try
            {
                _maxNumber = query.First().NewEntryNumber;
            }
            catch
            {
                _maxNumber = 0;
            }
        }

        public Int32 NewEntryNumber
        {
            get
            {
                return _maxNumber + 1;
            }
        }
    }


    public class MaxGameFowlNumber
    {
        private Int32 _maxNumber;
        private String _latestLBNumber;

        public MaxGameFowlNumber()
        {
            var dmDB = new DerbyMatchingDataContext();

            
            // get id of last record
            var query =
                from roosters in
                    (from roosters in dmDB.Roosters
                     select new
                     {
                         roosters.RoosterID,
                         Dummy = "x"
                     })
                group roosters by new { roosters.Dummy } into g
                select new
                {
                    lastRoosterID = g.Max(p => p.RoosterID)
                };


            // get the leg band number of the last record
            var query2 = from roosters in dmDB.Roosters
                         where roosters.RoosterID == query.First().lastRoosterID
                         select new
                         {
                             lastLBNumber = roosters.RoosterLegBandNumber
                         };
            

            var lastRecord = dmDB.Roosters.OrderByDescending(p => p.RoosterID).Take(1);


            /*
            try
            {
                Int32 temp;
                Boolean isNum = Int32.TryParse(lastRecord.First().RoosterLegBandNumber, out temp);

                if (isNum)
                {
                    _maxNumber = temp + 1;
                }
                else
                {
                    _maxNumber = 0;
                }
                //_maxNumber = query2.First().lastLBNumber;
            }
            catch
            {
                if (_maxNumber > 0)
                {
                    _maxNumber = _maxNumber + 1;
                }
                else
                {
                    _maxNumber = 1;
                }
            }

            */
        }

        public String NewLegBandNumber
        {
            get
            {
                return _maxNumber.ToString();
            }
        }
    }
}
