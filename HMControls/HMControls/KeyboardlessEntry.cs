using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using HMExtension.Xamarin;

namespace HMControls
{
    public abstract class KeyboardlessEntry : StandardEntry
    {
        public KeyboardlessEntry()
        {
            Focused += KeyboardlessEntry_Focused;
        }

        private ContentPage MasterParent { get; set; } = null;

        private bool Focusable { get; set; } = true;

        private bool IsMasterParentAppear { get; set; } = true;

        private void KeyboardlessEntry_Focused(object sender, FocusEventArgs e)
        {
            if (MasterParent == null)
            {
                MasterParent = this.GetParent<ContentPage>();
                if (MasterParent != null)
                {
                    MasterParent.Appearing -= MasterParent_Appearing;
                    MasterParent.Disappearing -= MasterParent_Disappearing;

                    MasterParent.Appearing += MasterParent_Appearing;
                    MasterParent.Disappearing += MasterParent_Disappearing;
                }
            }

            if (Focusable && IsMasterParentAppear)
            {
                ActionOnFocused();
            }
            else
            {
                Unfocus();
            }
        }

        private async void MasterParent_Appearing(object sender, EventArgs e)
        {
            Focusable = false;
            await Task.Delay(200);
            Focusable = true;
            IsMasterParentAppear = true;
        }

        private void MasterParent_Disappearing(object sender, EventArgs e)
        {
            IsMasterParentAppear = false;
        }

        public abstract void ActionOnFocused();
    }
}
