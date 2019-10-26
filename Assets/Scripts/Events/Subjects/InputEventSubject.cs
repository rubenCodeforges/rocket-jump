using System;
using Enums;
using UnityEngine;

namespace Events
{
    
    public class InputEventSubject
    {
        public delegate void UserInputEventHandler(object target, UserInputEventArgs e);

        public event UserInputEventHandler UserInput;

        public virtual void OnUserInput(InputDirection direction)
        {
            UserInput?.Invoke(this, new UserInputEventArgs() {Direction = direction} );
        }
    }

    public class UserInputEventArgs : EventArgs
    {
        public InputDirection Direction { get; set; }
    };
}