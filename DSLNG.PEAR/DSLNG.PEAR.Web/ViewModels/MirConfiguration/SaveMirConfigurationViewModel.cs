using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSLNG.PEAR.Web.ViewModels.MirConfiguration
{
    public class SaveMirConfigurationViewModel
    {
        public SaveMirConfigurationViewModel()
        {
            IsActive = true;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
    }
}