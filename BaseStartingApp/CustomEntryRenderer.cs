using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartingApp
{
  
        public class CustomEntryRenderer : entry
        {
            public CustomEntryRenderer(Context context) : base(context)
            {
                AutoPackage = false;
            }
            protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
            {
                base.OnElementChanged(e);
                if (Control != null)
                {
                    Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
                }
            }
        }
    
}
