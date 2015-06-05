using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using CockDerbyMatching_DAL;

namespace CockDerbyMatching_BL
{
    public class Roosters
    {

        public ObservableCollection<Rooster> GetEntryRoosters(Int32 entryID)
        {
            DerbyMatchingDataContext dmDB = new DerbyMatchingDataContext();

            dmDB.DeferredLoadingEnabled = false;

            ObservableCollection<Rooster> myRoosters = new ObservableCollection<Rooster>(); ;

            var results = (from rooster in dmDB.Roosters where rooster.EntryID == entryID orderby rooster.RoosterWeight select rooster);

            if (results.Count() > 0)
            {
                myRoosters = new ObservableCollection<Rooster>(results);
                return myRoosters;
            }
            else
            {
                return null;
            }
        }

        public ObservableCollection<UnMatched> GetUnMatchedRoosterEntries()
        {
            DerbyMatchingDataContext dmDB = new DerbyMatchingDataContext();

            ObservableCollection<UnMatched> unMatched = new ObservableCollection<UnMatched>();

            var query = (from rs in dmDB.Roosters
                         join en in dmDB.Entries on rs.EntryID equals en.EntryID
                         where !(from fightsequences in dmDB.FightSequences
                                 select fightsequences.Rooster1ID).Contains(rs.RoosterID)
                            && !(from fightsequences in dmDB.FightSequences
                                 select fightsequences.Rooster2ID).Contains(rs.RoosterID)
                         select new 
                         {
                             en.EntryNumber,
                             en.EntryName,
                             rs.RoosterLegBandNumber,
                             rs.RoosterWeight
                         });

            //unMatched = (ObservableCollection<UnMatched>)query;

            
            foreach (var r in query)
                unMatched.Add(new UnMatched(r.EntryNumber, r.EntryName, r.RoosterLegBandNumber, r.RoosterWeight));
            
            return unMatched;
        }

        public void AddRooster(Rooster entryRooster)
        {
            DerbyMatchingDataContext dmDB = new DerbyMatchingDataContext();

            dmDB.Roosters.InsertOnSubmit(entryRooster);
            dmDB.SubmitChanges();
        }

        public void AddRoosters(ObservableCollection<Rooster> entryRoosters, Int32 entryID)
        {
            DerbyMatchingDataContext dmDB = new DerbyMatchingDataContext();

            foreach (Rooster newRooster in entryRoosters)
            {
                newRooster.EntryID = entryID;
                newRooster.DateCreated = DateTime.Now;
                dmDB.Roosters.InsertOnSubmit(newRooster);
            }

            dmDB.SubmitChanges();
        }

        public void EditRoosters(ObservableCollection<Rooster> currentRoosters)
        {
            DerbyMatchingDataContext dmDB = new DerbyMatchingDataContext();

            foreach (Rooster currentRooster in currentRoosters)
            {
                dmDB.Roosters.Attach(currentRooster);
                currentRooster.LastUpdated = DateTime.Now;
                dmDB.Refresh(System.Data.Linq.RefreshMode.KeepCurrentValues, currentRooster);
            }

            //dmDB.GetChangeSet();
            dmDB.SubmitChanges();
        }
    }
}
