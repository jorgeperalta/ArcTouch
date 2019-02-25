using System;
using System.Collections.Generic;
using System.Text;
using ArcTouch.Core.Resources;
using MvvmCross.ViewModels;

namespace ArcTouch.Core.ViewModels
{
    public abstract class BaseViewModel : MvxViewModel
    {
        /// <summary>
        /// Gets the internationalized string at the given <paramref name="index"/>, which is the key of the resource.
        /// </summary>
        /// <param name="index">Index key of the string from the resources of internationalized strings.</param>
        public string this[string index] => Strings.ResourceManager.GetString(index);
    }
}
