using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using CockDerbyMatching_DAL;

namespace CockDerbyMatching_BL
{
    public class Derbies
    {
        public Derby GetDerby()
        {
            DerbyMatchingDataContext dmDB = new DerbyMatchingDataContext();

            dmDB.DeferredLoadingEnabled = false;

            Derby myDerby = null;

            myDerby = (from derby in dmDB.Derbies orderby derby.DerbyID ascending select derby).FirstOrDefault();

            return myDerby;
        }

        public void ResetDerby()
        {
            DerbyMatchingDataContext dmDB = new DerbyMatchingDataContext();

            dmDB.usp_ResetDerbyInfo();
        }

        public void UpdateDerbyInfo(Derby derbyInfo)
        {

            using (DerbyMatchingDataContext dmDB = new DerbyMatchingDataContext())
            {
                var derby = (from a in dmDB.Derbies
                             where a.DerbyID == derbyInfo.DerbyID
                             select a).SingleOrDefault();


                if (derby == null)
                {
                    derbyInfo.DateCreated = DateTime.Now;
                    derbyInfo.CreatedBy = "Administrator";
                    dmDB.Derbies.InsertOnSubmit(derbyInfo);
                }
                else
                {

                    derby.DerbyName = derbyInfo.DerbyName;
                    derby.DerbyDate = derbyInfo.DerbyDate;
                    derby.NumberOfEntries = derbyInfo.NumberOfEntries;
                    derby.UpdatedBy = "Administrator";
                }
                dmDB.SubmitChanges();

            }
        }

        /*
        public List<FightSequence> GenerateFightSequence()
        {
            DerbyMatchingDataContext dmDB = new DerbyMatchingDataContext();

            List<FightSequence> myFightSequence = null;

            myFightSequence = (from ftSeq in dmDB.FightSequences select ftSeq).ToList();

            return myFightSequence;
        }
         */

        public List<FightSequence> GenerateFightSequence()
        {
            DerbyMatchingDataContext dmDB = new DerbyMatchingDataContext();

            List<FightSequence> myFightSequence = new List<FightSequence>();

            myFightSequence = dmDB.usp_CreateRoosterMatching().ToList();

            //myFightSequence = results.ToList();
            return myFightSequence;
        }

        public List<FightSequence> GetExistingFightSequence()
        {
            DerbyMatchingDataContext dmDB = new DerbyMatchingDataContext();

            dmDB.DeferredLoadingEnabled = true;

            List<FightSequence> fightSequence = null;

            fightSequence = (from ft in dmDB.FightSequences orderby ft.FightNumber select ft).ToList();

            if (fightSequence.Count > 0)
            {
                return fightSequence;
            }
            else
            {
                return null;
            }

        }

        public ObservableCollection<HandPickedMatch> GetHandPickedMatches()
        {
            DerbyMatchingDataContext dmDB = new DerbyMatchingDataContext();

            dmDB.DeferredLoadingEnabled = true;

            ObservableCollection<HandPickedMatch> handPickedMatches = null;

            var results = (from hpm in dmDB.HandPickedMatches orderby hpm.Rooster1Weight select hpm);

            if (results.Count() > 0)
            {
                handPickedMatches = new ObservableCollection<HandPickedMatch>(results);
                return handPickedMatches;
            }
            else
            {
                return null;
            }

        }

        public void DeleteHandPickedMatch(Int32 matchUpID)
        {
            DerbyMatchingDataContext dmDB = new DerbyMatchingDataContext();

            dmDB.HandPickedMatches.DeleteAllOnSubmit(dmDB.HandPickedMatches.Where(f => f.MatchUpID == matchUpID));
            dmDB.SubmitChanges();
        }

        public void AddHandPickedMatch(HandPickedMatch handPickedMatch)
        {
            DerbyMatchingDataContext dmDB = new DerbyMatchingDataContext();

            dmDB.HandPickedMatches.InsertOnSubmit(handPickedMatch);
            dmDB.SubmitChanges();
        }
    }
}
