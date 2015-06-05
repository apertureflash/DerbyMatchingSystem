using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Reflection;
using System.Linq.Expressions;
using System.ComponentModel;

namespace CockDerbyMatching_DAL
{

    public partial class DerbyMatchingDataContext : System.Data.Linq.DataContext
    {
        #region Extensibility Method Definitions
        partial void InsertUnMatched(UnMatched instance);
        partial void DeleteUnMatched(UnMatched instance);
        #endregion

        public System.Data.Linq.Table<UnMatched> UnMatcheds
        {
            get
            {
                return this.GetTable<UnMatched>();
            }
        }

    }



    public partial class UnMatched : INotifyPropertyChanging, INotifyPropertyChanged
    {
        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private Int32 _entryNumber;
        private String _entryName;
        private String _roosterLegBandNumber;
        private Decimal _roosterWeight;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnEntryNumberChanging(Int32 value);
        partial void OnEntryNumberChanged();
        partial void OnEntryNameChanging(String value);
        partial void OnEntryNameChanged();


        partial void OnRoosterLegBandNumberChanging(String value);
        partial void OnRoosterLegBandNumberChanged();
        partial void OnRoosterWeightChanging(Decimal value);
        partial void OnRoosterWeightChanged();
        #endregion

        public UnMatched(Int32 AEntryNumber, String AEntryName, String ARoosterLegBandNumber,
                         Decimal ARoosterWeight)
        {
            _entryNumber = AEntryNumber;
            _entryName = AEntryName;
            _roosterLegBandNumber = ARoosterLegBandNumber;
            _roosterWeight = ARoosterWeight;

            OnCreated();
        }

        public Int32 EntryNumber 
        { 
            get 
            { 
                return _entryNumber; 
            } 
        }

        public String EntryName 
        { 
            get 
            { 
                return _entryName; 
            } 
        }

        public String RoosterLegBandNumber 
        { 
            get 
            { 
                return _roosterLegBandNumber; 
            } 
        }

        public Decimal RoosterWeight 
        { 
            get 
            { 
                return _roosterWeight; 
            } 
        }

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
            if ((this.PropertyChanging != null))
            {
                this.PropertyChanging(this, emptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
