using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace ChannelAdvisor
{
    /// <summary>
    /// Text box for integer values which allows NULL input
    /// </summary>
    public class NullableTextBox : TextBox
    {
        public event EventHandler<EventArgs> InvalidInput;

        [Bindable(true)]
        public new int? Text
        {
            get
            {
                if (!string.IsNullOrEmpty(base.Text))
                {
                    int i;
                    if (!int.TryParse(base.Text, out i))
                    {
                        if (InvalidInput != null)
                            InvalidInput(this, EventArgs.Empty);
                        return null;
                    }
                    else
                        return i;
                }
                return null;

            }
            set
            {
                if (value.HasValue == true)
                {
                    base.Text = value.Value.ToString();
                }
                else
                {
                    base.Text = null;
                }
            }
        }
    }

}
