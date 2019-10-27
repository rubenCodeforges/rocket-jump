using System;
using Enums;
using UnityEngine;

namespace Events
{
    
    public class InputEventSubject
    {
        public delegate void UserInputEventHandler(object target, UserInputEventArgs e);
        public delegate void UserInputStopEventHandler(object target, UserInputEventArgs e);

        public event UserInputEventHandler UserInput;
        public event UserInputStopEventHandler UserInputStop;

        public virtual void OnUserInput(InputDirection direction)
        {
            UserInput?.Invoke(this, new UserInputEventArgs() {Direction = direction} );
        }

        public virtual void OnUserInputStop()
        {
            UserInputStop?.Invoke(this, null );
        }
    }

    public class UserInputEventArgs : EventArgs
    {
        public InputDirection Direction { get; set; }
    };
}