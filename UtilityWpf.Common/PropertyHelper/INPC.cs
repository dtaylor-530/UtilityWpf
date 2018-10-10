using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UtilityWpf
{

    public abstract class NPC : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Implementation
        /// <summary>
        /// Occurs when any properties are changed on this object.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        ///  raises the PropertyChanged event for one to many properties.
        /// </summary>
        /// <param name="propertyNames">The names of the properties that changed.</param>
        public virtual void OnPropertiesChanged(params string[] propertyNames)
        {
            foreach (string name in propertyNames)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }

        /// <summary>
        ///  raises the PropertyChanged event for single property
        ///  propertyname can be left null (e.g OnPropertyChanged()) if called from body of property 
        /// </summary>
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        #endregion


    }




}
