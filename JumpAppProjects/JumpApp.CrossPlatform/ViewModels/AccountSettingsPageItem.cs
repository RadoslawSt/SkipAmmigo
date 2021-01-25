using System;
using System.Collections.Generic;
using System.Text;

namespace JumpApp.ViewModels
{
    public class AccountSettingsPageItem
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string IconStart { get; set; }
        public string IconEnd { get; set; }
        public Type TargetType { get; set; }
    }
}
